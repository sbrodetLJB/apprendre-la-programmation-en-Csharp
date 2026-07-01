// TP 16 : LINQ
//
// Une liste de contacts et une liste de livres sont déjà créées ci-dessous.
// Pour chaque question, écris la requête LINQ correspondante.
// N'oublie pas "using System.Linq;" (déjà présent en haut via ImplicitUsings).
//
// 1. Where  : filtre les contacts dont le nom de famille est "Martin". Affiche-les.
// 2. Select : projette la liste de contacts en une List<string> de noms complets
//             (format "Prénom NOM"). Affiche-les.
// 3. OrderBy : trie les livres par NombrePages croissant et affiche Titre + pages.
// 4. Any / All :
//    - affiche si au moins un contact a un email "@example.com" (Any)
//    - affiche si tous les livres ont plus de 50 pages (All)
// 5. Count / Sum / Average / Min / Max :
//    sur la liste de notes { 12, 15, 9, 18, 7, 14 }, affiche chacune de ces valeurs.
// 6. GroupBy : regroupe les contacts par nom de famille.
//    Pour chaque groupe, affiche "Famille X (N contact(s)) : prénom1, prénom2..."
// 7. Bonus — chaîner : renvoie les prénoms (Select) des contacts dont le nom est "Martin"
//    (Where), triés alphabétiquement (OrderBy), dans une List<string>.

Console.WriteLine("TODO : remplace cette ligne par ton code");

// ---- Données pour le TP ----

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
