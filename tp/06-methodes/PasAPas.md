# Pas à pas — TP 6 : Méthodes

Relis le cours [06-methodes.md](../../docs/06-methodes.md) avant de commencer. Place-toi dans `tp/06-methodes/Enonce/`.

Remarque importante sur la structure du fichier : l'énoncé te dit de déclarer tes méthodes **en dessous** du code qui les appelle. C'est inhabituel par rapport à d'autres langages, mais c'est la convention de ce cours en style "top-level statements" (voir TP1/cours leçon 1) : le code principal en haut, les méthodes en bas.

## Étape 1 : `Carre(int n)`

Le cours donne l'anatomie complète d'une méthode : type de retour, nom (en PascalCase, donc qui commence par une majuscule), paramètres, corps avec `return`.

Le carré d'un nombre, c'est `n * n`. Comme on veut récupérer un résultat (pas juste afficher quelque chose), le type de retour est `int` et on utilise `return` :

```csharp
static int Carre(int n)
{
    return n * n;
}
```

Place cette déclaration en bas du fichier. Puis, **en haut**, appelle-la et affiche le résultat :

```csharp
Console.WriteLine(Carre(5)); // doit afficher 25
```

Teste avec `dotnet run` avant de continuer : c'est plus facile de corriger une erreur sur une seule méthode que sur cinq à la fois.

## Étape 2 : `EstPair(int n)`

Tu as déjà utilisé le modulo `%` au TP 4 pour tester la parité. Ici, la différence est qu'on **encapsule** ce test dans une méthode qui renvoie directement un `bool` :

```csharp
static bool EstPair(int n)
{
    return n % 2 == 0;
}
```

Remarque : `n % 2 == 0` est déjà une expression qui vaut `true` ou `false` — pas besoin d'écrire un `if` pour ensuite faire `return true;` ou `return false;`, on peut renvoyer directement le résultat de la comparaison.

```csharp
Console.WriteLine(EstPair(7)); // doit afficher False
```

## Étape 3 : `Moyenne(double a, double b, double c)`

Trois paramètres cette fois, tous `double` (puisqu'une moyenne peut avoir des décimales même si les notes sont entières) :

```csharp
static double Moyenne(double a, double b, double c)
{
    return (a + b + c) / 3;
}
```

```csharp
Console.WriteLine(Moyenne(12, 15, 9)); // doit afficher 12
```

## Étape 4 : `AfficherLigne(int longueur, char symbole = '*')`

Cette méthode ne renvoie rien (elle se contente d'afficher) : son type de retour est donc `void`, comme expliqué dans le cours.

Le second paramètre a une **valeur par défaut** (`'*'`) : le cours précise que ça le rend optionnel à l'appel. Pour afficher "longueur fois le symbole", utilise une boucle `for` (TP5) qui répète `Console.Write(symbole)` (pas `WriteLine`, pour rester sur la même ligne) :

```csharp
static void AfficherLigne(int longueur, char symbole = '*')
{
    for (int i = 0; i < longueur; i++)
    {
        Console.Write(symbole);
    }
    Console.WriteLine(); // retour à la ligne final, une fois la ligne complète affichée
}
```

Appelle-la deux fois, comme demandé : une fois sans préciser le symbole (il prendra `'*'` par défaut), une fois en le précisant :

```csharp
AfficherLigne(5);       // *****
AfficherLigne(5, '#');  // #####
```

## Étape 5 : `Decrire` surchargée — et le piège des fonctions locales

C'est le point le plus délicat du TP. L'énoncé t'avertit : les méthodes déclarées en bas d'un fichier "top-level statements" sont en réalité des **fonctions locales**, qui ne supportent pas la surcharge (avoir deux méthodes du même nom). Le cours détaille ce point dans la section "La surcharge de méthode".

La solution : regrouper les deux versions de `Decrire` dans une classe statique. On n'a pas encore vu les classes en détail (ce sera la leçon 8), mais ici on s'en sert juste comme d'un "conteneur" de méthodes :

```csharp
static class Outils
{
    public static void Decrire(int valeur)
    {
        Console.WriteLine($"{valeur} est un nombre entier.");
    }

    public static void Decrire(string valeur)
    {
        Console.WriteLine($"\"{valeur}\" est une chaîne de {valeur.Length} caractères.");
    }
}
```

Remarque : `valeur.Length` (sur un `string`) donne le nombre de caractères — une fonctionnalité déjà disponible sur tout `string`, qu'on retrouvera souvent.

On appelle ensuite ces méthodes en préfixant par le nom de la classe, `Outils.` :

```csharp
Outils.Decrire(42);        // version (int)
Outils.Decrire("Bonjour"); // version (string) -- C# choisit automatiquement la bonne version
```

C'est exactement le mécanisme de surcharge décrit dans le cours : selon que tu donnes un `int` ou un `string` à `Decrire`, C# appelle la version adaptée.

## Vérifier son travail

Ton fichier doit ressembler à : tous les appels (`Console.WriteLine(Carre(5));`, etc.) en haut, puis toutes les déclarations de méthodes (`static int Carre...`, `static bool EstPair...`, etc., et la classe `Outils` en dernier) en bas. Relance `dotnet run` et vérifie chaque résultat affiché. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 7 — Tableaux et List\<T\>](../07-tableaux-et-listes/PasAPas.md)**
