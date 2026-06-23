// TP 5 : Boucles -- Corrigé

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(i);
}

Console.WriteLine();
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"7x{i}={7 * i}");
}

Console.WriteLine();
int somme = 0;
int n = 1;
while (n <= 100)
{
    somme += n;
    n++;
}
Console.WriteLine($"Somme de 1 à 100 = {somme}");

Console.WriteLine();
string? choix;
do
{
    Console.WriteLine("1. Dire bonjour");
    Console.WriteLine("2. Quitter");
    Console.Write("Ton choix : ");
    choix = Console.ReadLine();

    if (choix == "1")
    {
        Console.WriteLine("Bonjour !");
    }
    else if (choix != "2")
    {
        Console.WriteLine("Choix invalide.");
    }
}
while (choix != "2");

Console.WriteLine();
for (int ligne = 1; ligne <= 5; ligne++)
{
    for (int colonne = 1; colonne <= ligne; colonne++)
    {
        Console.Write("*");
    }
    Console.WriteLine();
}
