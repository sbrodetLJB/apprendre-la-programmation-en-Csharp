# Pas à pas — TP 18 : Entity Framework Core avec SQLite

Relis le cours [18-entity-framework-core.md](../../docs/18-entity-framework-core.md) avant de commencer. Place-toi dans `tp/18-entity-framework/Enonce/Enonce/`. Le fichier `carnet.db` sera créé automatiquement dans ce dossier au premier `dotnet run`.

Les classes `Contact` et `CarnetContext` sont déjà définies en bas de `Program.cs` — lis-les attentivement avant de commencer, elles sont le cœur d'EF Core.

## TODO 1 : créer la base et ajouter 3 contacts

Le cours explique `EnsureCreated()` : si `carnet.db` n'existe pas, EF Core lit les classes `Contact` et `CarnetContext`, génère le SQL `CREATE TABLE` correspondant et crée le fichier. Si la base existe déjà, il ne fait rien.

```csharp
using (var context = new CarnetContext())
{
    context.Database.EnsureCreated();

    context.Contacts.Add(new Contact { Prenom = "Lucas",  Nom = "Martin",  Email = "lucas@example.com" });
    context.Contacts.Add(new Contact { Prenom = "Emma",   Nom = "Bernard", Email = "emma@example.com" });
    context.Contacts.Add(new Contact { Prenom = "Nathan", Nom = "Martin",  Email = "nathan@example.com" });
    context.SaveChanges();
    Console.WriteLine("Base créée et contacts ajoutés.");
}
```

Deux points importants :
- `using (var context = ...)` : ferme la connexion SQLite proprement à la fin du bloc (le `DbContext` implémente `IDisposable`).
- `SaveChanges()` : sans cet appel, les `Add(...)` restent en mémoire et **rien n'est écrit dans la base**.

Lance `dotnet run` une première fois. Un fichier `carnet.db` doit apparaître dans le dossier.

## TODO 2 : lire et afficher tous les contacts

Dans un **nouveau** `using (var context = ...)` (pour simuler une session distincte, comme le ferait une vraie application) :

```csharp
using (var context = new CarnetContext())
{
    List<Contact> tous = context.Contacts.ToList();
    tous.ForEach(Console.WriteLine); // utilise ToString() défini dans Contact
}
```

`context.Contacts.ToList()` génère `SELECT * FROM Contacts` et charge tous les résultats en mémoire. Tu dois voir les 3 contacts affichés avec leurs ids auto-incrémentés.

## TODO 3 : modifier un contact

EF Core "surveille" les objets qu'il a chargés (**change tracking**). Si tu modifies une propriété d'un objet chargé via le contexte, puis appelles `SaveChanges()`, EF Core génère automatiquement un `UPDATE SQL` :

```csharp
using (var context = new CarnetContext())
{
    Contact? lucas = context.Contacts.FirstOrDefault(c => c.Id == 1);
    if (lucas != null)
    {
        lucas.Email = "lucas.martin@nouveau.com";
        context.SaveChanges(); // génère UPDATE Contacts SET Email=... WHERE Id=1
        Console.WriteLine(lucas);
    }
}
```

## TODO 4 : supprimer un contact

`Remove(objet)` marque l'objet pour suppression ; `SaveChanges()` génère le `DELETE SQL`. On vérifie ensuite avec `Count()` (qui génère `SELECT COUNT(*) FROM Contacts`) :

```csharp
using (var context = new CarnetContext())
{
    Contact? nathan = context.Contacts.FirstOrDefault(c => c.Prenom == "Nathan");
    if (nathan != null)
    {
        context.Contacts.Remove(nathan);
        context.SaveChanges();
    }
    Console.WriteLine($"Contacts restants : {context.Contacts.Count()}");
}
```

## TODO 5 (bonus) : LINQ sur `DbSet`

L'avantage clé d'EF Core : on peut utiliser **exactement la même syntaxe LINQ** que sur une `List<T>` (TP16), mais les requêtes s'exécutent dans la base de données, pas en mémoire. EF Core traduit le LINQ en SQL :

```csharp
using (var context = new CarnetContext())
{
    var martin = context.Contacts
        .Where(c => c.Nom == "Martin")      // WHERE Nom = 'Martin'
        .OrderBy(c => c.Prenom)             // ORDER BY Prenom ASC
        .ToList();                           // exécute la requête
    martin.ForEach(Console.WriteLine);
}
```

## Rejouer le programme proprement

Si tu relances `dotnet run` plusieurs fois, EF Core ajoutera de nouveaux contacts à chaque fois (la base n'est pas effacée). Pour éviter les doublons dans un contexte pédagogique, le corrigé ajoute `context.Contacts.RemoveRange(context.Contacts); context.SaveChanges();` avant d'insérer — tu peux faire de même.

## Vérifier son travail

Lance `dotnet run` : tu dois voir les 3 contacts ajoutés, le contact modifié, le nombre de contacts après suppression, et (si tu as fait le bonus) les Martin triés. Compare avec `../Corrige/Program.cs`.

**Bravo — tu maîtrises maintenant EF Core, LINQ, async/await et les génériques avancés !**
