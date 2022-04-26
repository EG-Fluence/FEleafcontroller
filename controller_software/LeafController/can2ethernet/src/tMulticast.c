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

#include "multicast.h"

using namespace logging;

static const int MCAST_PORT = 6000;
static const char* MCAST_GROUP = "239.0.0.1";

int main() {
	openlog("can2ethernet", LOG_CONS | LOG_PID | LOG_NDELAY | LOG_PERROR, LOG_LOCAL1);
    setlogmask(LOG_UPTO(LOG_DEBUG));
    
	{
		struct sockaddr_in senderAddr, receiverAddr;
		
		bzero((char *)&senderAddr, sizeof(senderAddr));
        senderAddr.sin_family = AF_INET;
        senderAddr.sin_addr.s_addr = inet_addr(MCAST_GROUP);
        senderAddr.sin_port = htons(MCAST_PORT);
        
        bzero((char *)&receiverAddr, sizeof(receiverAddr));
        receiverAddr.sin_family = AF_INET;
        receiverAddr.sin_addr.s_addr = htonl(INADDR_ANY);
        receiverAddr.sin_port = htons(MCAST_PORT);
        
        struct ip_mreq mreq;
        mreq.imr_multiaddr.s_addr = inet_addr(MCAST_GROUP);
		mreq.imr_interface.s_addr = htonl(INADDR_ANY);
		
		int fd_mcast_recv = initialize_mcast_receiver(receiverAddr,mreq);
		
		string message = "Hello World";
		
		int senderSock = initialize_mcast_sender();
		mcast_send(senderSock, senderAddr, message);
		
		string recvMessage, recvAddr;
		mcast_receive(fd_mcast_recv, receiverAddr, recvMessage, recvAddr);
		
		mdebug(recvMessage) 
	}

	return 0;
}
