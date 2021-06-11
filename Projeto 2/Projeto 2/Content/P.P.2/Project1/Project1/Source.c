#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>




int main() {
	FILE* Ficheiro;
	int  i=0;
	int j = 0;
	char fd[32] = "asd";
	Ficheiro = fopen("testt.txt", "w+");
	if (Ficheiro == NULL)
		printf("ERROR4");
	else {
		fprintf(Ficheiro,"%d",i);
		}
	}


