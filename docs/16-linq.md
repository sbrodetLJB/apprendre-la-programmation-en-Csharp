# 16. LINQ — Language Integrated Query

## Qu'est-ce que LINQ ?

**LINQ** est un ensemble de méthodes d'extension (disponibles via `using System.Linq;`) qui permettent d'interroger, filtrer et transformer n'importe quelle collection `IEnumerable<T>` (tableau, `List<T>`, résultat d'une requête EF Core...) avec une syntaxe fluide, proche du SQL.

```csharp
using System.Linq;

List<int> notes = new List<int> { 12, 15, 9, 18, 7, 14 };

// Sans LINQ
List<int> bonnesNotes = new List<int>();
foreach (int n in notes)
    if (n >= 10) bonnesNotes.Add(n);

// Avec LINQ
List<int> bonnesNotes = notes.Where(n => n >= 10).ToList();
```

## Les méthodes LINQ essentielles

Tous les exemples utilisent cette liste :

```csharp
var contacts = new List<Contact>
{
    new Contact("Lucas", "Martin", "lucas@example.com"),
    new Contact("Emma", "Bernard", "emma@example.com"),
    new Contact("Nathan", "Martin", "nathan@example.com"),
    new Contact("Alice", "Dupont", "alice@example.com"),
};
```

### `Where` — filtrer

```csharp
var martin = contacts.Where(c => c.Nom == "Martin").ToList();
// [Lucas Martin, Nathan Martin]
```

### `Select` — transformer (projeter)

```csharp
var nomsComplets = contacts.Select(c => $"{c.Prenom} {c.Nom}").ToList();
// ["Lucas Martin", "Emma Bernard", "Nathan Martin", "Alice Dupont"]
```

`Select` produit une nouvelle séquence en appliquant une transformation à chaque élément. La collection source n'est pas modifiée.

### `OrderBy` / `OrderByDescending`

```csharp
var parPrenom = contacts.OrderBy(c => c.Prenom).ToList();
// Alice, Emma, Lucas, Nathan

var parPrenomDesc = contacts.OrderByDescending(c => c.Prenom).ToList();
// Nathan, Lucas, Emma, Alice
```

### `FirstOrDefault` / `First`

```csharp
Contact? emma = contacts.FirstOrDefault(c => c.Prenom == "Emma");
// Renvoie le premier contact dont le prénom est "Emma", ou null si aucun ne correspond.

// First() lève InvalidOperationException si aucun élément ne correspond -- à éviter si le résultat peut être absent
```

### `Any` / `All`

```csharp
bool ilYAUnMartin = contacts.Any(c => c.Nom == "Martin");   // true
bool tousOntEmail = contacts.All(c => c.Email.Contains("@")); // true
```

### `Count` / `Sum` / `Average` / `Min` / `Max`

```csharp
List<int> notes = new List<int> { 12, 15, 9, 18, 7 };

Console.WriteLine(notes.Count());          // 5
Console.WriteLine(notes.Sum());            // 61
Console.WriteLine(notes.Average());        // 12.2
Console.WriteLine(notes.Min());            // 7
Console.WriteLine(notes.Max());            // 18
```

### `GroupBy` — regrouper

```csharp
var parNom = contacts.GroupBy(c => c.Nom);

foreach (var groupe in parNom)
{
    Console.WriteLine($"Famille {groupe.Key} : {groupe.Count()} contact(s)");
    foreach (var c in groupe)
        Console.WriteLine($"  {c.Prenom}");
}
// Famille Martin : 2 contacts
//   Lucas
//   Nathan
// Famille Bernard : 1 contact
//   Emma
// ...
```

### `Distinct` / `Contains`

```csharp
List<string> noms = contacts.Select(c => c.Nom).Distinct().ToList();
// ["Martin", "Bernard", "Dupont"]
```

## Chaîner les opérations

L'intérêt de LINQ est de pouvoir enchaîner les opérations :

```csharp
var resultat = contacts
    .Where(c => c.Nom == "Martin")    // filtrer
    .OrderBy(c => c.Prenom)           // trier
    .Select(c => c.Prenom)            // projeter
    .ToList();
// ["Lucas", "Nathan"]
```

Chaque méthode renvoie un `IEnumerable<T>` sur lequel on peut chaîner la suivante. `ToList()` (ou `ToArray()`) termine la chaîne et matérialise le résultat en collection concrète.

## Syntaxe de requête (query syntax)

LINQ offre aussi une syntaxe inspirée du SQL, équivalente aux méthodes :

```csharp
var martin = from c in contacts
             where c.Nom == "Martin"
             orderby c.Prenom
             select c.Prenom;
```

Les deux syntaxes sont strictement équivalentes. La syntaxe méthode (`.Where(...).Select(...)`) est plus couramment utilisée en pratique et s'intègre mieux dans un style fluide.

## `ToDictionary` — convertir une liste en dictionnaire

```csharp
// Indexer les contacts par email (supposé unique)
Dictionary<string, Contact> parEmail = contacts.ToDictionary(c => c.Email);
Contact? lucas = parEmail.GetValueOrDefault("lucas@example.com");
```

**Suite : [17. async/await](17-async-await.md)**
