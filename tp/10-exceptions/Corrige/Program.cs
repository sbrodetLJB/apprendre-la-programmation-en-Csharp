// TP 10 : Gestion des exceptions -- Corrigé

Console.Write("Premier nombre : ");
int.TryParse(Console.ReadLine(), out int a);
Console.Write("Second nombre : ");
int.TryParse(Console.ReadLine(), out int b);

try
{
    Console.WriteLine($"Résultat : {a / b}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Impossible de diviser par zéro.");
}
finally
{
    Console.WriteLine("Opération terminée.");
}

try
{
    double moyenne = CalculerMoyenne(Array.Empty<int>());
    Console.WriteLine(moyenne);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erreur : {ex.Message}");
}

try
{
    Deposer(100, -50);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erreur : {ex.Message}");
}

static double CalculerMoyenne(int[] notes)
{
    if (notes.Length == 0)
    {
        throw new ArgumentException("Le tableau de notes ne peut pas être vide.");
    }

    int somme = 0;
    foreach (int note in notes)
    {
        somme += note;
    }
    return (double)somme / notes.Length;
}

static void Deposer(double solde, double montant)
{
    if (montant <= 0)
    {
        throw new ArgumentException("Le montant doit être positif.");
    }
    Console.WriteLine($"Nouveau solde : {solde + montant}");
}
