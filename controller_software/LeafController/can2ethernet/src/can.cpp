#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#include <net/if.h>
#include <sys/ioctl.h>
#include <sys/socket.h>

#include <linux/can.h>
#include <linux/can/raw.h>
#include <errno.h>
#include <exception>

#include "Log.hxx"
#include "can.h"

int initialize_can_sender(const char* interface) {

    int s = socket(PF_CAN, SOCK_RAW, CAN_RAW);
    if (s < 0) {
        merr("socket:" +strerror(errno));
        exit(1);
    }
    
        struct ifreq ifr;

    strncpy(ifr.ifr_name, interface, IFNAMSIZ - 1);
    ifr.ifr_name[IFNAMSIZ - 1] = '\0';
    ifr.ifr_ifindex = if_nametoindex(ifr.ifr_name);
    if (!ifr.ifr_ifindex) {
        merr("if_nametoindex");
        throw std::runtime_error("CAN Interface does not exist");
        return 1;
    }

	struct sockaddr_can addr;
    memset(&addr, 0, sizeof(addr));
    addr.can_family = AF_CAN;
    addr.can_ifindex = ifr.ifr_ifindex;

    setsockopt(s, SOL_CAN_RAW, CAN_RAW_FILTER, NULL, 0);

    if (bind(s, (struct sockaddr *)&addr, sizeof(addr)) < 0) {
        merr("bind:" + strerror(errno));
        return 1;
    }
    
    return s;
}

int initialize_can_receiver(struct sockaddr_can addr) {
    int sock = socket(PF_CAN, SOCK_RAW, CAN_RAW);
    if (sock < 0) {
        merr("socket:" + strerror(errno));
        exit(1);
    }

    if (bind(sock, (struct sockaddr *)&addr, sizeof(addr)) < 0) {
        merr("bind:" + strerror(errno));
        exit(1);
    }

    return sock;
}

int can_receive(int sock, struct msghdr& msg) {

    int nbytes = recvmsg(sock, &msg, 0);

    if (nbytes < 0) {
        merr("read:" + strerror(errno));
        exit(1); // TODO
    }
    return nbytes;

}

int can_send(int s, struct canfd_frame frame, int required_mtu) {

	if (write(s, &frame, required_mtu) != required_mtu) {
		perror("write");
		return 1;
	}
	return required_mtu;
}
