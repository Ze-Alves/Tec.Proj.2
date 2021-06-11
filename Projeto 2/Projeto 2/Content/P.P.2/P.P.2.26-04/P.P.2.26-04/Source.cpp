#include <stdio.h>
#include <stdlib.h>


typedef int TreeEntry;
typedef enum balancefactor { LH, EH, RH } BalanceFactor;

typedef struct treenode *TreeNode;

struct treenode {
	BalanceFactor bf;
	TreeEntry entry;
	treenode* left;
	treenode* right;
};

TreeNode insert(TreeNode apt, int i) {
	TreeNode aux;
	if (apt == NULL) {
		aux = (TreeNode)malloc(sizeof(treenode));
		if (aux != NULL) {
			aux->entry = i;
			aux->left = NULL;
			aux->right = NULL;
			return aux;
		}
		else return(apt);
	}
	else
		if (i >= apt->entry) {
			apt->right = insert(apt->right, i);
			return apt;
		}
		else {
			apt->left = insert(apt->left, i);
			return apt;
		}
}


int main() {

}