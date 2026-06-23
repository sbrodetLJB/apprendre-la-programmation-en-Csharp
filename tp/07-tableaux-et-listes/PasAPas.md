# Pas à pas — TP 7 : Tableaux et `List<T>`

Relis le cours [07-tableaux-et-listes.md](../../docs/07-tableaux-et-listes.md) avant de commencer. Place-toi dans `tp/07-tableaux-et-listes/Enonce/`.

## Étape 1 : un tableau de 5 notes, affiché avec `foreach`

Le cours montre comment créer un tableau directement rempli de valeurs, avec des accolades :

```csharp
int[] notes = { 12, 15, 9, 18, 14 }; // choisis tes propres valeurs si tu veux
```

Pour l'afficher, l'énoncé demande un `foreach` (vu en TP5/cours leçon 5 et 7) — il est plus simple qu'un `for` ici car on n'a pas besoin de l'indice, juste de chaque valeur une par une :

```csharp
foreach (int note in notes)
{
    Console.WriteLine(note);
}
```

Teste avec `dotnet run` avant de poursuivre.

## Étape 2 : calculer la moyenne des notes

Il faut d'abord la somme, puis diviser par le nombre d'éléments (`notes.Length`, vu dans le cours). On accumule la somme avec une boucle, comme au TP5 :

```csharp
int somme = 0;
foreach (int note in notes)
{
    somme += note;
}
double moyenne = (double)somme / notes.Length;
Console.WriteLine($"Moyenne : {moyenne}");
```

Pourquoi `(double)somme` ? Rappelle-toi du cours sur les conversions (leçon 3) : la division entre deux `int` tronque la partie décimale. `somme` et `notes.Length` sont tous les deux des `int` ; sans ce cast explicite vers `double`, `15 / 5` donnerait `3` même si la vraie moyenne est `3.2`. Le `(double)` force le calcul à se faire avec des décimales.

## Étape 3 : la liste de courses

Le cours détaille `List<T>` : taille dynamique, `.Add()` pour ajouter, `.Count` (pas `.Length` !) pour la taille, `.Remove()` pour retirer une valeur précise.

```csharp
List<string> courses = new List<string>();
courses.Add("Pain");
courses.Add("Lait");
courses.Add("Oeufs");
courses.Add("Beurre");

Console.WriteLine($"Nombre d'articles : {courses.Count}");
```

L'énoncé demande de retirer le **2e article ajouté** — ici c'est `"Lait"`. `.Remove(valeur)` retire la première occurrence de cette valeur précise (pas par position) :

```csharp
courses.Remove("Lait");

foreach (string article in courses)
{
    Console.WriteLine(article);
}
```

Vérifie que la liste affichée à la fin contient bien 3 articles, sans "Lait".

## Étape 4 : trois prénoms saisis puis triés

Ici on combine une boucle `for` qui répète 3 fois la demande, la lecture clavier (TP3), et l'ajout dans une liste :

```csharp
List<string> prenoms = new List<string>();
for (int i = 1; i <= 3; i++)
{
    Console.Write($"Prénom n°{i} : ");
    string? prenom = Console.ReadLine();
    if (prenom != null)
    {
        prenoms.Add(prenom);
    }
}
```

Pourquoi le `if (prenom != null)` ? `Console.ReadLine()` peut renvoyer `null` (cours leçon 3) ; `.Add()` attend un `string` non-`null`, donc on vérifie avant d'ajouter, par précaution.

Ensuite, `.Sort()` (vu dans le cours, section "Méthodes utiles de List<T>") trie la liste **en place** (elle modifie directement `prenoms`, pas besoin de récupérer un résultat) :

```csharp
prenoms.Sort();
foreach (string prenom in prenoms)
{
    Console.WriteLine(prenom);
}
```

Teste en tapant trois prénoms dans le désordre alphabétique (ex. "Zoé", "Anna", "Marc") et vérifie qu'ils s'affichent triés.

## Étape 5 (bonus) : `.Max()`, `.Min()`, `.Exists()`

Le cours précise que `.Max()` et la lambda `n => n < 10` dans `.Exists()` nécessitent `using System.Linq;` **en tout début de fichier** (avant toute autre ligne de code) :

```csharp
using System.Linq;

// ... reste du programme
```

Ajoute cette ligne tout en haut du fichier si tu fais cette question. Puis :

```csharp
List<int> notesList = new List<int> { 12, 15, 9, 18, 14 };
Console.WriteLine($"Max : {notesList.Max()}");
Console.WriteLine($"Min : {notesList.Min()}");
Console.WriteLine($"Au moins une note insuffisante : {notesList.Exists(n => n < 10)}");
```

`n => n < 10` se lit "pour chaque élément n de la liste, teste si n < 10" — c'est une lambda, une syntaxe qu'on retrouvera plus tard ; pour l'instant, contente-toi de la recopier en remplaçant la condition si besoin.

## Vérifier son travail

Relance `dotnet run` et vérifie chaque bloc affiché : les 5 notes, la moyenne, la liste de courses sans "Lait", les prénoms triés, et (si tu as fait le bonus) max/min/test. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 8 — POO : classes, objets, encapsulation](../08-poo-classes-objets/PasAPas.md)**
