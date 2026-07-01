# Pas à pas — TP 17 : async/await

Relis le cours [17-async-await.md](../../docs/17-async-await.md) avant de commencer. Place-toi dans `tp/17-async-await/Enonce/Enonce/`. Ce TP nécessite une **connexion internet** pour appeler l'API jsonplaceholder.typicode.com.

## TODO : déclarer `TelechargerAsync`

Avant tout, écris la méthode asynchrone en bas du fichier. Le cours la présente en détail :

```csharp
static async Task<string> TelechargerAsync(string url)
{
    using HttpClient client = new HttpClient();
    return await client.GetStringAsync(url);
}
```

`using` sur le `HttpClient` garantit que la connexion est fermée proprement après usage. `GetStringAsync` est une méthode asynchrone fournie par .NET — on l'`await` pour que le programme reprenne une fois la réponse reçue, sans bloquer le thread.

## TODO 1 : télécharger et afficher le JSON brut

Maintenant, en haut du fichier, appelle la méthode. Rappelle-toi : dans un fichier "top-level statements" (notre style depuis le TP1), `await` est directement utilisable :

```csharp
string json = await TelechargerAsync(BaseUrl + "1");
Console.WriteLine(json);
```

Lance `dotnet run`. Tu dois voir un JSON brut comme :
```json
{ "userId": 1, "id": 1, "title": "delectus aut autem", "completed": false }
```

Si tu vois une erreur `HttpRequestException`, vérifie ta connexion internet.

## TODO 2 : désérialiser le JSON

`System.Text.Json.JsonSerializer.Deserialize<T>` convertit une chaîne JSON en objet C#. Il faut une classe ou un `record` dont les propriétés correspondent aux champs JSON. Le `record Todo` est déjà défini dans l'Enoncé.

```csharp
var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
Todo? todo = JsonSerializer.Deserialize<Todo>(json, options);
Console.WriteLine($"Id : {todo?.Id} | Titre : {todo?.Title} | Terminé : {todo?.Completed}");
```

`PropertyNameCaseInsensitive = true` permet de faire correspondre `"title"` (JSON, minuscule) avec `Title` (C#, majuscule) — sans cette option, la désérialisation retournerait `null` pour ce champ.

## TODO 3 : trois appels en parallèle avec `Task.WhenAll`

Sans `Task.WhenAll`, trois appels séquentiels attendent l'un après l'autre :
```csharp
// ❌ séquentiel : 3x le temps d'un appel
string j1 = await TelechargerAsync(BaseUrl + "1");
string j2 = await TelechargerAsync(BaseUrl + "2");
string j3 = await TelechargerAsync(BaseUrl + "3");
```

Avec `Task.WhenAll`, les trois partent en même temps et on attend qu'ils soient **tous** terminés — bien plus rapide :

```csharp
// ✅ parallèle : environ 1x le temps d'un appel
string[] jsons = await Task.WhenAll(
    TelechargerAsync(BaseUrl + "1"),
    TelechargerAsync(BaseUrl + "2"),
    TelechargerAsync(BaseUrl + "3")
);
foreach (string j in jsons)
{
    Todo? t = JsonSerializer.Deserialize<Todo>(j, options);
    Console.WriteLine($"[{t?.Id}] {t?.Title}");
}
```

## TODO 4 : gestion d'erreur avec `try`/`catch`

`HttpRequestException` est l'exception levée par `HttpClient` en cas de problème réseau (URL inaccessible, pas de connexion, erreur HTTP). Elle se capture exactement comme n'importe quelle exception (TP10) :

```csharp
try
{
    await TelechargerAsync("https://url-qui-nexiste-vraiment-pas.invalid/");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Erreur réseau : {ex.Message}");
}
```

Le programme doit afficher un message d'erreur clair **sans planter**.

## Vérifier son travail

Lance `dotnet run`. Tu dois voir successivement : le JSON brut (TODO 1), la ligne désérialisée (TODO 2), les titres des 3 todos (TODO 3), puis le message d'erreur propre (TODO 4). Compare avec `../Corrige/Program.cs`.

**Suite : [TP 18 — Entity Framework Core](../18-entity-framework/PasAPas.md)**
