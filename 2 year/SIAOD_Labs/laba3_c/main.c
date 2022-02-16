#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>

#include "ht.h"
#include "linked-list.h"

#define strings_equ(str1, str2) \
	(strcmp(str1, str2) == 0)

typedef struct Term Term;

typedef struct Term {
	char* value;
	Term* subterm;
} Term;

Term* term_new(char* value, Term* subterm)
{
	Term* term = new(Term);
	assert(term);
	term->value = value;
	term->subterm = subterm;
	return term;
}

void term_dump(Term* term, char* indent)
{
	if (!term)
		return free(indent);
	char* new_indent = (char*)malloc(
		sizeof(char) * (strlen(indent) + 4));
	assert(new_indent);
	strcpy(new_indent, indent);
	free(indent);
	printf("%s%s\n", new_indent, term->value);
	term_dump(term->subterm, strcat(new_indent, "   "));
}

void ht_entry_dump(HtEntry* entry)
{
	printf("%s\n", (char*)entry->key);
	LinkedListItem* item = ((LinkedList*)
		(entry->value))->head;
	while (item)
	{
		term_dump((Term*)(item->value),
			_strdup("   "));
		item = item->next;
	}
}

void ht_dump(Ht* ht)
{
	for (size_t i = 0; i < ht->cap; i++)
		if (ht->entries[i])
			ht_entry_dump(ht->entries[i]);
}

char* input(const char* message)
{
	char* buffer = malloc(256);
	printf(message);
	gets(buffer);
	return buffer;
}

void search(Ht* ht, char* term_name)
{
	for (size_t i = 0; i < ht->cap; i++)
	{
		if (ht->entries[i])
		{
			LinkedListItem* item = ((LinkedList*)
				(ht->entries[i]->value))->head;
			while (item)
			{
				Term* term = ((Term*)(item->value));
				while (term)
				{
					if (strings_equ(term->value, term_name))
					{
						ht_entry_dump(ht->entries[i]);
						break;
					}
					term = term->subterm;
				}
				item = item->next;
			}
		}
	}
}

void delete_page(Ht* ht, char* page_name)
{
	for (size_t i = 0; i < ht->cap; i++)
		if (ht->entries[i])
			if (strings_equ(ht->entries[i]->key, page_name))
				ht->entries[i] = NULL;
}

void delete_term(Ht* ht, char* term_name)
{
	for (size_t i = 0; i < ht->cap; i++)
	{
		if (ht->entries[i])
		{
			LinkedListItem* item = ((LinkedList*)
				(ht->entries[i]->value))->head;
			while (item)
			{
				Term* term = ((Term*)(item->value));
				Term* prev = term;
				while (term)
				{
					if (strings_equ(term->value, term_name))
					{
						if (prev == term)
							item->value = NULL;
						else 
							prev->subterm = NULL;
						break;
					}
					prev = term;
					term = term->subterm;
				}
				item = item->next;
			}
		}
	}
}

void add_page(Ht* ht, char* page_name)
{
	ht_str_set(ht, page_name, linked_list_new());
}

Term* add_term(LinkedList* list, char* term_name)
{
	LinkedListItem* item = list->head;
	while (item)
	{
		Term* term = ((Term*)(item->value));
		while (term)
		{
			if (strings_equ(term->value, term_name))
				return term;
			term = term->subterm;
		}
		item = item->next;
	}
	linked_list_append(list, term_new(term_name, NULL));
	return (Term*)(list->last->value);
}

Term* find_term(LinkedList* list, char* term_name);

Term* add_subterm(LinkedList* list, Term* term, char* term_name)
{
	return term->subterm = term_new(term_name, NULL);
}

Term* find_term(LinkedList* list, char* term_name)
{
	LinkedListItem* item = list->head;
	while (item)
	{
		Term* term = ((Term*)(item->value));
		while (term)
		{
			if (strings_equ(term->value, term_name))
				return term;
			term = term->subterm;
		}
		item = item->next;
	}
	return NULL;
}

LinkedList* find_page(Ht* ht, char* page_name)
{
	for (size_t i = 0; i < ht->cap; i++)
		if (ht->entries[i])
			if (strings_equ(ht->entries[i]->key, page_name))
				return (LinkedList*)(ht->entries[i]->value);
	return NULL;
}

