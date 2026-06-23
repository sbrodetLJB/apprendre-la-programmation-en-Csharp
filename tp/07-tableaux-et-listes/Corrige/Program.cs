using System.Linq;

// TP 7 : Tableaux et List<T> -- Corrigé

int[] notes = { 12, 15, 9, 18, 14 };

foreach (int note in notes)
{
    Console.WriteLine(note);
}

int somme = 0;
foreach (int note in notes)
{
    somme += note;
}
double moyenne = (double)somme / notes.Length;
Console.WriteLine($"Moyenne : {moyenne}");

List<string> courses = new List<string>();
courses.Add("Pain");
courses.Add("Lait");
courses.Add("Oeufs");
courses.Add("Beurre");

Console.WriteLine($"Nombre d'articles : {courses.Count}");
courses.Remove("Lait");

foreach (string article in courses)
{
    Console.WriteLine(article);
}

List<string> prenoms = new List<string>();
for (int i = 1; i <= 3; i++)
{
    Console.Write($"Prénom n°{i} : ");
    string? prenom = Console.ReadLine();
    if (prenom != null)
    {
        prenoms.Add(prenom);
    }
}
prenoms.Sort();
foreach (string prenom in prenoms)
{
    Console.WriteLine(prenom);
}

List<int> notesList = new List<int> { 12, 15, 9, 18, 14 };
Console.WriteLine($"Max : {notesList.Max()}");
Console.WriteLine($"Min : {notesList.Min()}");
Console.WriteLine($"Au moins une note insuffisante : {notesList.Exists(n => n < 10)}");
