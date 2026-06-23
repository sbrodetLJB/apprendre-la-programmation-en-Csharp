# Pas à pas — TP 1 : Introduction à C# et .NET

Tu n'as encore jamais programmé ? Ce guide t'accompagne ligne par ligne. Garde sous les yeux le cours [01-introduction-csharp-dotnet.md](../../docs/01-introduction-csharp-dotnet.md) : tout ce dont tu as besoin y est expliqué.

## Avant de commencer

1. Ouvre un terminal dans le dossier `tp/01-introduction/Enonce/`.
2. Vérifie que .NET est installé :
   ```bash
   dotnet --version
   ```
   Si une erreur apparaît, installe le SDK .NET (lien dans le cours) avant de continuer.
3. Ouvre `Program.cs` dans ton éditeur. Tu devrais voir une seule ligne :
   ```csharp
   Console.WriteLine("TODO : remplace cette ligne par ton code"); // tu peux supprimer cette ligne
   ```
   C'est une instruction qui affiche du texte dans le terminal — relis la section "Premier programme" du cours si ce n'est pas clair. Tu peux supprimer cette ligne, elle ne servait qu'à te montrer que le projet fonctionne.

## Étape 1 : afficher "Bonjour tout le monde !"

Le cours montre que `Console.WriteLine("...")` affiche une ligne de texte. Écris :

```csharp
Console.WriteLine("Bonjour tout le monde !");
```

N'oublie pas le point-virgule `;` à la fin : en C#, **chaque instruction se termine par un point-virgule**. Sans lui, le programme ne compilera pas.

Teste tout de suite (on ne déclare pas tout d'un coup avant de tester en C#, c'est une mauvaise habitude) :

```bash
dotnet run
```

Tu devrais voir `Bonjour tout le monde !` s'afficher dans le terminal. Si tu as une erreur, relis attentivement le message : il indique en général le numéro de ligne et la nature de l'erreur (souvent un point-virgule ou une accolade oubliée).

## Étape 2 : affiche ton prénom sur une ligne séparée

Ajoute une deuxième ligne, juste après la première :

```csharp
Console.WriteLine("Bonjour tout le monde !");
Console.WriteLine("Lucas"); // remplace "Lucas" par ton propre prénom
```

Remarque : les instructions s'exécutent **dans l'ordre, de haut en bas**. C'est pour ça que ce `WriteLine` s'affichera après le premier. Relance `dotnet run` pour vérifier.

## Étape 3 : trois lignes (prénom, classe, année scolaire)

Même principe : un `Console.WriteLine(...)` par information, l'un après l'autre.

```csharp
Console.WriteLine("Lucas");
Console.WriteLine("BTS SIO 2");
Console.WriteLine("2025-2026");
```

Tu peux les enchaîner directement après les lignes précédentes, ou les remplacer si tu préfères repartir sur ces trois lignes précises pour cette question.

## Étape 4 : ajoute des commentaires

Le cours explique les commentaires : `//` pour un commentaire sur une seule ligne, ignoré par le compilateur — il ne sert qu'à toi (et à celles et ceux qui relisent ton code).

Ajoute une ligne de commentaire **au-dessus** de chaque affichage, qui explique ce qu'il fait :

```csharp
// Salutation générale
Console.WriteLine("Bonjour tout le monde !");

// Affiche mon prénom
Console.WriteLine("Lucas");

// Affiche ma classe
Console.WriteLine("BTS SIO 2");

// Affiche mon année scolaire
Console.WriteLine("2025-2026");
```

## Vérifier son travail

Relance `dotnet run` une dernière fois et vérifie que les 4 lignes (ou plus, selon comment tu as regroupé les étapes 2-3) s'affichent dans l'ordre attendu, sans erreur de compilation.

Une fois que ton programme fonctionne, ouvre `../Corrige/Program.cs` et compare : la structure doit être très proche (les valeurs comme le prénom seront évidemment différentes). Si ton code fonctionne mais ressemble peu au corrigé, ce n'est pas grave — il existe souvent plusieurs façons correctes d'écrire la même chose.

**Suite : [TP 2 — Variables, types et opérateurs](../02-variables-types/PasAPas.md)**
