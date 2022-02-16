#include "binary-tree.h"

void interact(Tree* tree)
{
	char buffer[30];
	while (1)
	{
		printf(">");
		char* input = gets(buffer);
		if (strcmp(input, "dump") == 0)
			print_tree(tree);
		else if (strcmp(input, "cls") == 0)
			system("cls");
		else if (strcmp(input, "del") == 0)
		{
			int value = atoi(gets(input));
			Node* parent = find_max_node(tree->root, value);
			if (!parent)
			{
				if (value == tree->root->value)
					tree->root = NULL;
				else
					printf("No such node found!\n");
			}
			 
			{
				if (parent->lnode->value == value)
					parent->lnode = NULL;
				else
					parent->rnode = NULL;
			}
		}
	}
}

int main(int argc, char** argv)
{
	Node* root = node_new(1);
	Node* lnode = node_new(2);
	Node* rnode = node_new(3);
	root->lnode = lnode;
	root->rnode = rnode;

	Node* lnode2 = node_new(4);
	Node* rnode2 = node_new(5);

	Node* lnode3 = node_new(6);
	Node* rnode3 = node_new(7);

	root->lnode->lnode = lnode2;
	root->lnode->rnode = rnode2;

	root->rnode->lnode = lnode3;
	root->rnode->rnode = rnode3;

	Tree* tree = tree_new(root);
	interact(tree);
}