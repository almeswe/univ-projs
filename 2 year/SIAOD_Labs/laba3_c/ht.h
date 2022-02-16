#ifndef HASH_TABLE_H
#define HASH_TABLE_H

#include <stdint.h>
#include <stdlib.h>

#define HT_START_CAP 16
#define HT_LOAD_FACTOR 0.75

typedef struct HtEntry HtEntry;

typedef struct HtEntry
{
	int* key;
	int* value;
	HtEntry* next;
} HtEntry;

typedef struct Ht
{
	uint32_t cap;
	uint32_t len;
	HtEntry** entries;
} Ht;

Ht* ht_new();
HtEntry* ht_entry_new(int* key, int* value);

void ht_set(Ht* ht, int* key, int* value);
void ht_str_set(Ht* ht, char* key, int* value);
int* ht_str_get(Ht* ht, char* key);

#endif