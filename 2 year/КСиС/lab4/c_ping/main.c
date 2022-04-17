#include "c_ping.h"

int main(int argc, char** argv) {
    if (argc != 2) {
        printerr("%s\n", "Incorrect argument count passed.");
    }
    struct c_ping_in in = {
        .attempts = 4,
        .ip = inet_addr(argv[1]),
        .hostip = inet_addr("192.168.100.86")
    };
    c_ping_to(in);
    return 0;
}