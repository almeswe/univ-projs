#include <stdio.h>
#include <string.h>
#include <stdbool.h>

#include <errno.h>
#include <signal.h>
#include <unistd.h>
#include <libgen.h>
#include <sys/wait.h>
#include <sys/time.h>

#include "xmemory.h"

static int id = 0;
static char* module = NULL;

static pid_t root = 0;
static pid_t cpgid = 0;

static int sigusr1_sent = 0;
static int sigusr2_sent = 0;
static int sigusr1_recv = 0;
static int sigusr2_recv = 0;

static int wstatus = 0;
static bool is_in_action = true;

static struct timeval te;

#define make_sa(name, handler)  \
    struct sigaction name;      \
    name.sa_handler = &handler; \
    sigemptyset(&name.sa_mask); \
    name.sa_flags = SA_RESTART;

#define printcd(action, sig)                    \
    gettimeofday(&te, NULL);                    \
    printf("%d %d %d %s %s %ld\n", id, getpid(), \
        getppid(), #action, #sig, te.tv_usec);

#define bind_handler(n, name, sig, handler)  \
    make_sa(name, handler);                  \
    id = n, sigaction(sig, &name, NULL);                        

void perrorf(const char* message) {
    char* pure_module_name = basename(module);
    char* buffer = (char*)xcalloc(1,
        strlen(message) + strlen(pure_module_name) + 3 + 64);
    sprintf(buffer, "%d %s: %s", getpid(), pure_module_name, message);
    perror(buffer), free(buffer);
}

int killf(pid_t pid, int sig) {
    int res = kill(pid, sig);
    if (res < 0) {
        perrorf("kill");
    }
    return res;
}

pid_t forkf() {
    pid_t pid;
    if ((pid = fork()) < 0) {
        perrorf("forkf()");
        killf(root, SIGTERM);
    }
    return pid;
}

void process_func() {
    while (is_in_action) {
        pause();
    }
    exit(0);
}

void interrupt_handler(int sig) {
    printf("%d %d exited after %d SIGUSR1 and %d SIGUSR2\n",
        getpid(), getppid(), sigusr1_sent, sigusr2_sent);
    is_in_action = false;
}

void interrupt_handler6(int sig) {
    killf(-cpgid, SIGTERM);
    interrupt_handler(SIGTERM);
}

void p1_handler(int sig) {
    printcd(recv, USR2);
    if (++sigusr2_recv > 101) {
        killf(-cpgid, SIGTERM);
        interrupt_handler(SIGTERM);
    } else {
        printcd(sent, USR2);
        printcd(sent, USR2);
        printcd(sent, USR2);
        printcd(sent, USR2);
        printcd(sent, USR2);
        if (killf(-cpgid, SIGUSR2) == 0) {
            sigusr2_sent += 5;
        }
    }
}

void p2_handler(int sig) {
    sigusr2_recv += 1;
    printcd(recv, USR2);
}

void p3_handler(int sig) {
    sigusr2_recv += 1;
    printcd(recv, USR2);
}

void p4_handler(int sig) {
    sigusr2_recv += 1;
    printcd(recv, USR2);
}

void p5_handler(int sig) {
    sigusr2_recv += 1;
    printcd(recv, USR2);
}

void p6_handler(int sig) {
    sigusr2_recv += 1;
    printcd(recv, USR2);
    printcd(sent, USR1);
    printcd(sent, USR1);
    if (killf(-cpgid, SIGUSR1) == 0) {
        sigusr1_sent += 2;
    }
}

void p7_handler(int sig) {
    sigusr1_recv += 1;
    printcd(recv, USR1);
}

void p8_handler(int sig) {
    sigusr1_recv += 1;
    printcd(recv, USR1);
    printcd(sent, USR2);
    //printf("%d %d %d recv USR1\n", id, getpid(), getppid());
    //printf("%d %d %d sent USR2\n", id, getpid(), getppid());
    if (killf(root, SIGUSR2) == 0) {
        sigusr2_sent += 1;
    }
}

void init_process_tree() {
    //printf(".... -> %d(0)\n", getpid());
    if ((root = forkf()) == 0) {
        root = getpid();
        bind_handler(1, sa_p1, SIGUSR2, p1_handler);
        bind_handler(1, sa_p1_int, SIGTERM, interrupt_handler);
        if ((cpgid = forkf()) == 0) {
            setpgid(getpid(), cpgid);        
            bind_handler(2, sa_p2, SIGUSR2, p2_handler);
            bind_handler(2, sa_p2_int, SIGTERM, interrupt_handler);
            if (forkf() == 0) {
                bind_handler(4, sa_p4, SIGUSR2, p4_handler);
                bind_handler(4, sa_p4_int, SIGTERM, interrupt_handler);
                process_func();
            } else {
                if (forkf() == 0) {
                    bind_handler(5, sa_p5, SIGUSR2, p5_handler);
                    bind_handler(5, sa_p5_int, SIGTERM, interrupt_handler);
                    if (forkf() == 0) {
                        bind_handler(6, sa_p6, SIGUSR2, p6_handler);
                        bind_handler(6, sa_p6_int, SIGTERM, interrupt_handler6);
                        if ((cpgid = forkf()) == 0) {
                            setpgid(getpid(), cpgid);
                            bind_handler(7, sa_p7, SIGUSR1, p7_handler);
                            bind_handler(7, sa_p7_int, SIGTERM, interrupt_handler);
                            process_func();
                        } else {
                            if (forkf() == 0) {
                                setpgid(getpid(), cpgid);
                                bind_handler(8, sa_p8, SIGUSR1, p8_handler);
                                bind_handler(8, sa_p8_int, SIGTERM, interrupt_handler);
                                killf(root, SIGUSR2);
                                process_func();
                            }
                        }
                        process_func();
                    }
                    process_func();
                }
            }
            process_func();
        } else {
            if (forkf() == 0) {
                setpgid(getpid(), cpgid);        
                bind_handler(3, sa_p3, SIGUSR2, p3_handler);
                bind_handler(3, sa_p3_int, SIGTERM, interrupt_handler);
                process_func();
            }
        }
        process_func();
    }
    else {
        waitpid(root, &wstatus, 0);
    }
}

int main(int argc, char** argv) {
    module = basename(argv[0]);
    if (argc != 1) {
        printerr("%s: %s\n", module, "Incorrect argument count passed!");
        return 1;
    }
    else {
        init_process_tree();
    }
    return 0;
}