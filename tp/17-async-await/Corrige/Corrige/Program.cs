// TP 17 : async/await -- Corrigé

using System.Net.Http;
using System.Text.Json;

const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

// 1. Télécharger et afficher le JSON brut
Console.WriteLine("=== 1. JSON brut ===");
string json = await TelechargerAsync(BaseUrl + "1");
Console.WriteLine(json);

// 2. Désérialiser et afficher les champs
Console.WriteLine("\n=== 2. Désérialisé ===");
Todo? todo = JsonSerializer.Deserialize<Todo>(json, options);
Console.WriteLine($"Id : {todo?.Id} | Titre : {todo?.Title} | Terminé : {todo?.Completed}");

// 3. Trois appels en parallèle avec Task.WhenAll
Console.WriteLine("\n=== 3. Task.WhenAll ===");
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

// 4. Gestion d'erreur avec une URL invalide
Console.WriteLine("\n=== 4. Gestion d'erreur ===");
try
{
    await TelechargerAsync("https://url-qui-nexiste-vraiment-pas.invalid/");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Erreur réseau : {ex.Message}");
}

// ---- Méthode asynchrone ----

static async Task<string> TelechargerAsync(string url)
{
    using HttpClient client = new HttpClient();
    return await client.GetStringAsync(url);
}

// ---- Type ----

record Todo(int Id, string Title, bool Completed);
