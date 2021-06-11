#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>


int fibrec(int n) {
	if (n ==0||n==1) return n;
	return(fibrec(n - 1) + fibrec(n - 2));
}


int fibit(int n) {
	int aux = 1,res=0,m;
	for (int i = 2; i <= n; i++) {
	    m = aux;
		aux = aux + res;
		res = m;
	}
	return aux;
}


int main(void) {
	printf("%d", fibit(7));

}

