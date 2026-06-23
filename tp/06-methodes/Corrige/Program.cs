// TP 6 : Méthodes -- Corrigé

Console.WriteLine(Carre(5));        // 25
Console.WriteLine(EstPair(7));      // False
Console.WriteLine(Moyenne(12, 15, 9)); // 12

AfficherLigne(5);
AfficherLigne(5, '#');

Outils.Decrire(42);
Outils.Decrire("Bonjour");

static int Carre(int n)
{
    return n * n;
}

static bool EstPair(int n)
{
    return n % 2 == 0;
}

static double Moyenne(double a, double b, double c)
{
    return (a + b + c) / 3;
}

static void AfficherLigne(int longueur, char symbole = '*')
{
    for (int i = 0; i < longueur; i++)
    {
        Console.Write(symbole);
    }
    Console.WriteLine();
}

// Les fonctions locales ne supportent pas la surcharge : on regroupe Decrire dans une classe.
static class Outils
{
    public static void Decrire(int valeur)
    {
        Console.WriteLine($"{valeur} est un nombre entier.");
    }

    public static void Decrire(string valeur)
    {
        Console.WriteLine($"\"{valeur}\" est une chaîne de {valeur.Length} caractères.");
    }
}
