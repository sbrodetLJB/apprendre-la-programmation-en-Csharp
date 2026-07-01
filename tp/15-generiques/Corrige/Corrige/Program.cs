// TP 15 : Génériques avancés -- Corrigé

// 1. Méthode générique Echanger
int x = 3, y = 7;
Echanger(ref x, ref y);
Console.WriteLine($"int : {x}, {y}"); // 7, 3

string s1 = "Bonjour", s2 = "Au revoir";
Echanger(ref s1, ref s2);
Console.WriteLine($"string : {s1}, {s2}"); // Au revoir, Bonjour

// 2. Pile<T>
Console.WriteLine();
var pile = new Pile<int>();
pile.Empiler(1);
pile.Empiler(2);
pile.Empiler(3);
Console.WriteLine($"Sommet : {pile.Sommet}"); // 3
while (!pile.EstVide)
    Console.WriteLine(pile.Depiler()); // 3, 2, 1

// 3. Max<T> contraint
Console.WriteLine();
Console.WriteLine(Max(3, 7));           // 7
Console.WriteLine(Max("abc", "xyz"));   // xyz

// 4. Dictionary<string, List<string>>
Console.WriteLine();
var annuaire = new Dictionary<string, List<string>>
{
    ["Paris"]  = new List<string> { "Lucas", "Emma", "Nathan" },
    ["Lyon"]   = new List<string> { "Alice", "Zoé" },
};

foreach (var (ville, prenoms) in annuaire)
{
    Console.WriteLine($"{ville} : {string.Join(", ", prenoms)}");
}

if (annuaire.TryGetValue("Lyon", out List<string>? lyonnais))
    Console.WriteLine($"Lyonnais : {string.Join(", ", lyonnais)}");

if (!annuaire.TryGetValue("Marseille", out _))
    Console.WriteLine("Marseille : ville absente du dictionnaire.");

// ---- Méthodes et classes génériques ----

static void Echanger<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

static T Max<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) >= 0 ? a : b;

class Pile<T>
{
    private List<T> elements = new List<T>();

    public void Empiler(T element) => elements.Add(element);

    public T Depiler()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("La pile est vide.");
        T dernier = elements[^1];
        elements.RemoveAt(elements.Count - 1);
        return dernier;
    }

    public T Sommet => elements.Count > 0
        ? elements[^1]
        : throw new InvalidOperationException("La pile est vide.");

    public int Count => elements.Count;
    public bool EstVide => elements.Count == 0;
}
