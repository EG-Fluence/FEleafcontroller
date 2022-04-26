#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <time.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>

#include "multicast.h"
#include "run_loop.h"
#include "multicast.h"
#include "Log.hxx"
#include "can.h"
#include <string>
#include <signal.h>
#include <stdio.h>

#include <sys/types.h>
#include <ifaddrs.h>
#include <netinet/in.h>
#include <string.h>
#include <arpa/inet.h>
#include "process_params.h"

extern "C" {
#include "lib.h"
}

using namespace logging;
using namespace std;
using namespace runloop;

// config parameters are global

Params params;

void getLocalIpAddresses(list<string>& ips) {
    struct ifaddrs * ifAddrStruct=NULL;
    struct ifaddrs * ifa=NULL;
    void * tmpAddrPtr=NULL;

    getifaddrs(&ifAddrStruct);

    for (ifa = ifAddrStruct; ifa != NULL; ifa = ifa->ifa_next) {
        if (!ifa->ifa_addr) {
            continue;
        }
        if (ifa->ifa_addr->sa_family == AF_INET) { // check it is IP4
            // is a valid IP4 Address
            tmpAddrPtr=&((struct sockaddr_in *)ifa->ifa_addr)->sin_addr;
            char addressBuffer[INET_ADDRSTRLEN]; memset(addressBuffer,0,INET_ADDRSTRLEN);
            inet_ntop(AF_INET, tmpAddrPtr, addressBuffer, INET_ADDRSTRLEN);
            printf("%s IP Address %s\n", ifa->ifa_name, addressBuffer);
            string ip(addressBuffer);
            ips.push_back(ip);
        } else if (ifa->ifa_addr->sa_family == AF_INET6) { // check it is IP6
            // is a valid IP6 Address
            tmpAddrPtr=&((struct sockaddr_in6 *)ifa->ifa_addr)->sin6_addr;
            char addressBuffer[INET6_ADDRSTRLEN]; memset(addressBuffer,0,INET6_ADDRSTRLEN);
            inet_ntop(AF_INET6, tmpAddrPtr, addressBuffer, INET6_ADDRSTRLEN);
            printf("%s IP Address %s\n", ifa->ifa_name, addressBuffer);
            string ip(addressBuffer);
            ips.push_back(ip);
        }
    }
    if (ifAddrStruct!=NULL) freeifaddrs(ifAddrStruct);
}

static int running = 1;
static list<string> ips;

void callback_sigint(int signal) {
    minfo ("SIGINT Callback. Terminating program.");
    running = 0;
}

void callback_error (void* err_data) {
    merr ("Error Callback\n");
}

void callback_timeout (void* to_data) {
    minfo ("Timeout Callback\n");
}

static list<string> lastSendCanMessage;

void cb_mcast_receive (int listenDescriptor, void* data) {
    minfo ("Incoming multicast message\n");
    int fd_can_snd = *((int*)data);

    struct sockaddr_in receiverAddr;
    bzero((char *)&receiverAddr, sizeof(receiverAddr));
    receiverAddr.sin_family = AF_INET;
    receiverAddr.sin_addr.s_addr = htonl(INADDR_ANY);
    receiverAddr.sin_port = htons(params.mcast_port);

    string recvMessage, recvAddr;
    mcast_receive(listenDescriptor, receiverAddr, recvMessage, recvAddr);

    // --- we have to check that this mcast message does not come from us !
    for (std::list<string>::iterator it=ips.begin(); it != ips.end(); ++it) {
    	if ((*it)==recvAddr) {
    		mdebug("Ignoring mcast message since it comes from us.");
    		return;
    	}
    }

    struct canfd_frame frame;
    int required_mtu = parse_canframe(recvMessage.c_str(), &frame);
	if (!required_mtu) {
		merr("Wrong CAN Format. Skipping this message.");
		return;
	}
	can_send(fd_can_snd,frame,required_mtu);
	lastSendCanMessage.push_back(recvMessage);

}

