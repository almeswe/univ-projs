#include <stdio.h>
#include <stdint.h>
#include <math.h>
#include <assert.h>

int input(const char* msg)
{
	char s[20];
	printf("%s\n", msg);
	char* input = gets(s);
	int num_input = 0;
	for (int i = 0; i < strlen(input); i++)
		num_input = num_input * 10 + (input[i] - '0');
	return num_input;
}


long long factorial(int x)
{
	if (x == 0)
		return 1;
	long long fact = 1;
	for (int i = 1; i <= x; i++)
		fact *= i;
	return fact;
}

int main(int argc, char** argv)
{
	//1
	{
		int x, y, z;
		x = input("Enter x");
		y = input("Enter y");
		z = (x > y) ? (x - y) : (y - x + 1);
		printf("z = %d\n\n", z);
	}

	//7
	{
		int x, fx;
		x = input("Enter x");
		fx = (-2 <= x && x > 2) ? (x * x) : (4);
		printf("f(x) = %d\n\n", fx);
	}

	//14
	{
		int x;
		double z;

		x = input("Enter x");
		z = x;
		for (int fact = 3; fact <= 13; fact += 2)
			z += -(pow(x, fact) / factorial(fact));
		printf("factorial order = %f\n\n", z);
	}

	//23
	{
		int curr;
		int count = 0;
		int numbers[50];

		while (count < 50)
		{
			curr = input("Enter number (max-50, 9999-exit)");
			if (curr != 9999)
				numbers[count] = curr;
			else
				break;
			count++;
		}

		int psum = 0, ncount = 0;
		for (int i = 0; i < count; i++)
			(numbers[i] >= 0) ?
				psum += numbers[i] : ncount++;
		printf("a) sum of all positive nums: %d\n", psum);
		printf("b) count of negative nums: %d\n", ncount);
	}

	return 0;
}