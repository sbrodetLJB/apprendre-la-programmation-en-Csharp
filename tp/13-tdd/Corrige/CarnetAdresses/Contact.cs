namespace CarnetAdresses;

public class Contact
{
    public int Id { get; }
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public string? Telephone { get; set; }

    public Contact(int id, string prenom, string nom, string email, string? telephone = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("L'email ne peut pas être vide.");

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
