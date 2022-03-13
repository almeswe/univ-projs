#include "client.h"

int main(int argc, char** argv) {
    if (argc != 4) {
        return printf("%s", "Argument count passed is incorrect.\n"), 1;
    }
    if (strlen(argv[3]) > 8) {
        return printf("%s", "Size of name should be less than 8 bytes long.\n"), 1;
    }
    return client_start(client_init(argv[1], 
        atoi(argv[2]), argv[3])) ,0;
}