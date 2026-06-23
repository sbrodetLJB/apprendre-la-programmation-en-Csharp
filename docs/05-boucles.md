# 5. Boucles

Une boucle permet de répéter un bloc d'instructions plusieurs fois, sans avoir à les recopier.

## La boucle `for`

Idéale quand on connaît (ou peut calculer) **le nombre de répétitions** à l'avance :

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Itération numéro {i}");
}
// Affiche : 0, 1, 2, 3, 4
```

Le `for` a trois parties séparées par des `;` :

| Partie | Rôle | Ici |
|---|---|---|
| Initialisation | Exécutée une seule fois, au début | `int i = 0` |
| Condition | Vérifiée avant chaque itération ; la boucle continue tant qu'elle est vraie | `i < 5` |
| Incrément | Exécuté après chaque itération | `i++` |

## La boucle `while`

Idéale quand on ne connaît **pas** le nombre de répétitions à l'avance, mais qu'on a une condition d'arrêt :

```csharp
int compteur = 0;

while (compteur < 5)
{
    Console.WriteLine($"Compteur = {compteur}");
    compteur++;
}
```

La condition est testée **avant** chaque itération. Si elle est fausse dès le départ, le corps de la boucle ne s'exécute jamais.

## La boucle `do...while`

Comme `while`, mais la condition est testée **après** chaque itération : le corps s'exécute donc **au moins une fois**, même si la condition est fausse dès le début. Très utile pour les menus, où on veut afficher le menu au moins une fois avant de vérifier si l'utilisateur veut quitter :

```csharp
string? choix;

do
{
    Console.WriteLine("1. Ajouter");
    Console.WriteLine("2. Quitter");
    Console.Write("Ton choix : ");
    choix = Console.ReadLine();
}
while (choix != "2");

Console.WriteLine("Au revoir !");
```

## La boucle `foreach`

Utilisée pour parcourir tous les éléments d'une **collection** (un tableau, une liste...) sans avoir à gérer un index manuellement. On la verra en détail à la leçon suivante, mais voici un aperçu :

```csharp
string[] prenoms = { "Lucas", "Emma", "Nathan" };

foreach (string prenom in prenoms)
{
    Console.WriteLine($"Bonjour {prenom}");
}
```

## `break` et `continue`

- `break` arrête immédiatement la boucle, même si la condition est encore vraie.
- `continue` arrête l'itération en cours et passe directement à la suivante.

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 5)
    {
        break; // on arrête tout dès que i atteint 5
    }
    Console.WriteLine(i);
}
// Affiche : 0, 1, 2, 3, 4

for (int i = 0; i < 5; i++)
{
    if (i % 2 == 0)
    {
        continue; // on saute les nombres pairs
    }
    Console.WriteLine(i);
}
// Affiche : 1, 3
```

## Boucles imbriquées

On peut placer une boucle à l'intérieur d'une autre, par exemple pour afficher une grille :

```csharp
for (int ligne = 1; ligne <= 3; ligne++)
{
    for (int colonne = 1; colonne <= 3; colonne++)
    {
        Console.Write($"({ligne},{colonne}) ");
    }
    Console.WriteLine(); // retour à la ligne après chaque ligne de la grille
}
```

**Suite : [6. Méthodes](06-methodes.md)**
