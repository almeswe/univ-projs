#include <errno.h>
#include <stdbool.h>
#include <unistd.h>

#include <netinet/ip_icmp.h>

bool ping_to(uint32_t ip);