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




    return 0;
}
