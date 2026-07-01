// TP 16 : LINQ -- Corrigé

var contacts = new List<Contact>
{
    new Contact("Lucas",  "Martin",  "lucas@example.com"),
    new Contact("Emma",   "Bernard", "emma@example.com"),
    new Contact("Nathan", "Martin",  "nathan@example.com"),
    new Contact("Alice",  "Dupont",  "alice@example.com"),
    new Contact("Zoé",    "Bernard", "zoe@example.com"),
};

var livres = new List<Livre>
{
    new Livre("Le Petit Prince",   96),
    new Livre("1984",             328),
    new Livre("Dune",             412),
    new Livre("L'Étranger",       159),
};

// 1. Where — filtrer les Martin
Console.WriteLine("=== 1. Where ===");
var martin = contacts.Where(c => c.Nom == "Martin").ToList();
martin.ForEach(Console.WriteLine);

// 2. Select — projeter en noms complets
Console.WriteLine("\n=== 2. Select ===");
var nomsComplets = contacts.Select(c => $"{c.Prenom} {c.Nom.ToUpper()}").ToList();
nomsComplets.ForEach(Console.WriteLine);

// 3. OrderBy — trier les livres par pages
Console.WriteLine("\n=== 3. OrderBy ===");
var parPages = livres.OrderBy(l => l.NombrePages).ToList();
parPages.ForEach(l => Console.WriteLine($"{l.Titre} ({l.NombrePages} p.)"));

// 4. Any / All
Console.WriteLine("\n=== 4. Any / All ===");
bool auMoinsUnExample = contacts.Any(c => c.Email.Contains("@example.com"));
bool tousPlus50Pages  = livres.All(l => l.NombrePages > 50);
Console.WriteLine($"Au moins un @example.com : {auMoinsUnExample}");
Console.WriteLine($"Tous les livres > 50 pages : {tousPlus50Pages}");

// 5. Agrégats sur des notes
Console.WriteLine("\n=== 5. Agrégats ===");
var notes = new List<int> { 12, 15, 9, 18, 7, 14 };
Console.WriteLine($"Count   : {notes.Count()}");
Console.WriteLine($"Sum     : {notes.Sum()}");
Console.WriteLine($"Average : {notes.Average():F1}");
Console.WriteLine($"Min     : {notes.Min()}");
Console.WriteLine($"Max     : {notes.Max()}");

// 6. GroupBy — par nom de famille
Console.WriteLine("\n=== 6. GroupBy ===");
var parFamille = contacts.GroupBy(c => c.Nom);
foreach (var groupe in parFamille)
{
    string prenoms = string.Join(", ", groupe.Select(c => c.Prenom));
    Console.WriteLine($"Famille {groupe.Key} ({groupe.Count()} contact(s)) : {prenoms}");
}

// 7. Bonus — chaîner
Console.WriteLine("\n=== 7. Bonus — chaîner ===");
var prenomsMartinTries = contacts
    .Where(c => c.Nom == "Martin")
    .OrderBy(c => c.Prenom)
    .Select(c => c.Prenom)
    .ToList();
Console.WriteLine(string.Join(", ", prenomsMartinTries));

// ---- Classes ----

class Contact
{
    public string Prenom { get; }
    public string Nom    { get; }
    public string Email  { get; }
    public Contact(string prenom, string nom, string email)
        => (Prenom, Nom, Email) = (prenom, nom, email);
    public override string ToString() => $"{Prenom} {Nom} <{Email}>";
}

class Livre
{
    public string Titre       { get; }
    public int    NombrePages { get; }
    public Livre(string titre, int nombrePages)
        => (Titre, NombrePages) = (titre, nombrePages);
}
