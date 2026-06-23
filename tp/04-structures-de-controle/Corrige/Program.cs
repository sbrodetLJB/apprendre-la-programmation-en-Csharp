// TP 4 : Structures de contrôle -- Corrigé

Console.Write("Quelle note as-tu obtenue (sur 20) ? ");
int.TryParse(Console.ReadLine(), out int note);

if (note >= 16)
{
    Console.WriteLine("Très bien");
}
else if (note >= 14)
{
    Console.WriteLine("Bien");
}
else if (note >= 10)
{
    Console.WriteLine("Passable");
}
else
{
    Console.WriteLine("Insuffisant");
}

Console.Write("Quel est ton âge ? ");
int.TryParse(Console.ReadLine(), out int age);
Console.Write("As-tu le permis ? (oui/non) ");
string? reponsePermis = Console.ReadLine();

if (age >= 18 && reponsePermis == "oui")
{
    Console.WriteLine("Tu peux conduire.");
}
else
{
    Console.WriteLine("Tu ne peux pas conduire.");
}

Console.Write("Numéro du jour (1-7) ? ");
int.TryParse(Console.ReadLine(), out int jour);

string nomJour = jour switch
{
    1 => "Lundi",
    2 => "Mardi",
    3 => "Mercredi",
    4 => "Jeudi",
    5 => "Vendredi",
    6 => "Samedi",
    7 => "Dimanche",
    _ => "Jour inconnu"
};
Console.WriteLine(nomJour);

Console.Write("Donne un nombre : ");
int.TryParse(Console.ReadLine(), out int nombre);
Console.WriteLine(nombre % 2 == 0 ? "pair" : "impair");
