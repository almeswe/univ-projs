#include "interact.h"

uint16_t ask_for_port() {
    int32_t port;
    while (true) {
        printf("Enter port number: "); 
        if (scanf("%d", &port) == 1) {
            if (port >= 0 && port <= 65535) {
                return (uint16_t)port; 
            }
        }
        while (getchar() != '\n');
        printf("Incorrect input, try again.\n");
    }
    return 0;
}

uint32_t ask_for_ip4() {
    uint8_t octets[4]; 
    memset(octets, 0, sizeof octets);
    create_buf(ip, DEFAULT_INPUT_SIZE);
    
    while (true) {
        printf("Enter ip (xxx.xxx.xxx.xxx): "); 
        if (scanf("%s", ip) == 1) {
            if (sscanf(ip, "%hhd.%hhd.%hhd.%hhd", &octets[0], 
                    &octets[1], &octets[2], &octets[3]) == 4) {
                // it is not correct ...
                bool passed = true;
                for (int i = 0; i < 4; i++) {
                    if (octets[i] < 0 || octets[i] > 255) {
                        passed = false; break;
                    }
                }
                if (passed) {
                    printf("\n%d\n", *(((uint32_t*)octets)));
                    return *((uint32_t*)octets); 
                } 
            }
        }
        while (getchar() != '\n');
        printf("Incorrect input, try again.\n");
    }
}