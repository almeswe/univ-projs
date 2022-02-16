#include "ht.h"

//http://www.cse.yorku.ca/~oz/hash.html

Ht* ht_new()
{
	Ht* ht = (Ht*)malloc(sizeof(Ht));
	if (!ht)
		return NULL;
	ht->len = 0;
	ht->cap = HT_START_CAP;
	ht->entries = (HtEntry*)calloc(ht->cap, sizeof(HtEntry*));
	return ht;
}

void ht_rehash(Ht* ht)
{

}

uint32_t hash_key(uint32_t key) {
	key = ((key >> 16) ^ key) * 0x45d9f3b;
	key = ((key >> 16) ^ key) * 0x45d9f3b;
	key = (key >> 16) ^ key;
	return key;
}

uint32_t hash_str_key(const char* key)
{
	int c;
	uint32_t hash = 5381;
	while (c = *key++)
		hash = ((hash << 5) + hash) + c;
	return hash;
}

HtEntry* ht_entry_new(int* key, int* value)
{
	HtEntry* entry = (HtEntry*)malloc(sizeof(HtEntry));
	if (!entry)
		return NULL;
	entry->key = key;
	entry->value = value;
	entry->next = NULL;
	return entry;
}

void ht_set(Ht* ht, int* key, int* value)
{
	if (ht->len / ht->cap >= HT_LOAD_FACTOR)
		ht_rehash(ht);

	uint32_t index = hash_key(key) % ht->cap;

	if (!ht->entries[index])
		(ht->entries[index] = ht_entry_new(key, value), ht->len++);
	else
	{
		HtEntry* curr = ht->entries[index];
		HtEntry* prev = curr;
		while (curr)
		{
			if (curr->key == key)
			{
				curr->value = value;
				return;
			}
			prev = curr;
			curr = curr->next;
		}
		prev->next = ht_entry_new(key, value);
	}
}

void ht_str_set(Ht* ht, char* key, int* value)
{
	if (ht->len / ht->cap >= HT_LOAD_FACTOR)
		ht_rehash(ht);

	uint32_t index = hash_str_key(key) % ht->cap;

	if (!ht->entries[index])
		(ht->entries[index] = ht_entry_new(key, value), ht->len++);
	else
	{
		HtEntry* curr = ht->entries[index];
		HtEntry* prev = curr;
		while (curr)
		{
			if (curr->key == key)
			{
				curr->value = value;
				return;
			}
			prev = curr;
			curr = curr->next;
		}
		prev->next = ht_entry_new(key, value);
	}
}

int* ht_str_get(Ht* ht, char* key)
{
	uint32_t index = hash_str_key(key) % ht->cap;
	return !ht->entries[index] ? NULL : 
		ht->entries[index]->value;
}	

/*

ht_str_get proc

	push ebp
	mov  ebp, esp

	sub  esp, 4
	
	mov  eax, DWORD PTR [ebp+20]
	push eax
	call hash_str_key

	push edx
	mov  ebx, DWORD PTR [ebp+8]
	div  ebx
	mov  eax, edx
	pop  edx
	mov  DWORD PTR [ebp-4], eax 

	//... -> eax

	mov  esp, ebp
	pop  ebp
	ret

ht_str_get endp

*/