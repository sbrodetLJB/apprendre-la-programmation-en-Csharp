// TP 11 : Mini-projet de synthèse -- Carnet d'adresses console -- Corrigé

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
