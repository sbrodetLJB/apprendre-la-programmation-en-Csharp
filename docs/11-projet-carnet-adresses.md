# 11. Mini-projet de synthèse : carnet d'adresses console

## Objectif

Ce projet réunit toutes les notions vues depuis la leçon 1 : variables, boucles, méthodes, collections, classes, encapsulation, héritage et exceptions. On va construire un **carnet d'adresses en ligne de commande**, entièrement en mémoire (pas de fichier, pas de base de données, pas de réseau — uniquement de la POO).

C'est volontairement le **même problème** que le cours [apprendre_les_API](https://github.com/sbrodetLJB/apprendre_les_API) (qui modélisait un carnet d'adresses via une API REST en FastAPI/PHP). Ici, on modélise la même idée, mais entièrement côté C#, sans API : c'est l'occasion de voir comment un même besoin se traduit différemment selon le contexte technique.

## Modélisation

### La classe `Contact`

```csharp
class Contact
{
    public int Id { get; }
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public string? Telephone { get; set; } // optionnel

    public Contact(int id, string prenom, string nom, string email, string? telephone = null)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("L'email ne peut pas être vide.");
        }

        Id = id;
        Prenom = prenom;
        Nom = nom;
        Email = email;
        Telephone = telephone;
    }

    public string NomComplet => $"{Prenom} {Nom}";

    public override string ToString()
    {
        string tel = Telephone ?? "non renseigné";
        return $"[{Id}] {NomComplet} - {Email} - {tel}";
    }
}
```

Remarques :
- `Id { get; }` (sans `set`) : une fois assigné dans le constructeur, l'identifiant ne peut plus jamais être modifié — il est en lecture seule depuis l'extérieur.
- Le constructeur lève une `ArgumentException` si l'email est vide : on protège l'objet contre un état incohérent dès sa création (encapsulation).
- `override string ToString()` redéfinit la méthode `ToString()` héritée (implicitement) de la classe de base universelle `object` — toute classe C# en hérite. La redéfinir permet d'afficher un contact proprement avec `Console.WriteLine(contact)`.

### La classe `CarnetAdresses`

```csharp
class CarnetAdresses
{
    private List<Contact> contacts = new List<Contact>();
    private int prochainId = 1;

    public Contact Ajouter(string prenom, string nom, string email, string? telephone = null)
    {
        Contact contact = new Contact(prochainId, prenom, nom, email, telephone);
        contacts.Add(contact);
        prochainId++;
        return contact;
    }

    public bool Supprimer(int id)
    {
        Contact? contact = contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            return false;
        }
        contacts.Remove(contact);
        return true;
    }

    public Contact? Rechercher(int id)
    {
        return contacts.FirstOrDefault(c => c.Id == id);
    }

    public List<Contact> RechercherParNom(string motCle)
    {
        return contacts
            .Where(c => c.NomComplet.Contains(motCle, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Contact> ListerTout()
    {
        return contacts;
    }
}
```

`CarnetAdresses` encapsule la `List<Contact>` (elle est `private`) : tout code extérieur doit obligatoirement passer par `Ajouter`, `Supprimer`, `Rechercher`... pour interagir avec les contacts. Il n'y a aucun moyen de manipuler la liste interne de façon incohérente depuis l'extérieur.

## Le menu console

```csharp
CarnetAdresses carnet = new CarnetAdresses();
bool continuer = true;

while (continuer)
{
    Console.WriteLine();
    Console.WriteLine("=== Carnet d'adresses ===");
    Console.WriteLine("1. Ajouter un contact");
    Console.WriteLine("2. Lister tous les contacts");
    Console.WriteLine("3. Rechercher par nom");
    Console.WriteLine("4. Supprimer un contact");
    Console.WriteLine("5. Quitter");
    Console.Write("Ton choix : ");

    string? choix = Console.ReadLine();

    switch (choix)
    {
        case "1":
            // TODO : demander prénom, nom, email, téléphone, puis carnet.Ajouter(...)
            // Attention : gérer le cas où Ajouter lève une ArgumentException (email vide)
            break;
        case "2":
            // TODO : afficher tous les contacts avec carnet.ListerTout()
            break;
        case "3":
            // TODO : demander un mot-clé, afficher carnet.RechercherParNom(motCle)
            break;
        case "4":
            // TODO : demander un id (avec int.TryParse !), appeler carnet.Supprimer(id)
            break;
        case "5":
            continuer = false;
            break;
        default:
            Console.WriteLine("Choix invalide.");
            break;
    }
}
```

Ce squelette est le point de départ du TP de cette leçon : à toi d'écrire les `TODO` pour obtenir un carnet d'adresses fonctionnel.

## Pour aller plus loin (hors TP, à explorer librement)

- Ajouter une classe `ContactProfessionnel : Contact` avec un champ `Entreprise` en plus (héritage).
- Sauvegarder/charger le carnet dans un fichier texte ou JSON (`System.IO`, `System.Text.Json`).
- Comparer cette version avec l'API REST du cours [apprendre_les_API](https://github.com/sbrodetLJB/apprendre_les_API) : que change le fait d'avoir un réseau et plusieurs clients potentiels ?

**Suite vers les leçons avancées : [12. Tests unitaires avec xUnit](12-tests-unitaires-xunit.md)**
