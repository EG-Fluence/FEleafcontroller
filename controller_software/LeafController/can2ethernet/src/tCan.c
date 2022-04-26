#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <time.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>

#include "Log.hxx"

#include "can.h"

extern "C" {
#include "lib.h"
}

using namespace logging;

int main() {


    openlog("can2ethernet", LOG_CONS | LOG_PID | LOG_NDELAY | LOG_PERROR, LOG_LOCAL1);
    setlogmask(LOG_UPTO(LOG_DEBUG));

	{
		struct canfd_frame frame;
		int required_mtu = parse_canframe("127#DEADBEEF", &frame);
		if (!required_mtu) {
			fprintf(stderr, "\nWrong CAN-frame format!\n\n");
			exit(1);
		}
		int sock = initialize_can_sender();
		can_send(sock,frame,required_mtu);
	}



    {

        struct sockaddr_can addr;
        addr.can_family = AF_CAN;
        addr.can_ifindex = 0;

        int can_receiver_sock = initialize_can_receiver(addr);

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

        int cnt = can_receive(can_receiver_sock,msg);
        char buf[2048];
        memset(buf,0,2048);
        sprint_canframe(buf, &frame, 0, cnt);
        mdump(buf,cnt);

    }


    return 0;
}
