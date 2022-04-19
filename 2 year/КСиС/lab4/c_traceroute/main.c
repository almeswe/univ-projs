#include "c_traceroute.h"

int main(int argc, char** argv) {
    if (argc != 2) {
        printerr("%s\n", "Incorrect argument count passed.");
        return 1;
    }
    struct c_traceroute_in in = {
        .hostip = get_hostip(),
        .ip = inet_addr(argv[1])
    };
    c_traceroute_for(in);
    return 0;
}