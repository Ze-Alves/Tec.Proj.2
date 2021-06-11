#include <stdio.h>
#include <stdlib.h>

// Pretende-se definir uma lista ligada de inteiros

typedef struct nodo {
    int valor;
    struct nodo* seguinte;
} Nodo;

Nodo* inserirElementoInicio(Nodo* inicio, int elemento)
{
    Nodo* aux = (Nodo*)malloc(sizeof(Nodo));
    if (aux != NULL)
    {
        aux->valor = elemento;
        aux->seguinte = inicio;
        return(aux);
    }
    else return(inicio);
}

// Listar na consola o conteúdo da lista ligada (versão recursiva)
void listarRecursiva(Nodo* inicio)
{
    if (inicio == NULL) printf("Lista ligada vazia\n");
    else {
        if (inicio->seguinte != NULL)
        {
            printf("%d\n", inicio->valor);
            listarRecursiva(inicio->seguinte);
        }
        else printf("%d\n", inicio->valor);
    }
}

// Listar na consola o conteúdo da lista ligada (versão recursiva)
void listarRecursivaB(Nodo* inicio)
{
    if (inicio != NULL) {
        printf("%d\n", inicio->valor);
        listarRecursivaB(inicio->seguinte);
    }
}

// Listar na consola o conteúdo da lista ligada (versão iterativa)
void listarIterativa(Nodo* inicio)
{
    Nodo* aux = inicio;
    while (aux != NULL)
    {
        printf("%d\n", aux->valor);
        aux = aux->seguinte;
    }
}

void ListarInversa(Nodo* lista) {
    if (lista != NULL) {
        ListarInversa(lista->seguinte);
        printf("%d\n", lista->valor);
    }
}

int quantidadeIterativa(Nodo* inicio)
{
    int i = 0;
    while (inicio != NULL)
    {
        i++;
        inicio = inicio->seguinte;
    }
    return i;
}
void J() {
    return;
}



Nodo* InverterLista(Nodo* lista) {
    Nodo* ListaInvertida=NULL;
    while (lista != NULL) {
        ListaInvertida=inserirElementoInicio(ListaInvertida, lista->valor);
        lista = lista->seguinte;
    }
    return ListaInvertida;
}


Nodo* InserirFinal(Nodo* lis, int ele) {
    Nodo* auxi = (Nodo*)malloc(sizeof(Nodo));
    Nodo* aux = lis;
    if (auxi != NULL) {
        auxi->seguinte = NULL;
        auxi->valor = ele;
        if (lis != NULL) {
            while (lis->seguinte != NULL) {
                lis = lis->seguinte;
            }
            lis->seguinte = auxi;
        }
        else
        {
            aux = auxi;
        }
        return aux;
    }
}

Nodo* inserirPosicao(Nodo* inicio, int elemento, int posicao) {
    Nodo* aux = (Nodo*)malloc(sizeof(Nodo));
    Nodo* lista = inicio;
    Nodo* elementoAnt = NULL;
    if ((posicao <= (quantidadeIterativa(lista) + 1)) && (posicao > 0)) {
        for (int i = 1; i < posicao; i++) {
            elementoAnt = lista;
            lista = lista->seguinte;
        }
        if (elementoAnt != NULL) elementoAnt->seguinte = aux;
        aux->valor = elemento;
        aux->seguinte = lista;
    }
    if (posicao != 1) return(inicio);
    else return (aux);
}




Nodo* InserirMeio(Nodo* lis, int ele, int pos) {
    if (pos > quantidadeIterativa(lis)) {
        InserirFinal(lis, ele);
        return(lis);
    }

    Nodo* auxp = lis;
    Nodo* auxi = (Nodo*)malloc(sizeof(Nodo));
    if (pos == 1) {
        auxi->valor = ele;
        auxi->seguinte = auxp;
        return (auxi);
    }
    for (int i = 2; i < pos; i++) {
        auxp = auxp->seguinte;
    }
    Nodo* aux2;
    aux2 = auxp->seguinte;
    auxp->seguinte = auxi;
    auxi->seguinte = aux2;
    auxi->valor = ele;
    return(lis);
}



Nodo* JunçaoListas(Nodo* lis1, Nodo* lis2) {
    Nodo* aux2 = lis2;
    Nodo* ListaJunta = NULL;
    Nodo* ListaInvAux = InverterLista(lis1);

    while (ListaInvAux != NULL) {
        ListaJunta=inserirElementoInicio(ListaJunta, ListaInvAux->valor);
        ListaInvAux = ListaInvAux->seguinte;
    }
    listarIterativa(ListaJunta);
    Nodo* aux = ListaJunta;

    int i = 1;
    if (aux2->valor < ListaJunta->valor) {
        int j = aux2->valor;
        aux2->valor = ListaJunta->valor;
        ListaJunta->valor = j;
    }

    while (aux2 != NULL) {
        while (ListaJunta->seguinte != NULL) {
            i++;
            if (aux2->valor < ListaJunta->seguinte->valor) {
                InserirMeio(aux, aux2->valor, i);
                i--;
                break;
            }
            ListaJunta = ListaJunta->seguinte;

        }
        if (ListaJunta->seguinte == NULL) {
            InserirFinal(aux, aux2->valor);
        }
        aux2 = aux2->seguinte;
    }
    return aux;
}

