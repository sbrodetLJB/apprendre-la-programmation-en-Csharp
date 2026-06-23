# 1. Introduction à C# et .NET

## C#, c'est quoi ?

**C#** (prononcer "C sharp") est un langage de programmation créé par Microsoft, utilisé pour écrire des applications de bureau, des sites web, des jeux (avec Unity), des applications mobiles (avec .NET MAUI) ou des API (comme on l'a vu dans le cours sur les API).

C'est un langage **compilé** (le code source est transformé en code exécutable avant de tourner) et **fortement typé** (chaque donnée a un type bien défini, vérifié par le compilateur avant même d'exécuter le programme).

## .NET, c'est quoi ?

**.NET** est la plateforme qui exécute le code C#. Elle fournit :
- le compilateur (qui transforme le `.cs` en code exécutable) ;
- une immense bibliothèque de fonctionnalités prêtes à l'emploi (manipulation de texte, de fichiers, de dates, de réseau...) ;
- l'outil en ligne de commande `dotnet`, qu'on va utiliser tout au long de ce cours.

## Premier programme

Créons un nouveau projet de type "application console" (un programme qui s'exécute dans un terminal) :

```bash
dotnet new console -n MonPremierProgramme
cd MonPremierProgramme
```

Cette commande génère un dossier contenant deux fichiers importants :

| Fichier | Rôle |
|---|---|
| `MonPremierProgramme.csproj` | Décrit le projet (nom, version de .NET ciblée, dépendances) |
| `Program.cs` | Le code source de notre programme |

Le contenu généré de `Program.cs` ressemble à :

```csharp
Console.WriteLine("Hello, World!");
```

`Console.WriteLine(...)` affiche une ligne de texte dans le terminal. C'est l'équivalent du `print()` en Python ou du `echo` en PHP.

## Exécuter le programme

```bash
dotnet run
```

Cette commande compile **et** exécute le programme en une seule étape. Pendant qu'on apprend, c'est la commande qu'on utilisera tout le temps.

## Les commentaires

```csharp
// Ceci est un commentaire sur une seule ligne, ignoré par le compilateur.

/*
   Ceci est un commentaire
   sur plusieurs lignes.
*/

Console.WriteLine("Hello, World!"); // on peut aussi commenter en fin de ligne
```

## Le point-virgule et les blocs

En C#, chaque instruction se termine par un **point-virgule** `;`. Les blocs de code (le corps d'une méthode, d'une condition, d'une boucle...) sont délimités par des accolades `{ }`. L'indentation (les espaces en début de ligne) n'a aucune incidence sur l'exécution — contrairement à Python — mais on l'utilise quand même pour que le code reste lisible.

```csharp
Console.WriteLine("Ligne 1");
Console.WriteLine("Ligne 2");
```

## Structure d'un vrai projet C#

Dans les versions récentes de C# (celles qu'on utilise ici), on peut écrire du code directement au niveau du fichier (comme ci-dessus, appelé "top-level statements"), sans avoir à écrire explicitement de classe `Program` ni de méthode `Main`. C'est très pratique pour apprendre. Sache simplement que, en coulisses, le compilateur transforme ce code en quelque chose d'équivalent à :

```csharp
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
```

On reverra les classes et les méthodes en détail dans les prochaines leçons — pas besoin de comprendre cette syntaxe dès maintenant.

**Suite : [2. Variables, types et opérateurs](02-variables-types-operateurs.md)**
