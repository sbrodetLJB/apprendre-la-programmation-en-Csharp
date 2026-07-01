// TP 18 : Entity Framework Core avec SQLite
//
// Le DbContext et la classe Contact sont déjà définis en bas du fichier.
// Exécute avec : dotnet run  (le fichier carnet.db est créé automatiquement)
//
// 1. Crée le contexte, appelle EnsureCreated(), puis ajoute 3 contacts avec
//    context.Contacts.Add(...) et context.SaveChanges().
//    Affiche "Base créée et contacts ajoutés."
//
// 2. Lis tous les contacts avec context.Contacts.ToList() et affiche-les.
//
// 3. Modifie l'email du premier contact (Id == 1) et appelle SaveChanges().
//    Affiche le contact modifié.
//
// 4. Supprime le contact dont le prénom est "Nathan" et appelle SaveChanges().
//    Affiche le nombre de contacts restants.
//
// 5. Bonus : utilise une requête LINQ Where + OrderBy sur context.Contacts
//    pour lister les contacts dont le nom est "Martin", triés par prénom.

using Microsoft.EntityFrameworkCore;

// TODO 1 : EnsureCreated + Add + SaveChanges
// TODO 2 : ToList + affichage
// TODO 3 : modifier email + SaveChanges
// TODO 4 : supprimer + SaveChanges + Count
// TODO 5 (bonus) : LINQ Where + OrderBy

// ---- Modèle et DbContext déjà définis ----

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
