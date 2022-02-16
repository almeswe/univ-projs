#include <stdio.h>
#include <ctype.h>
#include "sbuffer.h"
#include "binary-tree.h"

#define LEAF(node) \
	(!node->rnode && !node->lnode)

int n = 0;

void build_subtree(Node* node, int edge)
{
#define BUILD_SUBTREE(child)         \
	if (!node->child)		         \
		node->child = node_new(edge);\
	else                             \
		build_subtree(node->child, edge)

	if (node->value < edge)
		BUILD_SUBTREE(lnode);
	else
		BUILD_SUBTREE(rnode);
}

void build_tree(int count, int* edges)
{
	Node* root = node_new(edges[0]);
	Tree* tree = tree_new(root);
	
	for (int i = 1; i < count; i++)
		build_subtree(root, edges[i]);

	print_tree(tree);
}

void interact()
{
	char buffer[30];
	char* input = gets(buffer);
	int* numbers = malloc(30 * sizeof(int));
	if (!numbers)
		assert(0);
	int current = 0, n = 0;
	for (int i = 0; i < 30; i++)
	{
		if (input[i] == ' ')
			numbers[n++] = current,
				current = 0;
		else
			current *= 10,
				current = current + (input[i] - '0');
	}

	build_tree(n, numbers);
}

char* input()
{
	char* chars = NULL;

	char* buffer[50];
	char* input = gets(buffer);

	for (int i = 0; i < strlen(input); i++)
		sbuffer_add(chars, input[i]);
	return chars;
}

Node* parse_const_expr(char* input)
{
	return node_new(input[n++]);
}

Node* parse_mul_expr(char* input)
{
	Node* cnst = parse_const_expr(input);
	if (input[n] == '*' ||
		input[n] == '/')
	{
		Node* mul_expr = node_new(input[n++]);
		mul_expr->lnode = cnst;
		mul_expr->rnode = parse_const_expr(input);
		return mul_expr;
	}
	return cnst;
}

Node* parse_add_expr(char* input)
{
	Node* mul_expr = parse_mul_expr(input);
	if (input[n] == '+' ||
		input[n] == '-')
	{
		Node* add_expr = node_new(input[n++]);
		add_expr->lnode = mul_expr;
		add_expr->rnode = parse_add_expr(input);
		return add_expr;
	}
	return mul_expr;
}

Tree* parse_expr(char* input)
{
	return tree_new(parse_add_expr(input));
}

void infix(Node* node)
{
	if (LEAF(node))
		printf("%c", node->value);
	else
	{
		infix(node->lnode);
		printf("%c", node->value);
		infix(node->rnode);
	}
}

void prefix(Node* node)
{
	if (!node)
		return;
	printf("%c", node->value);
	prefix(node->lnode),
		prefix(node->rnode);
}

void postfix(Node* node)
{
	if (LEAF(node))
		printf("%c", node->value);
	else
	{
		infix(node->lnode);
		infix(node->rnode);
		printf("%c", node->value);
	}
}

int main(int argc, char** argv)
{
	Tree* tree;
	print_tree(tree = parse_expr(input()));
	printf("prefix: "); prefix(tree->root);
	printf("\n");

	printf("infix: "); infix(tree->root);
	printf("\n");

	printf("postfix: "); postfix(tree->root);
	printf("\n");

	return 1;
}