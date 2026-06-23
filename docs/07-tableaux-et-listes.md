# 7. Tableaux et `List<T>`

## Les tableaux (`array`)

Un tableau stocke plusieurs valeurs **du même type**, dans un ordre, avec une **taille fixe** définie à la création :

```csharp
string[] prenoms = { "Lucas", "Emma", "Nathan" };

Console.WriteLine(prenoms[0]); // "Lucas" -- le premier élément est à l'indice 0
Console.WriteLine(prenoms[2]); // "Nathan"
Console.WriteLine(prenoms.Length); // 3 -- nombre d'éléments
```

Autre façon de créer un tableau, avec une taille fixée mais des valeurs à remplir ensuite :

```csharp
int[] notes = new int[5]; // tableau de 5 int, tous initialisés à 0
notes[0] = 15;
notes[1] = 12;
```

Attention : essayer d'accéder à un indice qui n'existe pas (`prenoms[10]` alors qu'il n'y a que 3 éléments) provoque une exception `IndexOutOfRangeException` qui arrête le programme si elle n'est pas gérée.

## Parcourir un tableau

```csharp
string[] prenoms = { "Lucas", "Emma", "Nathan" };

// Avec for (utile si on a besoin de l'indice)
for (int i = 0; i < prenoms.Length; i++)
{
    Console.WriteLine($"{i} : {prenoms[i]}");
}

// Avec foreach (plus simple si on n'a pas besoin de l'indice)
foreach (string prenom in prenoms)
{
    Console.WriteLine(prenom);
}
```

## La limite des tableaux : taille fixe

```csharp
string[] prenoms = { "Lucas", "Emma" };
// prenoms.Ajouter("Nathan"); // IMPOSSIBLE : un tableau ne peut pas grandir
```

Quand on a besoin d'ajouter ou de retirer des éléments dynamiquement, on utilise une **liste**.

## `List<T>`

`List<T>` est une collection dont la taille s'adapte automatiquement. Le `<T>` indique le type des éléments qu'elle contient (`T` pour "Type" — on parle de **type générique**) :

```csharp
using System.Collections.Generic;

List<string> prenoms = new List<string>();
prenoms.Add("Lucas");
prenoms.Add("Emma");
prenoms.Add("Nathan");

Console.WriteLine(prenoms.Count); // 3 -- (pas .Length comme pour un tableau !)
Console.WriteLine(prenoms[0]);    // "Lucas" -- accès par indice, comme un tableau

prenoms.Remove("Emma");  // retire la première occurrence de "Emma"
prenoms.RemoveAt(0);     // retire l'élément à l'indice 0

Console.WriteLine(prenoms.Contains("Nathan")); // true
```

On peut aussi initialiser une `List<T>` directement avec des valeurs :

```csharp
List<int> notes = new List<int> { 15, 12, 18, 9 };
```

## Méthodes utiles de `List<T>`

```csharp
List<int> notes = new List<int> { 15, 12, 18, 9 };

notes.Sort();                 // trie la liste en place : 9, 12, 15, 18
notes.Reverse();               // inverse l'ordre : 18, 15, 12, 9
bool aUneNoteFaible = notes.Exists(n => n < 10); // true -- on reverra cette syntaxe (lambda) plus tard
int max = notes.Max();          // 18 (nécessite using System.Linq;)
double moyenne = notes.Average(); // moyenne des notes (nécessite using System.Linq;)
```

## Tableau vs `List<T>` : que choisir ?

| | Tableau (`T[]`) | `List<T>` |
|---|---|---|
| Taille | Fixe | Dynamique (s'agrandit/rétrécit) |
| Ajouter/retirer un élément | Impossible directement | `.Add()` / `.Remove()` |
| Nombre d'éléments | `.Length` | `.Count` |
| Quand l'utiliser | Quand le nombre d'éléments est connu et fixe | Dans la grande majorité des cas en pratique |

En pratique, dans du code métier, on utilise `List<T>` bien plus souvent que les tableaux.

**Suite : [8. POO : classes, objets, encapsulation](08-poo-classes-objets.md)**
