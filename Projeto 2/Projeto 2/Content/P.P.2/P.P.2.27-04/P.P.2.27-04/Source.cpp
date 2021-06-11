#include <stdio.h>
#include <stdlib.h>

#define TAM 100

typedef struct registo {
	int numero;
	float nota;
	struct registo* seguinte;
}* Colisoes;

void inicialize(Colisoes t[]) {
	int i;
	for (i = 0; i < TAM; i++)t[i] = NULL;

}

int fhash(int numero) {
	return(numero % 100);
}


int inserir(Colisoes tabela[], int num, int nt) {
	Colisoes novo = (Colisoes)malloc(sizeof(struct registo));
	if (novo != NULL) {
		int indice = fhash(num);
		novo->numero = num;
		novo->nota = nt;
		novo->seguinte = tabela[indice];
		tabela[indice] = novo;
		return 1;
	}
	else return 0;

}



int consultar(Colisoes tabela[],int numero) {
	Colisoes aux = tabela[fhash(numero)];
	int i=0;
	while (aux != NULL) {
		if (aux->numero == numero) {
			i = aux->nota;
			break;
		}
		aux = aux->seguinte;
	}
	return i;
}


void main() {
	Colisoes tabela[TAM];
	inicialize(tabela);

	inserir(tabela, 14000, 17);
	inserir(tabela, 14076, 16);
	inserir(tabela, 14330,13);
	inserir(tabela, 14003, 14);
	inserir(tabela, 14005, 11);

	printf("%d", consultar(tabela, 14070));


}