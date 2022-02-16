#ifndef BINARY_TREE_H
#define BINARY_TREE_H

#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <assert.h>
#include <string.h>

#define new(type) \
	((type*)malloc(sizeof(type)))

#define cnew(type, count) \
	((type*)calloc(count, sizeof(type)))

typedef struct Node {
	struct Node* lnode;
	struct Node* rnode;
	char value;
} Node;

typedef struct BinaryTree
{
	Node* root;
} Tree;

Node* node_new(int value);
Tree* tree_new(Node* root);

Node* find_max_node(Node* node, int value);

void print_tree(Tree* tree);

#endif