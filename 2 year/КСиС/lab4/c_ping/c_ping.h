#ifndef _CUSTOM_PING_H
#define _CUSTOM_PING_H

#include <time.h>
#include <errno.h>
#include <string.h>
#include <unistd.h>

#include <netdb.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <sys/socket.h>

#include "../xmemory.h"
#include "../prothandy.h"
#include "../printerr.h"

struct c_ping_in {
    union {
        uint32_t ip;        // destination ip address for pinging
        const char* domain; // possible variant to pass domain instead of ip (not supported)
    };
    uint8_t attempts;       // count of maximum sent packet in future
    uint32_t hostip;        // host ip address from which pinging is performed
};

int c_ping_to(struct c_ping_in in);
void c_ping_print(struct iphdr* iphdr, struct icmphdr* icmphdr, 
    int attempt, double elapsed);
void c_ping_final_print(int sent, int received);

#endif // _CUSTOM_PING_H