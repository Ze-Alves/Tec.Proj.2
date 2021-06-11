#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "ll.h"
#include "LerFicheiro.h"

// Estruturas de Dados

typedef struct 
{char arma[50]; // arma de combate
 int pontuacao; // 0 a 100
} Preferencias;

typedef struct dados
{int numero;
 char nome[50];
 char obtido[50];
 Preferencias preferencias[5]; // ordem de preferências (máximo de 5)
}* Jogador;


typedef struct Armas {
	char nome[50];
	int quantidade;
	struct ListElem *obtido;


}*Arma;




// Protótipos

void show(void* data);
int igual(void* data1, void* data2);
void main();

// Implementações

void showAr(void* data) {
	Arma g = (Arma)data;
	if (g != NULL)
		printf("%d %s\n", g->quantidade, g->nome);
	if (g->obtido != NULL){
		showListIterative(g->obtido, &show);
	}
}





void ArmasTree(ListElem dados,ListElem Armas){


	//Cria na lista das Armas a lista dos Jogadores que a Mencianam nas Preferencias

	while (Armas != NULL) {
		Arma g = Armas->data;
		ListElem obt = NULL;
			 for (int i = 0; i < 5; i++) {
				 ListElem aux = dados;
				 while (aux != NULL) {
					 Jogador j = dados->data;
				if (strcmp(g->nome,j->preferencias[i].arma)==0) {
					obt = addItemHead(obt, j);
				}
				aux = aux->next;
			}
		}
		g->obtido = obt;
		Armas = Armas->next;
	}
}


/*
void ArmasTree(ListElem dados, ListElem Armas) {


	//Cria na lista das Armas a lista dos Jogadores que a Mencianam nas Preferencias

	while (Armas != NULL) {
		ListElem aux = dados;
		Arma g = Armas->data;
		ListElem obt = NULL;
		while (aux != NULL) {
			Jogador j = aux->data;
			for (int i = 0; i < 5; i++) {
				if (strcmp(g->nome, j->preferencias[i].arma) == 0) {
					obt = addItemHead(obt, j);
				}
			}
			aux = aux->next;
		}
		g->obtido = obt;
		Armas = Armas->next;
	}
}*/



void DarArma(Arma g, int n) {
	ListElem aux = g->obtido;
	while (aux != NULL)
	{
		Jogador j = aux->data;
		if (j->numero == n) {
			strcpy(j->obtido, g->nome);
			break;
		}
		aux = aux->next;
	}
}







void ObtidoAlg(ListElem Armas) {
	

	//


	int i = 0;
	int n = 0;
	while (Armas != NULL) {
		Arma g = Armas->data;
		
		while (g->quantidade > 0) {
			ListElem aux = g->obtido;
			while (aux != NULL) {
				Jogador j = aux->data;

				if (j->obtido != "") {
					if (strcmp(g->nome, j->preferencias[0].arma) == 0) {
						if (j->preferencias[0].pontuacao >= i) {
							i = j->preferencias[0].pontuacao;
							n = j->numero;
						}
					}
				}

				aux = aux->next;
			}
			DarArma(g, n);
		}
		Armas = Armas->next;

	}
	
}




int OrdenarObt(Jogador j1,Jogador j2) {





	return 0;
}
























// Se iguais devolve 0
// se número do jogador referenciado por data1 for inferior devolve -1
// caso contrário 1
int comparar(void* data1, void* data2)
{
	Jogador d1 = (Jogador)data1;
	Jogador d2 = (Jogador)data2;
	if (d1->numero < d2->numero) return(-1);
	else if (d1->numero > d2->numero) return(1);
	else return(0);
}

int compararNomes(void* data1, void* data2)
{
	Jogador d1 = (Jogador)data1;
	Jogador d2 = (Jogador)data2;
	return(strcmp(d1->nome, d2->nome));
}


// Escrita na consola do dados de um jogador
void show(void* data)
{Jogador j = (Jogador)data;
 if (j != NULL) 
  printf("%d %s ",j->numero, j->nome);
 for(int i=0;i<5;i++)
  printf("%s %d ", j->preferencias[i].arma, j->preferencias[i].pontuacao);
 printf("\n %s \n",j->obtido);
}

// Se iguais devolve 1 senão devolve 0
int igual(void* data1, void* data2)
{int* d1 = (int*) data1;
 int* d2 = (int*) data2;
 if ((d1!=NULL)&&(d2!=NULL)) return(*d1 == *d2);
 else return(0);
}





// Procedimento principal
void main()
{ListElem lista = NULL;
 Jogador j;

 ListElem Armas = NULL;
 Arma g;






 j = (Jogador) malloc(sizeof(struct dados));
 j->numero = 1;
 strcpy(j->nome,"Joao");
 strcpy(j->preferencias[0].arma,"sniper");
 j->preferencias[0].pontuacao = 87;
 strcpy(j->preferencias[1].arma,"pistola");
 j->preferencias[1].pontuacao = 67;
 strcpy(j->preferencias[2].arma,"metralhadora");
 j->preferencias[2].pontuacao = 57;
 strcpy(j->preferencias[3].arma,"-");
 j->preferencias[3].pontuacao = 0;
 strcpy(j->preferencias[4].arma,"-");
 j->preferencias[4].pontuacao = 0;
 lista = addItemHead(lista,j);

 printf("%s\n", j->obtido);



 j = (Jogador) malloc(sizeof(struct dados));
 j->numero = 2;
 strcpy(j->nome,"Maria");
 strcpy(j->preferencias[0].arma,"metralhadora");
 j->preferencias[0].pontuacao = 77;
 strcpy(j->preferencias[1].arma,"sniper");
 j->preferencias[1].pontuacao = 89;
 strcpy(j->preferencias[2].arma,"-");
 j->preferencias[2].pontuacao = 0;
 strcpy(j->preferencias[3].arma,"-");
 j->preferencias[3].pontuacao = 0;
 strcpy(j->preferencias[4].arma,"-");
 j->preferencias[4].pontuacao = 0;
 lista = addItemHead(lista, j);


 g = (Arma)malloc(sizeof(struct Armas));
 strcpy(g->nome, "sniper");
 g->quantidade = 1;
 Armas = addItemHead(Armas, g);



 g = (Arma)malloc(sizeof(struct Armas));
 strcpy(g->nome, "pistola");
 g->quantidade = 0;
 Armas = addItemHead(Armas, g);

 ArmasTree(lista, Armas);

 showListIterative(Armas, &showAr);

 // Escrita na consola do conteúdo da lista ligada
 //showListIterative(lista,&show);
 
 printf("----------------\n");
 int numero = 1;
 // Remoção da primeiro ocorrência do registo com número de jogador 2
 lista = removeItemIterative(lista,&numero,&igual);
 
 // Escrita na consola do conteúdo da lista ligada
 showListIterative(lista,&show);



 //ObtidoAlg(Armas);
}
