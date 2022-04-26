#include <string>

using namespace std;

int initialize_mcast_sender();
int initialize_mcast_receiver(struct sockaddr_in addr, struct ip_mreq mreq);
int mcast_send(int sock, struct sockaddr_in addr, string& message);
int mcast_receive(int sock, struct sockaddr_in addr, string& strMessage, string& recvAddr);