void cb_can_receive (int listenDescriptor, void* data) {
    minfo ("Incoming CAN message\n");
    int fd_mcast_snd = *((int*)data);

    struct sockaddr_can addr;
    addr.can_family = AF_CAN;
    addr.can_ifindex = 0;

    char ctrlmsg[CMSG_SPACE(sizeof(struct timeval) + 3*sizeof(struct timespec) + sizeof(__u32))];
    struct canfd_frame frame;
    struct iovec iov;
    struct msghdr msg;
    iov.iov_base = &frame;
    msg.msg_name = &addr;
    msg.msg_iov = &iov;
    msg.msg_iovlen = 1;
    msg.msg_control = &ctrlmsg;

    iov.iov_len = sizeof(frame);
    msg.msg_namelen = sizeof(addr);
    msg.msg_controllen = sizeof(ctrlmsg);
    msg.msg_flags = 0;

    int nbytes = can_receive(listenDescriptor,msg);
    int maxdlen=0;
	if ((size_t)nbytes == CAN_MTU)
		maxdlen = CAN_MAX_DLEN;
	else if ((size_t)nbytes == CANFD_MTU)
		maxdlen = CANFD_MAX_DLEN;
	else {
		fprintf(stderr, "read: incomplete CAN frame\n");
		return;
	}

    char buf[CL_CFSZ]; /* max length */
	/* log CAN frame with absolute timestamp & device */
	int rc = sprint_canframe(buf, &frame, 0, maxdlen);
	mdump(buf,rc);

	struct sockaddr_in senderAddr;

	bzero((char *)&senderAddr, sizeof(senderAddr));
    senderAddr.sin_family = AF_INET;
    senderAddr.sin_addr.s_addr = inet_addr(params.mcast_ip);
    senderAddr.sin_port = htons(params.mcast_port);

    string s(buf,rc);

    for (std::list<string>::iterator it=lastSendCanMessage.begin(); it != lastSendCanMessage.end(); ++it) {
    	if ((*it) == s) {
    		lastSendCanMessage.remove(s);
        	mdebug("Ignoring CAN message since it comes from us.");
    		return;
    	}
    }

    // --- for secure reasons
    if (lastSendCanMessage.size()>5) {
    	merr("We have a problem with the loop can messages.");
    }

    mcast_send(fd_mcast_snd, senderAddr, s);

}


int main(int argc, char **argv) {

	try {
		openlog("can2ethernet", LOG_CONS | LOG_PID | LOG_NDELAY | LOG_PERROR, LOG_LOCAL1);
		setlogmask(LOG_UPTO(LOG_DEBUG));

		signal(SIGINT, callback_sigint);

		memset(&params,0,sizeof(Params));
		params.verbose_flag = 1;
		retrieve_params (argc, argv, &params);
		if (!params.can_interface_name) {
			params.can_interface_name = "can0";
			minfo("No 'can_interface_name', defaults to: " + params.can_interface_name);
		}
		if (!params.mcast_ip) {
			params.mcast_ip = "239.0.0.1";
			minfo("No 'mcast_ip', defaults to: " + params.mcast_ip);
		}
		if (!params.mcast_port) {
			params.mcast_port = 6000;
			minfo("No 'mcast_port', defaults to: " + params.mcast_port);

		}

		run_loop_init(&callback_timeout, &callback_error, 60, 0);

		// --- initialize multicast receiver and sender
		struct sockaddr_in senderAddr, receiverAddr;

		bzero((char *)&senderAddr, sizeof(senderAddr));
		senderAddr.sin_family = AF_INET;
		senderAddr.sin_addr.s_addr = inet_addr(params.mcast_ip);
		senderAddr.sin_port = htons(params.mcast_port);

		bzero((char *)&receiverAddr, sizeof(receiverAddr));
		receiverAddr.sin_family = AF_INET;
		receiverAddr.sin_addr.s_addr = htonl(INADDR_ANY);
		receiverAddr.sin_port = htons(params.mcast_port);

		struct ip_mreq mreq;
		mreq.imr_multiaddr.s_addr = inet_addr(params.mcast_ip);
		mreq.imr_interface.s_addr = htonl(INADDR_ANY);

		int fd_mcast_recv = initialize_mcast_receiver(receiverAddr,mreq);
		int fd_mcast_snd = initialize_mcast_sender();

		// --- initialize can receiver and sender
		struct sockaddr_can addr;
		addr.can_family = AF_CAN;
		addr.can_ifindex = 0;

		int fd_can_recv = initialize_can_receiver(addr);
		int fd_can_snd = initialize_can_sender(params.can_interface_name);

		// --- add the receiver sockets to the run loop
		run_loop_add_fd(fd_mcast_recv, cb_mcast_receive, &fd_can_snd);
		run_loop_add_fd(fd_can_recv, cb_can_receive, &fd_mcast_snd);

		// --- get local ip addresses
		getLocalIpAddresses(ips);
		mdebug("We have " + ips.size() + " addresses found.");

		while (running) {
			run_loop_run();
		}
	} catch (exception& e) {
		merr("Aborting ... " + e.what());
	}
    return 0;
}
