// TP 8 : POO -- classes, objets, encapsulation -- Corrigé

List<Livre> livres = new List<Livre>
{
    new Livre("Le Petit Prince", "Antoine de Saint-Exupéry", 96),
    new Livre("1984", "George Orwell", 328),
    new Livre("Dune", "Frank Herbert", 412),
};

foreach (Livre livre in livres)
{
    Console.WriteLine($"{livre.Titre} - {livre.Auteur} ({livre.NombrePages} pages)");
}

Console.WriteLine();
CompteBancaire compte = new CompteBancaire(100);
compte.Deposer(50);
compte.Retirer(30);
compte.Retirer(1000); // impossible : solde insuffisant
Console.WriteLine($"Solde final : {compte.ConsulterSolde()}");

class Livre
{
    public string Titre { get; set; }
    public string Auteur { get; set; }
    public int NombrePages { get; set; }

    public Livre(string titre, string auteur, int nombrePages)
    {
        Titre = titre;
        Auteur = auteur;
        NombrePages = nombrePages;
    }
}

class CompteBancaire
{
    public double Solde { get; private set; }

    public CompteBancaire(double soldeInitial)
    {
        Solde = soldeInitial;
    }

    public void Deposer(double montant)
    {
        if (montant > 0)
        {
            Solde += montant;
        }
    }

    public void Retirer(double montant)
    {
        if (montant > 0 && montant <= Solde)
        {
            Solde -= montant;
        }
        else
        {
            Console.WriteLine("Retrait impossible.");
        }
    }

    public double ConsulterSolde()
    {
        return Solde;
    }
}
