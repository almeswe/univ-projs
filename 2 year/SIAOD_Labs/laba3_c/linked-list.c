#include "linked-list.h"



LinkedList* linked_list_new()
{
	LinkedList* list = new(LinkedList);
	assert(list);
	list->head = list->last = NULL;
	return list;
}

LinkedListItem* linked_list_item_new(int* value)
{
	LinkedListItem* item = new(LinkedListItem);
	assert(item);
	item->value = value;
	item->next = NULL;
	return item;
}

void linked_list_append(LinkedList* list, int* value)
{
	LinkedListItem* item = linked_list_item_new(value);
	
	if (!list->head && !list->last)
		list->head = list->last = item;
	else
	{
		if (list->head == list->last)
			list->head->next = list->last = item;
		else
		{
			list->last->next = item;
			list->last = item;
		}
	}
}