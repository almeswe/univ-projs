#include "subnet.h"

#define sockaddr__cast struct sockaddr_in*

bool get_interface_info(char* interface, 
    __Out__ uint32_t* mask, __Out__ uint32_t* hostip) {
    
    struct ifaddrs* temp;
    struct ifaddrs* interfaces;

    if (getifaddrs(&interfaces) != 0) {
        return printf("Cannot get the network interfaces for this machine.\n"), false;
    }

    for (temp = interfaces; interfaces; interfaces = interfaces->ifa_next){
        if (strcmp(interface, interfaces->ifa_name) == 0) {
            if (interfaces->ifa_netmask) {
                *mask = ((sockaddr__cast)(interfaces->ifa_netmask))
                    ->sin_addr.s_addr;
                *hostip = ((sockaddr__cast)(interfaces->ifa_addr))
                    ->sin_addr.s_addr;
                return freeifaddrs(temp), true;
            }
        }
    }
    return printf("Interface not found.\n"), 
        freeifaddrs(temp), false;
}

bool get_subnet_nodes(uint32_t mask, uint32_t hostip,
    __Out__ uint32_t* nodes) {

    int count = 0;
    for (uint32_t ip = htonl(hostip & mask) + 1; ip < htonl(~mask | hostip); ip++) {
        
        struct in_addr addr = { .s_addr = htonl(ip) };

        if (ping_to(ip)) {
            printf("[  OK  ] %s\n", inet_ntoa(addr));
            if (count < MAX_NODES_STORED) {
                ((nodes)[count++]) = ip; 
            }
        } /*else {
            printf("[ FAIL ] %s\n", inet_ntoa(addr));
        }*/
    }
}

bool get_mac_address(uint32_t ip,
    __Out__ char* mac) {
    
    FILE* fptr = fopen("/proc/net/arp", "r");
    if (fptr == NULL) {
        return printf("Cannot open arp cache file. "
            "Message: %s\n", strerror(errno)), false;
    }
    
    char line[1024];
    struct in_addr addr = {
        .s_addr = htonl(ip)
    };

    char* ipstr = inet_ntoa(addr);

    fgets(line, sizeof line, fptr);
    while (fgets(line, sizeof line, fptr) != NULL) {
        char ip[IP_ADDR_LEN];
        char unused[128];
        int32_t unusedi;
        sscanf(line, "%s 0x%x 0x%x %s %s %s\n", ip,
            &unusedi, &unusedi, mac, unused, unused);
        if (strcmp(ip, ipstr) == 0) {
            return fclose(fptr), true;
        }
    }
    return fclose(fptr), false;
}

bool get_host_mac_address(const char* interface,
    __Out__ char* mac) {
    
    char path[512];
    sprintf(path, "/sys/class/net/%s/address", interface);

    FILE* fptr = fopen(path, "r");
    if (fptr == NULL) {
        return printf("No such file for this interface found. "
            "Message: %s\n", strerror(errno)), false;
    }

    char* res = fgets(mac, MAC_ADDR_LEN, fptr);
    return fclose(fptr), (res != NULL); 
}