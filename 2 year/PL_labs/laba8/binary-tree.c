#include "binary-tree.h"

Node* node_new(int value)
{
	Node* node = new(Node);
	assert(node);
	node->value = value;
	node->lnode = node->rnode = NULL;
	return node;
}

Tree* tree_new(Node* root)
{
	Tree* tree = new(Tree);
	assert(tree);
	tree->root = root;
	return tree;
}

Node* find_max_node(Node* node, int value)
{
	if (!node)
		return NULL;
	if (node->lnode && node->lnode->value == value)
		return node;
	if (node->rnode && node->rnode->value == value)
		return node;
	Node* max_node;
	if (max_node = find_max_node(node->lnode, value))
		return node;
	return find_max_node(node->rnode, value);
}

void print_node(Node* node, char* indent)
{
	if (!node)
		return;
	int size = strlen(indent);
	char* new_indent = calloc(size + 3, sizeof(char));
	assert(new_indent);
	for (int i = 0; i < size; i++)
		new_indent[i] = indent[i];

	for (int i = size; i < size + 2; i++)
		new_indent[i] = ' ';
	new_indent[size + 3] = '\0';

	printf("%s%d\n", indent, node->value);
	print_node(node->lnode, new_indent);
	print_node(node->rnode, new_indent);
}

void print_tree(Tree* tree)
{
	print_node(tree->root, "");
}