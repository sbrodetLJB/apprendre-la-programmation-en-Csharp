// TP 3 : Entrées/sorties et conversions -- Corrigé

Console.Write("Quel est ton prénom ? ");
string? prenom = Console.ReadLine();
Console.WriteLine($"Bonjour {prenom} !");

Console.Write("Combien de cours suis-tu cette semaine ? ");
string? saisieCours = Console.ReadLine();
if (int.TryParse(saisieCours, out int nombreCours))
{
    Console.WriteLine($"Cela représente environ {nombreCours} heures de cours.");
}
else
{
    Console.WriteLine("Saisie invalide.");
}

Console.Write("Quel est le prix HT d'un article ? ");
string? saisiePrix = Console.ReadLine();
if (double.TryParse(saisiePrix, out double prixHT))
{
    double prixTTC = prixHT * 1.20;
    Console.WriteLine($"Prix TTC : {prixTTC} €");
}
else
{
    Console.WriteLine("Saisie invalide.");
}

Console.Write("Premier nombre : ");
int.TryParse(Console.ReadLine(), out int a);
Console.Write("Second nombre : ");
int.TryParse(Console.ReadLine(), out int b);

Console.WriteLine($"Somme : {a + b}");
Console.WriteLine($"Différence : {a - b}");
Console.WriteLine($"Produit : {a * b}");
