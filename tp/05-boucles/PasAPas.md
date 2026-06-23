# Pas à pas — TP 5 : Boucles

Relis le cours [05-boucles.md](../../docs/05-boucles.md) avant de commencer. Place-toi dans `tp/05-boucles/Enonce/`. C'est un TP avec beaucoup de questions indépendantes : avance une par une, en testant après chacune.

## Étape 1 : afficher les nombres de 1 à 10

Le cours détaille les trois parties du `for` : initialisation, condition, incrément. Ici, on veut commencer à 1 et s'arrêter à 10 **inclus** :

```csharp
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(i);
}
```

Pourquoi `i <= 10` et pas `i < 10` ? Parce qu'on veut que 10 soit affiché aussi. Si tu mets `i < 10`, la boucle s'arrêtera à 9. C'est une erreur très fréquente (l'erreur "off-by-one") : prends l'habitude de vérifier la borne en te demandant "est-ce que la dernière valeur que je veux est incluse ou pas ?"

Teste avec `dotnet run`.

## Étape 2 : la table de multiplication de 7

Même structure de boucle, mais cette fois on calcule `7 * i` à chaque itération et on l'affiche avec interpolation (vu en TP 2) :

```csharp
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"7x{i}={7 * i}");
}
```

Ajoute cette boucle après la première (tu peux mettre une ligne `Console.WriteLine();` vide entre les deux pour séparer visuellement les résultats dans le terminal).

## Étape 3 : somme de 1 à 100, avec `while`

Ici on **ne sait pas a priori** combien de tours fera la boucle (même si en réalité on pourrait le calculer, l'énoncé impose `while` pour s'entraîner). Le cours explique qu'avec `while`, il faut gérer soi-même la variable de comptage : la déclarer avant, l'incrémenter dans le corps de la boucle.

Construis-le en réfléchissant à trois éléments, comme pour un `for` :
- une variable pour accumuler le résultat, qui démarre à 0 : `int somme = 0;`
- une variable de comptage, qui démarre à 1 : `int n = 1;`
- une condition d'arrêt : `n <= 100`

```csharp
int somme = 0;
int n = 1;
while (n <= 100)
{
    somme += n; // équivalent à somme = somme + n; (vu en TP 2)
    n++;
}
Console.WriteLine($"Somme de 1 à 100 = {somme}");
```

**Piège classique** : oublier `n++;` dans le corps de la boucle. Sans cet incrément, `n` ne change jamais, la condition `n <= 100` reste toujours vraie, et le programme tourne à l'infini (on appelle ça une "boucle infinie" — si ça t'arrive, fais `Ctrl+C` dans le terminal pour arrêter le programme).

## Étape 4 : le mini-menu avec `do...while`

Le cours présente exactement ce cas d'usage : un menu qu'on veut afficher **au moins une fois**, avant même de savoir ce que l'utilisateur va choisir — c'est pour ça qu'on utilise `do...while` plutôt que `while`.

Réfléchis à la condition d'arrêt : on continue **tant que** le choix n'est pas "2". Donc la condition du `while` final est `choix != "2"`.

```csharp
string? choix;
do
{
    Console.WriteLine("1. Dire bonjour");
    Console.WriteLine("2. Quitter");
    Console.Write("Ton choix : ");
    choix = Console.ReadLine();

    if (choix == "1")
    {
        Console.WriteLine("Bonjour !");
    }
    else if (choix != "2")
    {
        Console.WriteLine("Choix invalide.");
    }
}
while (choix != "2");
```

Remarque : `choix` est déclarée **avant** le `do`, sans valeur initiale précise (juste `string? choix;`), car elle doit rester accessible à la fois dans le corps de la boucle et dans la condition `while` finale — si tu la déclarais à l'intérieur du `do { }`, elle n'existerait plus au moment de tester la condition (rappelle-toi de la notion de "portée" qu'on reverra en détail au TP 6).

Teste en tapant "1" (doit afficher "Bonjour !" puis reposer la question), puis autre chose comme "abc" (doit afficher "Choix invalide." puis reposer la question), puis "2" (doit arrêter la boucle).

## Étape 5 : le triangle d'étoiles, boucles imbriquées

Le cours montre un exemple de boucles imbriquées (la grille `(ligne,colonne)`). Ici, il faut repérer le lien entre le numéro de ligne et le nombre d'étoiles : **la ligne n affiche n étoiles**.

- La boucle extérieure parcourt les lignes, de 1 à 5.
- La boucle intérieure affiche autant d'étoiles que le numéro de la ligne courante — donc sa condition d'arrêt dépend de la variable de la boucle extérieure (`colonne <= ligne`, pas une valeur fixe comme 5).

```csharp
for (int ligne = 1; ligne <= 5; ligne++)
{
    for (int colonne = 1; colonne <= ligne; colonne++)
    {
        Console.Write("*"); // Write, pas WriteLine : pas de retour à la ligne entre les étoiles
    }
    Console.WriteLine(); // retour à la ligne UNE FOIS la ligne d'étoiles terminée
}
```

Le détail important : `Console.WriteLine();` (vide) est placé **après** la boucle intérieure, mais **dans** la boucle extérieure — c'est ce qui permet de passer à la ligne suivante après chaque ligne d'étoiles, sans en rajouter une après chaque étoile individuelle.

## Vérifier son travail

Relance `dotnet run` et vérifie chaque résultat un par un : les 10 nombres, la table de 7, la somme (qui doit valoir 5050), le comportement du menu, et la forme triangulaire exacte. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 6 — Méthodes](../06-methodes/PasAPas.md)**
