# Pas à pas — TP 16 : LINQ

Relis le cours [16-linq.md](../../docs/16-linq.md) avant de commencer. Place-toi dans `tp/16-linq/Enonce/Enonce/`. Les listes `contacts` et `livres` sont déjà définies dans `Program.cs` — ne les modifie pas, écris tes requêtes juste après le `// TODO`.

## TODO 1 : `Where` — filtrer les Martin

Le cours montre que `Where` prend une lambda (condition) et renvoie tous les éléments qui la satisfont. La lambda `c => c.Nom == "Martin"` se lit "pour chaque contact c, teste si son Nom est 'Martin'" :

```csharp
var martin = contacts.Where(c => c.Nom == "Martin").ToList();
martin.ForEach(Console.WriteLine); // utilise ToString() défini dans la classe Contact
```

`ToList()` est indispensable ici pour obtenir une `List<Contact>` concrète. Sans lui, `Where` renvoie un `IEnumerable<Contact>` paresseux (non encore évalué).

## TODO 2 : `Select` — projeter en noms complets

`Select` transforme chaque élément selon une lambda. Le résultat est une séquence du **type renvoyé par la lambda** — ici `string` :

```csharp
var nomsComplets = contacts.Select(c => $"{c.Prenom} {c.Nom.ToUpper()}").ToList();
nomsComplets.ForEach(Console.WriteLine);
```

## TODO 3 : `OrderBy` — trier les livres

```csharp
var parPages = livres.OrderBy(l => l.NombrePages).ToList();
parPages.ForEach(l => Console.WriteLine($"{l.Titre} ({l.NombrePages} p.)"));
```

Pour l'ordre décroissant, utilise `OrderByDescending`.

## TODO 4 : `Any` / `All`

`Any(condition)` renvoie `true` si au moins un élément satisfait la condition. `All(condition)` renvoie `true` si **tous** les éléments la satisfont :

```csharp
bool auMoinsUnExample = contacts.Any(c => c.Email.Contains("@example.com"));
bool tousPlus50Pages  = livres.All(l => l.NombrePages > 50);
Console.WriteLine($"Au moins un @example.com : {auMoinsUnExample}");
Console.WriteLine($"Tous > 50 pages : {tousPlus50Pages}");
```

## TODO 5 : agrégats sur une liste de notes

Crée la liste de notes directement dans ton code, puis applique les méthodes une par une — elles sont toutes sans paramètre quand on les appelle sur une `List<int>` :

```csharp
var notes = new List<int> { 12, 15, 9, 18, 7, 14 };
Console.WriteLine($"Count   : {notes.Count()}");
Console.WriteLine($"Sum     : {notes.Sum()}");
Console.WriteLine($"Average : {notes.Average():F1}"); // F1 = 1 décimale
Console.WriteLine($"Min     : {notes.Min()}");
Console.WriteLine($"Max     : {notes.Max()}");
```

## TODO 6 : `GroupBy` — regrouper par famille

`GroupBy` renvoie une séquence de **groupes**, où chaque groupe a une propriété `.Key` (la valeur commune) et est lui-même énumérable (les éléments du groupe). On peut enchaîner d'autres opérations LINQ à l'intérieur :

```csharp
var parFamille = contacts.GroupBy(c => c.Nom);
foreach (var groupe in parFamille)
{
    string prenoms = string.Join(", ", groupe.Select(c => c.Prenom));
    Console.WriteLine($"Famille {groupe.Key} ({groupe.Count()} contact(s)) : {prenoms}");
}
```

## TODO 7 (bonus) : chaîner plusieurs opérations

C'est la puissance de LINQ : chaque méthode renvoie un `IEnumerable<T>` sur lequel on peut appeler la suivante. L'ordre est : filtre d'abord (`Where`), puis tri (`OrderBy`), puis projection (`Select`) — même logique qu'une requête SQL :

```csharp
var prenomsMartinTries = contacts
    .Where(c => c.Nom == "Martin")
    .OrderBy(c => c.Prenom)
    .Select(c => c.Prenom)
    .ToList();
Console.WriteLine(string.Join(", ", prenomsMartinTries)); // Lucas, Nathan
```

## Vérifier son travail

Lance `dotnet run` et vérifie chaque section. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 17 — async/await](../17-async-await/PasAPas.md)**
