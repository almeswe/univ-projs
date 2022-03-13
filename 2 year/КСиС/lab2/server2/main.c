#include "server.h"

int main(int argc, char** argv) {
    if (argc != 2) {
        serv_start(serv_init(argv[1], atoi(argv[2])));
    }
}