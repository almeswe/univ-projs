#include <stdio.h>
#include <ctype.h>
#include <string.h>
#include <stdint.h>
#include <stdlib.h>

#define uchar unsigned char

#define ctn(c)                      ((c) - '0')
#define create_buf(buf, size)       char buf[size]; memset(buf, 0, size)

uint64_t get_number_from_str(const char* str) {
    ushort ch; 
    uint64_t sum = 0;
    for (int i = 0; str[i]; i+=2) {
        ch = 0;
        if (str[i] != ' ') {
            ch += (uchar)str[i];
            ch <<= 8;
            ch += (uchar)str[i+1];
            sum += (ulong)ch * (ulong)ch;
            printf("%x\n", ch);
        } 
    }
    return sum;
}

uint32_t get_number_from_nstr(const char* nstr) {
    uint64_t sum = 0;
    for (int i = 0; nstr[i]; i++)
        if (nstr[i] != ' ') {
            if (!isdigit(nstr[i]))
                printf("Cannot validate the K argument.\n"), exit(1);       
            sum = (sum * 10) + ctn(nstr[i]);
        }
    return sum;
}

uint64_t get_number_from_date(const char* date) {
    uint32_t day, month, year;
    sscanf(date, "%u.%u.%u", &day, &month, &year);
    return day + month * 30 + year * 365;
}

uint32_t get_medium_number(uint64_t primary) {
    create_buf(buf, 32);
    sprintf(buf, "%lu", primary);
    
    uint32_t number = 0;
    int index = strlen(buf) / 2;

    for (int i = index-1; i <= index+1; i++)
        number = (number * 10) + ctn(buf[i]);
    return number;
}

int main(int argc, char** argv) {
    if (argc != 6)
        printf("Argument count is not valid.\n"), exit(1);

    uint64_t from_name = get_number_from_str(argv[1]) + 
        get_number_from_str(argv[2]) + get_number_from_str(argv[3]);

    uint64_t from_date = get_number_from_date(argv[4]);
    uint32_t k_arg     = get_number_from_nstr(argv[5]);

    printf("s0  = %lu\n", from_date);
    printf("s1  = %lu\n", from_name);
    printf("s1 * s0 = %lu\n", from_name);
    printf("mod = %u\n", get_medium_number(from_name * from_date));

    printf("variant: %d\n", get_medium_number(
        from_name * from_date) % k_arg + 1);
    return 0;
}