#include <stdio.h>
#include <stdlib.h>

 typedef int Conteudo;

 typedef struct nodo* Lista;

 typedef struct nodo{
 Conteudo valor;
 Lista Seguinte;
} Nodo;

 Nodo* ColIn(Nodo* i, int j) {

	 Nodo* s = (Nodo*)malloc(sizeof(Nodo));
	 s->valor=j;
	 s->Seguinte = i;
	 return s;
 }

 int main() {
	 nodo i;
	 i.valor = 2;
	 
	 Nodo s;
	 s.valor = 5;
	 s.Seguinte = &i;
	 Nodo* a,*as;
	 as = (Nodo*)malloc(sizeof(Nodo));
	 as->valor = 3;
	 a = (Nodo*)malloc(sizeof(Nodo));
	 a->Seguinte = as;
	 as->Seguinte = &s;
	 printf("%d ", a->Seguinte->Seguinte->Seguinte->valor);
 }

 lista
	 lista = 0


	 lista - 1     1 = 1
	 lista = 1 1 = 0


	 lista - 0 - 1   0 = 2
	 lista = 2 0 = 1 1 = 0


	 lista - 0 - 1 - 2    2 = 4
	 lista = 2 0 = 1 1 = 4 2 = 0

	Nodo *g;
 g = lista;
for (int = 0; i < 2; i++) {
	g = g->seguinte;
}
//g=1 aux=2
Nodo* aux;
aux->segunite = g->seguinte;
//1=0 2=0
g->seguinte = aux;
//1=0 2=4




while (g != NULL){
g->valor;
g = g->seguinte;
}


	


