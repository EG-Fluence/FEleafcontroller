/*

multicast.c

The following program sends or receives multicast packets. If invoked
with one argument, it sends a packet containing the current time to an
arbitrarily chosen multicast group and UDP port. If invoked with no
arguments, it receives and prints these packets. Start it as a sender on
just one host and as a receiver on all the other hosts

*/

#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <time.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <errno.h>

#include "Log.hxx"

using namespace logging;

int initialize_mcast_sender() {

    int sock = socket(AF_INET, SOCK_DGRAM, 0);
    if (sock < 0) {
        merr("sender socket failure");
    }
    return sock;
}

int mcast_send(int sock, struct sockaddr_in addr, string& message) {
    int addrlen = sizeof(addr);

    int cnt = sendto(sock, message.c_str(), message.length(), 0,
                 (struct sockaddr *) &addr, addrlen);
    if (cnt < 0) {
        merr("send to multicast failed");
    }
    return cnt;
}

int mcast_receive(int sock, struct sockaddr_in addr, string& strMessage, string& recvAddr) {
	char message[64]; memset(message,0,64);
	int addrlen = sizeof(addr);
	
    int cnt = recvfrom(sock, message, sizeof(message), 0,
                   (struct sockaddr *) &addr, (socklen_t*)&addrlen);
    if (cnt < 0) {
        merr("recvfrom" + strerror(errno));
        exit(1);
    } else if (cnt == 0) {
        return 0;
    }
    string tmp(message, cnt);
    strMessage.swap(tmp); tmp.clear();
    recvAddr = string(inet_ntoa(addr.sin_addr));
    mdebug(string(inet_ntoa(addr.sin_addr)) + ":" + strMessage);
    return cnt;
}

int initialize_mcast_receiver(struct sockaddr_in addr, struct ip_mreq mreq) {
    int addrlen, sock, cnt;

    /* set up socket */
    sock = socket(AF_INET, SOCK_DGRAM, 0);
    if (sock < 0) {
        perror("socket");
        exit(1);
    }
    addrlen = sizeof(addr);

    /* receive */
    if (bind(sock, (struct sockaddr *) &addr, sizeof(addr)) < 0) {
        merr("bind");
        exit(1);
    }

    if (setsockopt(sock, IPPROTO_IP, IP_ADD_MEMBERSHIP,
                   &mreq, sizeof(mreq)) < 0) {
        merr("setsockopt mreq");
        exit(1);
    }
    return sock;
}
