# Pas à pas — TP 11 : Mini-projet de synthèse — Carnet d'adresses console

Relis le cours [11-projet-carnet-adresses.md](../../docs/11-projet-carnet-adresses.md) avant de commencer : il explique en détail les classes `Contact` et `CarnetAdresses` que tu vas utiliser ici. Place-toi dans `tp/11-projet-carnet-adresses/Enonce/`.

## Ce qui est déjà fait pour toi

Contrairement aux TP précédents, `Program.cs` contient déjà :
- la classe `Contact` (avec son constructeur qui lève une `ArgumentException` si l'email est vide — vu au TP10) ;
- la classe `CarnetAdresses` (qui encapsule une `List<Contact>` privée — vu au TP8/9, avec les méthodes `Ajouter`, `Supprimer`, `Rechercher`, `RechercherParNom`, `ListerTout`) ;
- la boucle du menu, avec un `switch` (vu au TP4) sur le choix de l'utilisateur.

Ton travail : remplir les 4 `// TODO` dans les `case` du `switch`, pour que chaque option du menu fonctionne réellement. Ne touche pas aux classes `Contact` et `CarnetAdresses` : tout ce que tu dois écrire se trouve dans la boucle `while (continuer) { ... }`.

Avant de commencer, lance `dotnet run` une fois pour voir le menu s'afficher (les options ne feront encore rien).

## TODO 1 : ajouter un contact (`case "1"`)

Il faut demander chaque information un par un, comme au TP3, puis appeler `carnet.Ajouter(...)`.

```csharp
case "1":
    Console.Write("Prénom : ");
    string? prenom = Console.ReadLine() ?? "";
    Console.Write("Nom : ");
    string? nom = Console.ReadLine() ?? "";
    Console.Write("Email : ");
    string? email = Console.ReadLine() ?? "";
    Console.Write("Téléphone (optionnel) : ");
    string? telephone = Console.ReadLine();
    telephone = string.IsNullOrWhiteSpace(telephone) ? null : telephone;

    try
    {
        Contact nouveau = carnet.Ajouter(prenom, nom, email, telephone);
        Console.WriteLine($"Contact ajouté : {nouveau}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Impossible d'ajouter ce contact : {ex.Message}");
    }
    break;
```

Décortiquons les parties nouvelles :
- `Console.ReadLine() ?? ""` : `??` est l'opérateur "valeur de remplacement si null", vu rapidement dans le code du cours (`Telephone ?? "non renseigné"`). Ici, si `ReadLine()` renvoie `null`, on utilise `""` à la place, pour avoir un `string` garanti non-`null` (les propriétés `Prenom`/`Nom`/`Email` de `Contact` attendent un `string`, pas un `string?`).
- Le téléphone reste optionnel : si l'utilisateur ne tape rien (`string.IsNullOrWhiteSpace` vrai), on le force à `null` plutôt que de garder une chaîne vide — c'est cohérent avec `string? Telephone` dans `Contact`, qui distingue "non renseigné" (`null`) d'une chaîne vide.
- L'appel `carnet.Ajouter(...)` est dans un `try`/`catch` : le cours rappelle que le constructeur de `Contact` lève une `ArgumentException` si l'email est vide (revoir TP10) — sans ce `try`/`catch`, taper un email vide ferait planter tout le programme.

Teste : ajoute un contact valide (doit afficher "Contact ajouté : [...]"), puis un contact avec un email vide (doit afficher le message d'erreur, **sans planter**).

## TODO 2 : lister tous les contacts (`case "2"`)

`carnet.ListerTout()` renvoie une `List<Contact>` (cours). Affiche-la avec un `foreach` (TP7/8) : pas besoin d'accéder aux propriétés une par une, `Contact.ToString()` est déjà défini dans la classe (cours, section "La classe Contact") pour afficher proprement un contact directement avec `Console.WriteLine(contact)`.

```csharp
case "2":
    List<Contact> tous = carnet.ListerTout();
    if (tous.Count == 0)
    {
        Console.WriteLine("Le carnet est vide.");
    }
    foreach (Contact contact in tous)
    {
        Console.WriteLine(contact);
    }
    break;
```

Le `if (tous.Count == 0)` n'est pas strictement obligatoire (sans lui, la liste vide n'afficherait simplement rien), mais c'est un meilleur retour pour l'utilisateur qui ne verrait sinon aucune ligne et pourrait croire à un bug.

## TODO 3 : rechercher par nom (`case "3"`)

Même principe : demander un mot-clé, puis afficher le résultat de `carnet.RechercherParNom(motCle)` (qui renvoie aussi une `List<Contact>`, cours).

```csharp
case "3":
    Console.Write("Mot-clé à rechercher : ");
    string motCle = Console.ReadLine() ?? "";
    List<Contact> resultats = carnet.RechercherParNom(motCle);
    if (resultats.Count == 0)
    {
        Console.WriteLine("Aucun contact trouvé.");
    }
    foreach (Contact contact in resultats)
    {
        Console.WriteLine(contact);
    }
    break;
```

Teste en recherchant un mot-clé qui correspond à un contact existant, puis un mot-clé qui ne correspond à rien.

## TODO 4 : supprimer un contact (`case "4"`)

`carnet.Supprimer(id)` attend un `int` (cours) et renvoie `true`/`false` selon que la suppression a réussi. Comme la saisie clavier est toujours un `string`, il faut la convertir avec `int.TryParse` (vu au TP3) — l'énoncé insiste sur ce point.

```csharp
case "4":
    Console.Write("Id du contact à supprimer : ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        bool supprime = carnet.Supprimer(id);
        Console.WriteLine(supprime ? "Contact supprimé." : "Aucun contact avec cet id.");
    }
    else
    {
        Console.WriteLine("Id invalide.");
    }
    break;
```

Remarque l'opérateur ternaire (TP2/4) pour choisir le message selon le `bool` renvoyé par `Supprimer`.

Teste trois cas : un id existant (doit supprimer), un id qui n'existe pas comme `999` (doit afficher "Aucun contact avec cet id."), et une saisie non-numérique comme `"abc"` (doit afficher "Id invalide.", sans planter).

## Vérifier son travail de bout en bout

Relance `dotnet run` et déroule un scénario complet :
1. Ajoute 2-3 contacts (avec et sans téléphone).
2. Liste-les tous (option 2).
3. Recherche un mot-clé qui correspond à l'un d'eux (option 3).
4. Supprime-en un par son id (option 4), puis liste à nouveau pour vérifier qu'il a bien disparu.
5. Essaie d'ajouter un contact avec un email vide pour vérifier que le programme ne plante pas.
6. Quitte avec l'option 5.

Si tout se comporte comme attendu, compare avec `../Corrige/Program.cs`. Bravo : tu as terminé toute la progression, des `Console.WriteLine` du TP1 jusqu'à un mini-projet orienté objet complet !