static int myCompare(const void* a, const void* b)
{
	return strcmp(*(const char**)a, *(const char**)b);
}

void sort_terms(Ht* ht)
{
	Term terms[20];
	size_t size = 0;

	for (size_t i = 0; i < ht->cap; i++)
	{
		if (ht->entries[i])
		{
			LinkedListItem* item = ((LinkedList*)
				(ht->entries[i]->value))->head;
			while (item)
			{
				Term term = *((Term*)(item->value));
				terms[size++] = term;
				item = item->next;
			}
		}
	}

	for (size_t i = 0; i < size; i++)
	{
		for (size_t j = i + 1; j < size; j++)
		{
			if (strcmp(terms[i].value, terms[j].value) > 0)
			{
				Term buffer = terms[i];
				terms[i] = terms[j];
				terms[j] = buffer;
			}
		}
	}

	for (size_t i = 0; i < size; i++)
		term_dump(&terms[i], _strdup(""));
}	

void sort_pages(Ht* ht)
{
	int size = 0;
	HtEntry* entries[16] = { 0 };
	for (size_t i = 0; i < ht->cap; i++)
		if (ht->entries[i])
			entries[size++] = ht->entries[i];
	for (size_t i = 0; i < ht->cap; i++)
	{
		for (size_t j = i + 1; j < ht->cap; j++)
		{
			if (entries[i] && entries[j])
			{
				if (atoi(entries[i]->key) > atoi(entries[j]->key))
				{
					HtEntry* buffer = entries[i];
					entries[i] = entries[j];
					entries[j] = buffer;
				}
			}
		}
	}
	for (size_t i = 0; i < ht->cap; i++)
		if (ht->entries[i])
			ht_entry_dump(ht->entries[i]);
}

void interact(Ht* ht)
{
	while (1)
	{
		char* c = input(">");
		if (strings_equ(c, "dump"))
			ht_dump(ht);
		else if (strings_equ(c, "cls"))
			system("cls");
		else if (strings_equ(c, "search"))
		{
			char* name = input("Enter term/subterm name: ");
			search(ht, name);
			free(name);
		}
		else if (strings_equ(c, "dpage"))
		{
			char* name = input("Enter page for deleting: ");
			delete_page(ht, name);
			free(name);
		}
		else if (strings_equ(c, "dterm"))
		{
			char* name = input("Enter term/subterm for deleting: ");
			delete_term(ht, name);
			free(name);
		}
		else if (strings_equ(c, "addpage"))
			add_page(ht, input("Enter page name: "));
		else if (strings_equ(c, "addterm"))
		{
			char* name = input("Enter page where you wanna store term: ");
			LinkedList* page;
			if (page = find_page(ht, name))
			{
				free(name);
				name = input("Enter term which you wanna add: ");
				Term* term = find_term(page, name);
				if (!term)
					term = add_term(page, name);
				while (!strings_equ(name = input("Enter term which you wanna add: "), "$stop"))
					term = add_subterm(page, term, name);
			}
			else
				free(name);
		}
		else if (strings_equ(c, "sortpages"))
			sort_pages(ht);
		else if (strings_equ(c, "sortterms"))
			sort_terms(ht);
		free(c);
	}
}

int main(int argc, char** argv)
{
	Ht* ht = ht_new();

	Term* term = term_new("term1", 
		term_new("term1.1", term_new("term1.2", NULL)));
	Term* sterm = term_new("term2",
		term_new("term2.1", term_new("term2.2", NULL)));
	
	LinkedList* list = linked_list_new();
	linked_list_append(list, term);
	linked_list_append(list, sterm);
	ht_str_set(ht, "2", list);

	term = term_new("term3",
		term_new("tern3.1", term_new("term3.2", NULL)));
	sterm = term_new("term4",
		term_new("term4.1", term_new("term4.2", NULL)));

	list = linked_list_new();
	linked_list_append(list, term);
	linked_list_append(list, sterm);
	ht_str_set(ht, "1", list);

	interact(ht);

	return 1;
}