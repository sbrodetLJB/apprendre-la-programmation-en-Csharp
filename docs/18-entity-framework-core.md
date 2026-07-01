# 18. Accès aux données avec Entity Framework Core

## Qu'est-ce qu'un ORM ?

Un **ORM** (Object-Relational Mapper) fait le pont entre les **objets C#** et les **tables d'une base de données relationnelle** — sans avoir à écrire de SQL à la main. Entity Framework Core (EF Core) est l'ORM officiel de .NET.

Avec EF Core :
- Chaque **classe C#** correspond à une **table**.
- Chaque **propriété** correspond à une **colonne**.
- Les opérations CRUD (Create, Read, Update, Delete) s'écrivent en C# ; EF Core génère le SQL correspondant.

## Installation

```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

On utilisera **SQLite** : une base de données stockée dans un seul fichier `.db`, parfaite pour apprendre (pas de serveur à installer).

## Les deux concepts clés : `DbContext` et `DbSet<T>`

**`DbContext`** représente la session avec la base de données — c'est la classe qu'on hérite pour configurer la connexion et les tables.

**`DbSet<T>`** représente une table : c'est une propriété du `DbContext` sur laquelle on peut appliquer des requêtes LINQ, et qui expose les opérations CRUD.

```csharp
using Microsoft.EntityFrameworkCore;

class CarnetContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=carnet.db"); // nom du fichier SQLite
    }
}
```

## Le modèle

EF Core a besoin que la classe utilisée comme entité ait une **clé primaire** — par convention, une propriété nommée `Id` ou `[NomClasse]Id` est automatiquement reconnue comme clé primaire :

```csharp
class Contact
{
    public int Id { get; set; }           // clé primaire, auto-incrémentée
    public string Prenom { get; set; } = "";
    public string Nom { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Telephone { get; set; }
}
```

## Créer la base de données

```csharp
using var context = new CarnetContext();
context.Database.EnsureCreated(); // crée le fichier carnet.db si il n'existe pas encore
```

`EnsureCreated()` crée les tables à partir des classes C# si la base n'existe pas. C'est la méthode la plus simple pour commencer — on verra les **migrations** (plus puissantes, pour faire évoluer le schéma sans perdre les données) dans un projet plus avancé.

## CRUD avec EF Core

### Create — ajouter

```csharp
using var context = new CarnetContext();
context.Database.EnsureCreated();

context.Contacts.Add(new Contact
{
    Prenom = "Lucas",
    Nom = "Martin",
    Email = "lucas@example.com"
});
context.SaveChanges(); // IMPORTANT : sans SaveChanges(), rien n'est écrit en base
```

`SaveChanges()` matérialise toutes les modifications en attente (ajouts, mises à jour, suppressions) dans la base, en une seule transaction.

### Read — lire

```csharp
List<Contact> tous = context.Contacts.ToList();

Contact? lucas = context.Contacts.FirstOrDefault(c => c.Prenom == "Lucas");
```

On utilise exactement les mêmes méthodes LINQ que sur `List<T>` (leçon 16) — EF Core les traduit en SQL.

### Update — modifier

```csharp
Contact? contact = context.Contacts.FirstOrDefault(c => c.Id == 1);
if (contact != null)
{
    contact.Telephone = "06 12 34 56 78";
    context.SaveChanges(); // persiste la modification
}
```

EF Core **suit les modifications** sur les objets qu'il a chargés (mécanisme de "change tracking"). Modifier une propriété puis appeler `SaveChanges()` génère automatiquement un `UPDATE SQL`.

### Delete — supprimer

```csharp
Contact? contact = context.Contacts.FirstOrDefault(c => c.Id == 1);
if (contact != null)
{
    context.Contacts.Remove(contact);
    context.SaveChanges();
}
```

## LINQ sur `DbSet<T>`

Les requêtes LINQ sur un `DbSet<T>` sont traduites en SQL par EF Core — elles s'exécutent **côté base de données**, pas en mémoire :

```csharp
// EF Core génère : SELECT * FROM Contacts WHERE Nom = 'Martin'
var martin = context.Contacts
    .Where(c => c.Nom == "Martin")
    .OrderBy(c => c.Prenom)
    .ToList();
```

Toujours terminer par `.ToList()` (ou `.FirstOrDefault()`, `.Count()`, etc.) pour **déclencher** l'exécution de la requête.

## `using var context`

On utilise systématiquement `using var context = new CarnetContext();` : le `using` garantit que la connexion à la base est fermée proprement à la fin du bloc, même en cas d'exception (le `DbContext` implémente `IDisposable`).

**Tu maîtrises maintenant l'accès aux données avec EF Core !** La combinaison EF Core + LINQ + async/await (les requêtes EF Core ont des variantes asynchrones : `ToListAsync()`, `FirstOrDefaultAsync()`...) est le socle de la grande majorité des applications .NET professionnelles.
