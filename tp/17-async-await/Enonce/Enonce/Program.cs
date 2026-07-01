// TP 17 : async/await
//
// Ce TP appelle l'API publique https://jsonplaceholder.typicode.com (aucune clé nécessaire).
// Exécute avec : dotnet run
//
// 1. Écris une méthode "static async Task<string> TelechargerAsync(string url)"
//    qui utilise un HttpClient pour télécharger le contenu d'une URL et le renvoyer.
//    Appelle-la avec l'URL ci-dessous et affiche le JSON brut.
//
// 2. Désérialise le JSON obtenu en un objet "Todo" (record déjà défini ci-dessous)
//    avec JsonSerializer.Deserialize<Todo>(..., options).
//    Affiche "Id : X | Titre : Y | Terminé : Z".
//
// 3. Télécharge 3 todos en parallèle (ids 1, 2 et 3) avec Task.WhenAll.
//    Affiche les 3 titres.
//
// 4. Ajoute un try/catch autour de l'un de tes appels pour gérer HttpRequestException
//    (simule une URL invalide pour tester).

using System.Net.Http;
using System.Text.Json;

const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";

// TODO 1 : appeler TelechargerAsync et afficher le JSON brut
// TODO 2 : désérialiser et afficher les champs du Todo
// TODO 3 : Task.WhenAll sur 3 URLs
// TODO 4 : try/catch sur une URL invalide

// ---- Types déjà définis ----

record Todo(int Id, string Title, bool Completed);

// TODO : déclare TelechargerAsync ici
