#ifndef LINKED_LIST_H
#define LINKED_LIST_H

#include <stdlib.h>
#include <string.h>
#include <assert.h>

#define new(type) \
	(type*)malloc(sizeof(type))

typedef struct LinkedListItem 
	LinkedListItem;

typedef struct LinkedListItem {
	int* value;
	LinkedListItem* next;
} LinkedListItem;

typedef struct LinkedList {
	LinkedListItem* head;
	LinkedListItem* last;
} LinkedList;

LinkedList* linked_list_new();
LinkedListItem* linked_list_item_new(int* value);
void linked_list_append(LinkedList* list, int* value);

#endif