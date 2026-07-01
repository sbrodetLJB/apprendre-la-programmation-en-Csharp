# 17. Programmation asynchrone avec async/await

## Le problème : les opérations qui attendent

Certaines opérations prennent du temps : appeler une API sur le réseau, lire un gros fichier, interroger une base de données. Si on les exécute de manière **synchrone**, le programme est bloqué pendant toute la durée de l'attente — le thread principal ne peut rien faire d'autre.

La programmation **asynchrone** permet de lancer une telle opération et de **libérer le thread** pendant qu'elle s'exécute en arrière-plan, pour reprendre là où on en était une fois le résultat disponible.

## `Task` et `Task<T>`

En C#, une opération asynchrone est représentée par un objet `Task` (sans résultat) ou `Task<T>` (avec un résultat de type `T`). C'est la "promesse" que quelque chose se terminera dans le futur.

## `async` et `await`

- `async` sur une méthode indique qu'elle contient des opérations asynchrones.
- `await` devant un `Task` suspend l'exécution **de la méthode courante** jusqu'à ce que le `Task` soit terminé — sans bloquer le thread principal.

```csharp
using System.Net.Http;

HttpClient client = new HttpClient();

// Version synchrone (bloque le thread) :
// string contenu = client.GetStringAsync(url).Result; // ❌ à éviter

// Version asynchrone :
static async Task<string> TelechargerAsync(string url)
{
    using HttpClient client = new HttpClient();
    string contenu = await client.GetStringAsync(url);
    return contenu;
}
```

La méthode déclarée `async` **doit** retourner `Task`, `Task<T>` ou `void` (les méthodes `async void` sont à éviter sauf pour les gestionnaires d'événements, car les exceptions qu'elles lèvent ne peuvent pas être capturées).

## Appel depuis le programme principal

En .NET 7+ avec les "top-level statements", `await` est directement utilisable au niveau supérieur :

```csharp
string json = await TelechargerAsync("https://jsonplaceholder.typicode.com/todos/1");
Console.WriteLine(json);
```

Le compilateur enveloppe automatiquement le code top-level dans une méthode `async Task Main(...)`.

## Désérialiser du JSON avec `System.Text.Json`

`System.Text.Json` est inclus dans .NET sans package supplémentaire. On définit une classe qui correspond à la structure JSON attendue, puis on désérialise :

```csharp
using System.Text.Json;

record Todo(int Id, string Title, bool Completed);

string json = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos/1");
Todo? todo = JsonSerializer.Deserialize<Todo>(json, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true // "title" en JSON → Title en C#
});

Console.WriteLine($"{todo?.Id} : {todo?.Title} (terminé : {todo?.Completed})");
```

## Gérer les erreurs

`await` propage les exceptions exactement comme du code synchrone — on les attrape avec `try`/`catch` :

```csharp
try
{
    string json = await TelechargerAsync("https://url-invalide.example");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Erreur réseau : {ex.Message}");
}
```

## Exécuter plusieurs tâches en parallèle

`Task.WhenAll` attend que **toutes** les tâches passées soient terminées, en les exécutant en parallèle — bien plus rapide que d'`await`er l'une après l'autre :

```csharp
Task<string> t1 = TelechargerAsync("https://jsonplaceholder.typicode.com/todos/1");
Task<string> t2 = TelechargerAsync("https://jsonplaceholder.typicode.com/todos/2");
Task<string> t3 = TelechargerAsync("https://jsonplaceholder.typicode.com/todos/3");

string[] resultats = await Task.WhenAll(t1, t2, t3);

foreach (string r in resultats)
    Console.WriteLine(r);
```

## Pièges à éviter

| À éviter | Pourquoi | Préférer |
|---|---|---|
| `.Result` ou `.Wait()` sur un `Task` | Bloque le thread, risque de deadlock | `await` |
| `async void` (sauf événements) | Les exceptions échappent silencieusement | `async Task` |
| `await` dans une boucle `foreach` séquentielle quand les appels sont indépendants | Lent : chaque appel attend le précédent | `Task.WhenAll` |

**Suite : [18. Accès aux données avec Entity Framework Core](18-entity-framework-core.md)**
