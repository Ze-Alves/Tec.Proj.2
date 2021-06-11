#include <stdio.h>
#include <stdlib.h>

// Implementação de uma lista de listas ligadas

typedef struct registo2
{
	int elemento;
	struct registo2* seguinte;
}*Registo2;

typedef struct registo1
{
	int elemento;
	struct registo1* seguinte;
	struct registo2* lista;
}*Registo1;

Registo1 inserir(Registo1 inicio, int elemento)
{
	Registo1 novo = (Registo1)malloc(sizeof(struct registo1));
	if (novo != NULL)
	{
		novo->elemento = elemento;
		novo->seguinte = inicio;
		novo->lista = NULL;
		return(novo);
	}
	else return(inicio);
}


void associar(Registo1 inicio, int elemento1, int elemento2)
{
	while ((inicio != NULL) && (inicio->elemento != elemento1))
		inicio = inicio->seguinte;

	if (inicio != NULL)
	{
		Registo2 novo = (Registo2)malloc(sizeof(struct registo2));
		if (novo != NULL)
		{
			novo->elemento = elemento2;
			novo->seguinte = inicio->lista;
			inicio->lista = novo;
		}
	}
}


float media(Registo1 inicio, int elemento) {

	while ((inicio != NULL) && (inicio->elemento != elemento))
		inicio = inicio->seguinte;
	if (inicio != NULL) {
		if (inicio->lista != NULL) {
			float i = 0, j = 0;
			Registo2 aux = inicio->lista;

			while (aux != NULL) {
				i += aux->elemento;
				j++;
				aux = aux->seguinte;
			}
			return i / j;
		}
	}
	else return 0;
}

int removerUltimo(Registo1 inicio, int elemento1)
{
	while ((inicio != NULL) && (inicio->elemento != elemento1))
		inicio = inicio->seguinte;

	if (inicio != NULL)
	{
		Registo2 ant = inicio->lista;
		Registo2 act = inicio->lista;
		while ((act != NULL) && (act->seguinte != NULL))
		{
			ant = act;
			act = act->seguinte;
		}
		if (act != NULL)
		{
			ant->seguinte = NULL;
			free(act);
			if (ant == inicio->lista) inicio->lista = NULL;
			return(1);
		}
		else return(0);
	}
	else return(0);
}

void main()
{
	Registo1 dados = NULL;

	dados = inserir(dados, 10);
	dados = inserir(dados, 5);
	dados = inserir(dados, 1);
	dados = inserir(dados, 6);

	associar(dados, 6, 20);
	associar(dados, 6, 25);
	associar(dados, 6, 30);

	associar(dados, 5, 0);
	associar(dados, 5, 2);


	removerUltimo(dados, 6);

	printf("%.2f", media(dados, 6));
}






