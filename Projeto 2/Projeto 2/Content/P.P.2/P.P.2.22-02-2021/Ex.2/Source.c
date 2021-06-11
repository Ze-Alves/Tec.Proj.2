#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>


int rem(int x, int y) {
	return(x % y);
}

int gcd(int x, int y) {

	if(y==0||x==0)
	return x;
	if(x>=y && y>0)
	return (gcd(y, rem(x, y)));
	if (x <= y && x > 0)
	return (gcd(x, rem(x, y)));
}



int main(void) {

	printf("%d", gcd(6, 12));

}

