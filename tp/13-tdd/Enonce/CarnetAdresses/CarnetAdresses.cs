// Stub TDD : toutes les méthodes sont intentionnellement incomplètes.
// Le cycle TDD : pour chaque TODO dans les tests, fais passer le test au minimum,
// puis refactorise si nécessaire avant de passer au suivant.

namespace CarnetAdresses;

public class CarnetAdresses
{
    private List<Contact> contacts = new List<Contact>();
    private int prochainId = 1;

    // TODO TDD — implémenter au fur et à mesure, guidé par les tests

    public Contact Ajouter(string prenom, string nom, string email, string? telephone = null)
    {
        throw new NotImplementedException();
    }

    public List<Contact> ListerTout()
    {
        throw new NotImplementedException();
    }

    public bool Supprimer(int id)
    {
        throw new NotImplementedException();
    }

    public List<Contact> RechercherParNom(string motCle)
    {
        throw new NotImplementedException();
    }
}
