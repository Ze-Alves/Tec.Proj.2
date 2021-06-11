#include <stdio.h>
#include <stdlib.h>


typedef struct nodo {
	int elem;
	struct nodo* dir;
	struct nodo* esq;
}Node, *Tree;

Tree insert(Tree apt,int i) {
	Tree aux;
	if (apt == NULL) {
		aux = (Tree)malloc(sizeof(Node));
		if (aux != NULL) {
			aux->elem = i;
			aux->esq = NULL;
			aux->dir = NULL;
			return aux;
		}
		else return(apt);
	}
	else 
		if (i >= apt->elem) {
			apt->dir = insert(apt->dir, i);
			return apt;
		}
		else {
			apt->esq = insert(apt->esq, i);
			return apt;
		}
}

Tree inseririt(Tree t, int ele) {
	Tree novo = (Tree)malloc(sizeof(Node));
	Tree *aux=&t,aux1;
	if (novo != NULL) {
		
			novo->elem = ele;
			novo->dir = NULL;
			novo->esq = NULL;
			if (t == NULL) {
				return  novo;
		}
		else {
				aux1 = t;
				while (aux1 != NULL) {
					if (aux1->elem <= ele) { aux = &(aux1->dir); aux1 = aux1->dir;  }
					else {
						aux = &(aux1->esq); aux1 = aux1->esq;
					}
				}
				*aux=novo;

				return t;
		}
		
	}
	else return t;
}

void ListarFolhas(Tree t) {
	if (t != NULL) {
		if (t->dir == NULL && t->esq == NULL)printf("%d ", t->elem);
		ListarFolhas(t->esq);
		ListarFolhas(t->dir);
	}
}

int altura(Tree t) {
	int i, j;
	if (t != NULL){
	
	j= altura(t->esq);
	i = altura(t->dir);
	if (i > j)return i + 1;
	else return j + 1;
	}
	else return 0;
}



int existe(Tree apt, int i) {
	while (apt!=NULL)
	{
		if (i == apt->elem)return 1;
		else if (i >= apt->elem)apt = apt->dir;
		else apt = apt->esq;
	}
	return 0;
}

void Listar(Tree apt) {
	if (apt != NULL) {
		printf("(%d", apt->elem);
		Listar(apt->esq);
		Listar(apt->dir);
		printf(") ");
	}
	else printf(" vazia");
}

int menor(Tree apt) {
	int i=0;
	while (apt!=NULL)
	{
		i = apt->elem;
		apt=apt->esq;
	}
	return i;
}

int Soma(Tree apt) {
	int i = 0;

	i +=apt->elem;
	if (apt->dir != NULL)i += Soma(apt->dir);
	if (apt->esq != NULL) i+= Soma(apt->esq);
	return i;
}

int TotalElem(Tree apt) {
	if (apt == NULL)return 0;
	else return(1 + TotalElem(apt->dir) + TotalElem(apt->esq));
}

Tree Excluiri(Tree apt, int i) {
	int sub = 0;
	Tree ret = apt;
	while (i!=apt->elem)
	{
		if (i >= apt->elem)apt = apt->dir;
		else apt = apt->esq;
	}
	Tree aux = apt;
	
		if (aux->dir != NULL) {
			aux = aux->dir;
			while (aux->esq != NULL) {
				aux = aux->esq;
		}
			sub = aux->elem;
	}
		apt->elem = sub;
		aux = NULL;

	return ret;
}



Tree Excluir(Tree t) {
	Tree aux = t;
	if (aux->dir == NULL) {
		t = aux->esq;
	}
	else if (aux->esq == NULL) {
		t = aux->dir;
	}
	else {
		Tree aux2 = aux->dir;
		while (aux2->esq != NULL)
			aux2 = aux2->esq;
		aux2->esq = aux->esq;
		t = aux->dir;
	}
	free(aux);
	return t;
}

Tree remove(Tree t, int ele) {
	Tree aux = t;
	if (aux != NULL) {
		if (aux->elem == ele)
			aux = Excluir(aux);
		else if (ele < aux->elem)
			aux->esq = remove(aux->esq, ele);
		else
			aux->dir = remove(aux->dir, ele);
	}
	return aux;
}


void ListarNivel(Tree t,int n) {
	int i = n;
	if (t != NULL) {
		if (i <= 1) {
			printf("\n%d ", t->elem);
		}
		else {
			i--;
			ListarNivel(t->esq, i);
			ListarNivel(t->dir, i);
		}
	}
}

int Balanceada(Tree t) {
	int i=1,j=1;

	if (t != NULL) {
		if (t->esq != NULL)
			i = Balanceada(t->esq);
		if (t->dir != NULL)
			j = Balanceada(t->dir);

		if (i == 1 && j == 1) {
			if (altura(t->esq) - altura(t->dir) >= -1 && altura(t->esq) - altura(t->dir) <= 1) {
				return 1;
			}
			else return 0;
		}
		else return 0;
	}
	else return 1;
}

int Iguais(Tree t, Tree t2) {
	int i = 1,j=1;

	if (t != NULL && t2 != NULL) {
		i = Iguais(t->esq, t2->esq);
		j = Iguais(t->dir, t2->dir);
		if (i == 1 && j == 1) {
			if (t->elem == t2->elem) {
				return 1;
			}
			else return 0;
		}
		else return 0;
	}
	else if (t == t2)
		return 1;
	else return 0;
}

void Escrever(Tree t) {
	if (t != NULL) {
		Escrever(t->esq);
		printf("\n%d", t->elem);
		Escrever(t->dir);
	}
}


void TreeSort(int sequencia[],int tam) {
	Tree t=NULL;
	for (int i = 0; i < tam; i++) {
		t = inseririt(t, sequencia[i]);
	}
	Escrever(t);
}




int main(){
	Tree apt=NULL,abt=NULL;
	apt=insert(apt, 3);
	apt=insert(apt, 5);
	apt = insert(apt, 1);
	apt = insert(apt, 2);
	apt = insert(apt, 4);
	apt = insert(apt, 6);
	apt = inseririt(apt, 7);
	Listar(apt);
	apt = insert(apt, 8);
	printf("\n");
	
	abt = insert(abt, 3);
	abt = insert(abt, 5);
	abt = insert(abt, 1);
	abt = insert(abt, 2);
	abt = insert(abt, 4);
	abt = insert(abt, 6);
	abt= inseririt(abt, 7);
	abt = insert(abt, 8);
	
	Listar(apt);
	printf("%d", menor(apt));
	printf("\n%djj\n", altura(apt));
	printf(" %d\n", TotalElem(apt));
	ListarFolhas(apt);
	printf("%d",Balanceada(apt));
	printf("\n %d", Iguais(apt, abt));

	int seq[8] = { 3,9,1,5,2,12,0,3 };
	TreeSort(seq, 8);


}