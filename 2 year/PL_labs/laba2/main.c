#include <stdio.h>
#include <stdlib.h>
#define SIZE 5

#define RAND_MATRIX(matrix, size)			 \
	for (int i = 0; i < size; i++)           \
		for (int j = 0; j < size; j++)		 \
			matrix[i][j] = rand() % 9 + 1;

void task1()
{
	//16
	int matrix[SIZE][SIZE];

	RAND_MATRIX(matrix, SIZE)

	for (int i = 0; i < SIZE; i++)
	{
		for (int j = 0; j < SIZE; j++)
		{
			if (matrix[i][j] % 2 == 0)
			{
				for (int z = j + 1; z < SIZE; z++)
				{
					if (matrix[i][j] < matrix[i][z] && matrix[i][z] % 2 == 0)
					{
						int buf = matrix[i][j];
						matrix[i][j] = matrix[i][z];
						matrix[i][z] = buf;
					}
				}
			}
			else 
			{
				for (int z = j + 1; z < SIZE; z++)
				{
					if (matrix[i][j] > matrix[i][z] && matrix[i][z] % 2 == 1)
					{
						int buf = matrix[i][j];
						matrix[i][j] = matrix[i][z];
						matrix[i][z] = buf;
					}
				}
			}
		}
	}
}

void task2()
{
	int matrix[SIZE][SIZE];
	RAND_MATRIX(matrix, SIZE);

	int max_dup_diag = INT_MIN;
	int max_main_diag = INT_MIN;

	for (int i = 0; i < SIZE; i++)
		max_main_diag = max_main_diag < matrix[i][i] ?
			matrix[i][i] : max_main_diag;

	for (int i = SIZE - 1; i >= 0; i--)
		max_dup_diag = max_dup_diag < matrix[i][SIZE - 1 - i] ?
			matrix[i][SIZE - 1 - i] : max_dup_diag;

	matrix[SIZE / 2][SIZE / 2] = max(max_dup_diag, max_main_diag);
}

void task3()
{
	int matrix[SIZE][SIZE];
	RAND_MATRIX(matrix, SIZE);

	{
		//3.2
		int i = 0 , j;
		int min_elem = INT_MAX;
		while (i <= SIZE / 2)
		{
			for (int j = i; j < SIZE - i; j++)
				min_elem = min(min_elem, matrix[i][j]);
			i++;
		}
		printf("3.2: %d\n", min_elem);
	}

	{
		//3.1
		int i, j = 0;
		int min_elem = INT_MAX;

		while (j <= SIZE / 2)
		{
			for (int i = j; i < SIZE - j; i++)
				min_elem = min(min_elem, matrix[i][j]);
			j++;
		}
		j = SIZE - 1;
		while (j >= SIZE / 2)
		{
			for (int i = (SIZE - 1) - j; i <= j; i++)
				min_elem = min(min_elem, matrix[i][j]);
			j--;
		}
		printf("3.1: %d", min_elem);
	}
}

int main(int argc, char** argv)
{
	task3();
	return 0;
}