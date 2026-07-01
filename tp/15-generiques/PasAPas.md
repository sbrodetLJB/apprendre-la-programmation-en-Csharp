# Pas à pas — TP 15 : Génériques avancés

Relis le cours [15-generiques-avances.md](../../docs/15-generiques-avances.md) avant de commencer. Place-toi dans `tp/15-generiques/Enonce/Enonce/`.

## TODO 1 : méthode générique `Echanger<T>`

Le `<T>` après le nom de la méthode déclare un paramètre de type. `ref` permet de modifier les variables de l'appelant (sans `ref`, C# passe les valeurs par copie, l'échange ne serait pas visible à l'extérieur) :

```csharp
static void Echanger<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
```

Place cette méthode en bas du fichier. Puis teste-la en haut :

```csharp
int x = 3, y = 7;
Echanger(ref x, ref y);
Console.WriteLine($"{x}, {y}"); // 7, 3

string s1 = "Bonjour", s2 = "Au revoir";
Echanger(ref s1, ref s2);
Console.WriteLine($"{s1}, {s2}"); // Au revoir, Bonjour
```

Remarque : C# **infère** le type `T` automatiquement à l'appel (`int` pour le premier, `string` pour le second) — pas besoin d'écrire `Echanger<int>(ref x, ref y)`.

## TODO 2 : classe générique `Pile<T>`

Une pile (stack) fonctionne selon le principe LIFO : le dernier élément empilé est le premier dépilé — comme une pile d'assiettes.

Commence par écrire le squelette et avance propriété par propriété :

**`Empiler` et `EstVide`** — les plus simples :
```csharp
class Pile<T>
{
    private List<T> elements = new List<T>();

    public void Empiler(T element) => elements.Add(element);
    public int Count => elements.Count;
    public bool EstVide => elements.Count == 0;
```

**`Sommet`** — accès au dernier élément sans le retirer. `[^1]` est une syntaxe C# moderne pour "le dernier élément" (équivalent de `[elements.Count - 1]`) :
```csharp
    public T Sommet => elements.Count > 0
        ? elements[^1]
        : throw new InvalidOperationException("La pile est vide.");
```

**`Depiler`** — retire et renvoie le dernier élément :
```csharp
    public T Depiler()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("La pile est vide.");
        T dernier = elements[^1];
        elements.RemoveAt(elements.Count - 1);
        return dernier;
    }
}
```

Test en haut du fichier :
```csharp
var pile = new Pile<int>();
pile.Empiler(1); pile.Empiler(2); pile.Empiler(3);
Console.WriteLine(pile.Sommet); // 3, sans retirer
while (!pile.EstVide)
    Console.WriteLine(pile.Depiler()); // 3, 2, 1
```

## TODO 3 : `Max<T>` avec contrainte `IComparable<T>`

Sans la contrainte `where T : IComparable<T>`, le compilateur refuserait d'appeler `a.CompareTo(b)` car il ne saurait pas si `T` dispose de cette méthode. La contrainte le garantit :

```csharp
static T Max<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) >= 0 ? a : b;
```

`CompareTo` renvoie un entier : positif si `a > b`, 0 si égaux, négatif si `a < b`. `int`, `double`, `string` implémentent tous `IComparable<T>`, donc cette méthode fonctionne avec tous ces types.

## TODO 4 : `Dictionary<string, List<string>>`

Un dictionnaire dont les valeurs sont elles-mêmes des listes. À l'initialisation, les `["clé"] = new List<string> { ... }` sont des **initialiseurs de collection**, qui fonctionnent comme plusieurs `Add` successifs :

```csharp
var annuaire = new Dictionary<string, List<string>>
{
    ["Paris"] = new List<string> { "Lucas", "Emma" },
    ["Lyon"]  = new List<string> { "Alice", "Zoé" },
};

foreach (var (ville, prenoms) in annuaire)
    Console.WriteLine($"{ville} : {string.Join(", ", prenoms)}");
```

Pour lire sans risque d'exception si la ville n'existe pas :
```csharp
if (annuaire.TryGetValue("Lyon", out List<string>? lyonnais))
    Console.WriteLine(string.Join(", ", lyonnais));
else
    Console.WriteLine("Ville absente.");
```

## Vérifier son travail

Lance `dotnet run`. Tu dois voir l'échange de valeurs, la pile se vider dans l'ordre 3-2-1, le `Max` correct sur des int et des string, puis le dictionnaire affiché ville par ville. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 16 — LINQ](../16-linq/PasAPas.md)**
