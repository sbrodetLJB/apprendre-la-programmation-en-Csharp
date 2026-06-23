// TP 11 : Mini-projet de synthèse -- Carnet d'adresses console
//
// Objectif : terminer le carnet d'adresses présenté dans la leçon 11.
// Les classes Contact et CarnetAdresses ci-dessous sont déjà écrites (recopiées du cours).
// Ton travail : compléter le menu pour que les 4 actions fonctionnent réellement.
//
// Rappels :
// - carnet.Ajouter(prenom, nom, email, telephone) peut lever une ArgumentException
//   si l'email est vide -> entoure l'appel d'un try/catch.
// - carnet.ListerTout() renvoie une List<Contact> -> parcours-la avec foreach et
//   Console.WriteLine(contact) (le ToString() est déjà défini).
// - carnet.RechercherParNom(motCle) renvoie une List<Contact> correspondant à la recherche.
// - carnet.Supprimer(id) renvoie true/false -> utilise int.TryParse pour lire l'id saisi.

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

class Contact
{
    public int Id { get; }
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public string? Telephone { get; set; }

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