Nodo* JunçaoListas2(Nodo* lis1, Nodo* lis2) {

    Nodo* ListaJunta = NULL;

    while (lis1 != NULL && lis2 != NULL) {
        if (lis1->valor <= lis2->valor) {
            ListaJunta=InserirFinal(ListaJunta, lis1->valor);
            lis1 = lis1->seguinte;
        }
        else {
            ListaJunta=InserirFinal(ListaJunta, lis2->valor);
            lis2 = lis2->seguinte;
        }
    }

        while (lis2 != NULL) {
            InserirFinal(ListaJunta, lis2->valor);
            lis2 = lis2->seguinte;
        }
    
        while (lis1 != NULL) {
            InserirFinal(ListaJunta, lis1->valor);
            lis1 = lis1->seguinte;
        }

    return ListaJunta;
}





int MaiorElem(Nodo* Lista) {
    int i=0;

    i = Lista->valor;
    while (Lista != NULL) {
        if (Lista->valor > i)i = Lista->valor;
        Lista = Lista->seguinte;
    }
    return i;
}



void AdicionarLista(Nodo* lis, int val) {
    while (lis != NULL) {
        lis->valor += val;
        lis = lis->seguinte;
    }
}

int AdicionarSomat(Nodo* lis) {
    if (lis != NULL) {
        lis->valor += (AdicionarSomat(lis->seguinte));
        return lis->valor;
    }
    else return 0;
}


// Eliminar todos os registos de uma lista ligada passada por parâmetro
void eliminar(Nodo** apt)
{
    Nodo* aux = *apt, * proximo;
    while (aux != NULL)
    {
        proximo = aux->seguinte;
        free(aux);
        aux = proximo;
    }

    *apt = NULL; // permite guardar o endereço NULL na variável local lista definido no procedimento main
}

// Eliminar recursiva
void eliminarRecursiva(Nodo **inicio)
{
    if (*inicio != NULL) {
        *inicio = (*inicio)->seguinte;
        eliminarRecursiva(inicio);
        free(*inicio);
    }
    else *inicio = NULL;
}

// Devolve a quantidade de inteiros (versão iterativa)


// Devolve a quantidade de inteiros (versão recursiva)
int quantidadeRecursiva(Nodo* inicio)
{
    int i = 0;
    if (inicio != NULL) {
        i = 1;
        i += quantidadeRecursiva(inicio->seguinte);
    }
    return i;
}

int qunatRec2(Nodo* inicio) {
    if (inicio != NULL)
        return (1 + qunatRec2(inicio->seguinte));
    else return 0;

}

int somatorioIterativa(Nodo* inicio)
{
    int i = 0;
    while (inicio != NULL)
    {
        i+=inicio->valor;
        inicio = inicio->seguinte;
    }
    return i;
}


int somatorioRecursiva(Nodo* inicio)
{
    int i=0;
    if (inicio != NULL) {
         i = inicio->valor;

        i += somatorioRecursiva(inicio->seguinte);
    }
    return i;
}

int somatRec2(Nodo* inicio) {
    if (inicio != NULL)
        return (inicio->valor + qunatRec2(inicio->seguinte));
    else return 0;

}






int main()
{
    Nodo* lista = NULL; // criar uma lista ligada vazia
    Nodo* lista2 = NULL;
    Nodo* ListaInversa=NULL;
    Nodo* ListaJunta;

    lista = inserirElementoInicio(lista, 8);
    lista = inserirElementoInicio(lista, 6);
    lista = inserirElementoInicio(lista, 4);
    lista = inserirElementoInicio(lista, 3);
    InserirFinal(lista, 9);
    /*ista=InserirMeio(lista, 23, 3);
    lista= inserirPosicao(lista, 6, 1);
    InserirFinal(lista, 3);
    */
    ListaInversa = InverterLista(lista);
    listarRecursiva(lista);
    lista2 = inserirElementoInicio(lista2, 16);
    lista2 = inserirElementoInicio(lista2, 10);
    lista2 = inserirElementoInicio(lista2, 1);
    lista2 = inserirElementoInicio(lista2, 1);


    printf("_________________________________________\n");
    listarIterativa(lista2);
    printf("----------------------\n");
    
    //listarRecursiva(ListaJunta);
    //AdicionarLista(lista, 3);
    listarIterativa(lista);
    printf("----------------------\n");
    ListaJunta = JunçaoListas2(lista, lista2);
    //AdicionarSomat(lista);
    listarIterativa(ListaJunta);

    printf("----------------------\n");

    listarIterativa(lista);

    printf(" %d \n %d \n %d \n %d \n %d \n t %d \n", somatorioIterativa(lista), somatorioRecursiva(lista), 
        quantidadeIterativa(lista), quantidadeRecursiva(lista),qunatRec2(lista),MaiorElem(lista));
    
    eliminarRecursiva(&lista);
    listarRecursiva(lista);

    /*
    int a = 10;
    int* b = NULL;
    b = &a; // & -> endereço de memória de uma variável
    *b = 11; // b* permite aceder à variável apontada pelo conteúdo de b e atribuir o valor 11
    printf("valor de a: %d\n", a);
    printf("Conteúdo de b: %p\n", b); // Escrita de um endereço de memória numa base hexadecimal
    */



}