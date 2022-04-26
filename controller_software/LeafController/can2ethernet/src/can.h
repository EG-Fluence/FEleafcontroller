#include <linux/can.h>

int initialize_can_sender(const char* interface);
int initialize_can_receiver(struct sockaddr_can addr);

int can_receive(int sock, struct msghdr& msg);
int can_send(int s, struct canfd_frame frame, int required_mtu);
