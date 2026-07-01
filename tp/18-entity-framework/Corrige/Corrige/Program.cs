// TP 18 : Entity Framework Core avec SQLite -- Corrigé

using Microsoft.EntityFrameworkCore;

// 1. Créer la base et ajouter 3 contacts
using (var context = new CarnetContext())
{
    context.Database.EnsureCreated();

    // Vider la table pour que le programme soit rejouable proprement
    context.Contacts.RemoveRange(context.Contacts);
    context.SaveChanges();

    context.Contacts.Add(new Contact { Prenom = "Lucas",  Nom = "Martin",  Email = "lucas@example.com" });
    context.Contacts.Add(new Contact { Prenom = "Emma",   Nom = "Bernard", Email = "emma@example.com" });
    context.Contacts.Add(new Contact { Prenom = "Nathan", Nom = "Martin",  Email = "nathan@example.com" });
    context.SaveChanges();
    Console.WriteLine("Base créée et contacts ajoutés.");
}

// 2. Lire et afficher tous les contacts
Console.WriteLine("\n=== Tous les contacts ===");
using (var context = new CarnetContext())
{
    List<Contact> tous = context.Contacts.ToList();
    tous.ForEach(Console.WriteLine);
}

// 3. Modifier l'email du contact Id == 1
Console.WriteLine("\n=== Après modification ===");
using (var context = new CarnetContext())
{
    Contact? lucas = context.Contacts.FirstOrDefault(c => c.Id == 1);
    if (lucas != null)
    {
        lucas.Email = "lucas.martin@nouveau.com";
        context.SaveChanges();
        Console.WriteLine(lucas);
    }
}

// 4. Supprimer Nathan et afficher le nombre restant
Console.WriteLine("\n=== Après suppression ===");
using (var context = new CarnetContext())
{
    Contact? nathan = context.Contacts.FirstOrDefault(c => c.Prenom == "Nathan");
    if (nathan != null)
    {
        context.Contacts.Remove(nathan);
        context.SaveChanges();
    }
    Console.WriteLine($"Contacts restants : {context.Contacts.Count()}");
}

// 5. Bonus : LINQ Where + OrderBy sur DbSet
Console.WriteLine("\n=== Bonus : LINQ EF Core ===");
using (var context = new CarnetContext())
{
    var martin = context.Contacts
        .Where(c => c.Nom == "Martin")
        .OrderBy(c => c.Prenom)
        .ToList();
    martin.ForEach(Console.WriteLine);
}

// ---- Modèle et DbContext ----

class Contact
{
    public int     Id        { get; set; }
    public string  Prenom    { get; set; } = "";
    public string  Nom       { get; set; } = "";
    public string  Email     { get; set; } = "";
    public string? Telephone { get; set; }

    public override string ToString() =>
        $"[{Id}] {Prenom} {Nom} - {Email}" +
        (Telephone is null ? "" : $" - {Telephone}");
}

class CarnetContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=carnet.db");
}
