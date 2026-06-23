# 6. Méthodes

## Pourquoi des méthodes ?

Une **méthode** (qu'on appelle aussi "fonction" dans d'autres langages) est un bloc de code nommé, réutilisable, auquel on peut donner des informations en entrée (les **paramètres**) et qui peut renvoyer un résultat (la **valeur de retour**). Elle évite de recopier le même code plusieurs fois.

## Déclarer une méthode

```csharp
static int Additionner(int a, int b)
{
    return a + b;
}

int resultat = Additionner(3, 4);
Console.WriteLine(resultat); // 7
```

Anatomie d'une déclaration de méthode :

| Élément | Ici | Rôle |
|---|---|---|
| Type de retour | `int` | Le type de la valeur renvoyée par `return`. `void` si la méthode ne renvoie rien. |
| Nom | `Additionner` | En C#, par convention, les noms de méthode commencent par une majuscule (PascalCase) |
| Paramètres | `(int a, int b)` | Les informations attendues en entrée, avec leur type |
| Corps | `{ return a + b; }` | Le code exécuté |

Le mot-clé `static` sera expliqué en détail dans la leçon sur la POO ; pour l'instant, retiens simplement qu'il faut l'écrire pour les méthodes qu'on appelle directement dans un programme "top-level statements" comme les nôtres.

## Méthode sans valeur de retour

```csharp
static void AfficherBienvenue(string prenom)
{
    Console.WriteLine($"Bienvenue {prenom} !");
}

AfficherBienvenue("Lucas"); // n'attend pas de résultat
```

`void` signifie "ne renvoie rien". On ne peut donc pas écrire `int x = AfficherBienvenue("Lucas");` — la méthode n'a pas de valeur à donner.

## Paramètres optionnels

On peut donner une valeur par défaut à un paramètre ; il devient alors optionnel à l'appel :

```csharp
static void Saluer(string prenom, string salutation = "Bonjour")
{
    Console.WriteLine($"{salutation} {prenom} !");
}

Saluer("Lucas");              // "Bonjour Lucas !"
Saluer("Emma", "Bonsoir");    // "Bonsoir Emma !"
```

Les paramètres avec valeur par défaut doivent toujours être placés **après** les paramètres obligatoires.

## La surcharge de méthode

Plusieurs méthodes peuvent porter le **même nom** si elles ont des paramètres différents (en nombre ou en type). C# choisit automatiquement la bonne version selon les arguments fournis à l'appel — c'est la **surcharge** (overload) :

```csharp
static int Additionner(int a, int b)
{
    return a + b;
}

static double Additionner(double a, double b)
{
    return a + b;
}

Console.WriteLine(Additionner(2, 3));       // 5    -> version (int, int)
Console.WriteLine(Additionner(2.5, 3.5));   // 6.0  -> version (double, double)
```

> **Remarque pratique** : dans un programme écrit en style "top-level statements" (comme tous nos exemples, sans classe `Program` explicite), les méthodes qu'on déclare en bas du fichier sont en réalité des **fonctions locales**, qui ne supportent pas la surcharge. Pour surcharger plusieurs méthodes de même nom, il faut les placer dans une classe, par exemple `static class Outils { ... }`, puis les appeler avec `Outils.Additionner(...)`.

## Portée des variables

Une variable déclarée à l'intérieur d'une méthode (ou d'un bloc `{ }`) n'existe que dans ce bloc : c'est sa **portée** (scope). Elle est inaccessible en dehors.

```csharp
static void UneMethode()
{
    int x = 10; // x n'existe que dans cette méthode
    Console.WriteLine(x);
}

// Console.WriteLine(x); // ERREUR : x n'existe pas ici
```

**Suite : [7. Tableaux et List\<T\>](07-tableaux-et-listes.md)**
