namespace CarnetAdresses;

public class CarnetAdresses
{
    private List<Contact> contacts = new List<Contact>();
    private int prochainId = 1;

    public Contact Ajouter(string prenom, string nom, string email, string? telephone = null)
    {
        var contact = new Contact(prochainId, prenom, nom, email, telephone);
        contacts.Add(contact);
        prochainId++;
        return contact;
    }

    public List<Contact> ListerTout()
    {
        return contacts;
    }

    public bool Supprimer(int id)
    {
        var contact = contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null) return false;
        contacts.Remove(contact);
        return true;
    }

    public List<Contact> RechercherParNom(string motCle)
    {
        return contacts
            .Where(c => c.NomComplet.Contains(motCle, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
