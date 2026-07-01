# 15. Génériques avancés

## Rappel : les génériques, pourquoi ?

On a déjà utilisé `List<T>` et `Dictionary<K,V>` : le `<T>` signifie "ce conteneur fonctionne avec n'importe quel type". Les génériques permettent d'écrire **du code réutilisable qui reste fortement typé** — pas besoin de dupliquer la logique pour chaque type.

## Créer une méthode générique

```csharp
static void Echanger<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

int x = 3, y = 7;
Echanger(ref x, ref y);
Console.WriteLine($"{x}, {y}"); // 7, 3

string s1 = "Bonjour", s2 = "Au revoir";
Echanger(ref s1, ref s2);
Console.WriteLine($"{s1}, {s2}"); // Au revoir, Bonjour
```

`<T>` après le nom de la méthode déclare le **paramètre de type**. C# infère automatiquement `T` à l'appel selon le type des arguments passés.

## Créer une classe générique

```csharp
class Pile<T>
{
    private List<T> elements = new List<T>();

    public void Empiler(T element) => elements.Add(element);

    public T Depiler()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("La pile est vide.");
        T dernier = elements[^1]; // [^1] = dernier élément (index depuis la fin)
        elements.RemoveAt(elements.Count - 1);
        return dernier;
    }

    public T Sommet => elements.Count > 0
        ? elements[^1]
        : throw new InvalidOperationException("La pile est vide.");

    public int Count => elements.Count;
    public bool EstVide => elements.Count == 0;
}
```

```csharp
var pile = new Pile<int>();
pile.Empiler(1);
pile.Empiler(2);
pile.Empiler(3);
Console.WriteLine(pile.Depiler()); // 3 -- dernier entré, premier sorti (LIFO)
Console.WriteLine(pile.Sommet);    // 2
```

Le `T` est résolu à la compilation : `Pile<int>` ne peut contenir que des `int`, `Pile<string>` que des `string`. Il n'y a pas de cast, pas de risque d'erreur de type à l'exécution.

## Contraintes sur `T`

Sans contrainte, `T` peut être n'importe quoi. Mais parfois on a besoin que `T` fournisse certaines garanties. On les exprime avec le mot-clé `where` :

```csharp
// T doit implémenter IComparable<T> (donc on peut utiliser CompareTo)
static T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) >= 0 ? a : b;
}

Console.WriteLine(Max(3, 7));         // 7
Console.WriteLine(Max("abc", "xyz")); // xyz
```

Contraintes courantes :

| Contrainte | Signification |
|---|---|
| `where T : class` | T doit être un type référence (une classe) |
| `where T : struct` | T doit être un type valeur (int, double, struct...) |
| `where T : new()` | T doit avoir un constructeur sans paramètre |
| `where T : IComparable<T>` | T doit implémenter l'interface indiquée |
| `where T : MonAutreClasse` | T doit hériter de la classe indiquée |

## `Dictionary<TKey, TValue>`

`Dictionary<TKey, TValue>` est la collection générique pour stocker des paires **clé → valeur**, avec accès en temps constant par clé.

```csharp
var ages = new Dictionary<string, int>();
ages["Lucas"] = 17;
ages["Emma"] = 19;
ages.Add("Nathan", 18);

Console.WriteLine(ages["Lucas"]); // 17
Console.WriteLine(ages.ContainsKey("Emma")); // true

foreach (var paire in ages)
{
    Console.WriteLine($"{paire.Key} : {paire.Value}");
}
```

Erreurs fréquentes :
- Accéder à une clé inexistante (`ages["Inconnu"]`) lève `KeyNotFoundException`. Préférer `TryGetValue` :
  ```csharp
  if (ages.TryGetValue("Inconnu", out int age))
      Console.WriteLine(age);
  else
      Console.WriteLine("Clé absente.");
  ```
- Ajouter une clé déjà présente avec `.Add(...)` lève une exception. Utiliser `ages[clé] = valeur` pour un comportement "ajout ou mise à jour" sans risque.

**Suite : [16. LINQ](16-linq.md)**
