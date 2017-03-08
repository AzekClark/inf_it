#include <stdio.h>
int main() {
	int m, n, k, t;
	scanf("%d %d", &m, &n);
	k = 1;
	t = 1;
	while (k*k < n)
	{
		if (k*k >= m) 
			t = t + 1;
		k = k + 1;
	}
	printf("%d", t);
	return 0;
}