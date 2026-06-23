# 9. Héritage, polymorphisme et interfaces

## L'héritage

L'**héritage** permet de créer une nouvelle classe (la classe **dérivée** ou **fille**) à partir d'une classe existante (la classe **de base** ou **mère**), en récupérant automatiquement ses champs, propriétés et méthodes, puis en y ajoutant ou en y modifiant des éléments.

C'est utile quand plusieurs classes partagent une partie commune de comportement, mais se distinguent par certains détails.

```csharp
class Animal
{
    public string Nom { get; set; }

    public Animal(string nom)
    {
        Nom = nom;
    }

    public void Dormir()
    {
        Console.WriteLine($"{Nom} dort.");
    }

    public virtual void Crier()
    {
        Console.WriteLine($"{Nom} fait un bruit.");
    }
}

class Chien : Animal // Chien hérite de Animal
{
    public Chien(string nom) : base(nom) // appelle le constructeur de Animal
    {
    }

    public override void Crier() // redéfinit le comportement de la classe mère
    {
        Console.WriteLine($"{Nom} aboie : Wouf !");
    }
}
```

```csharp
Chien medor = new Chien("Médor");
medor.Dormir(); // hérité de Animal : "Médor dort."
medor.Crier();  // redéfini dans Chien : "Médor aboie : Wouf !"
```

Vocabulaire :

| Mot-clé | Rôle |
|---|---|
| `: Animal` (après le nom de classe) | Indique que `Chien` hérite de `Animal` |
| `base(nom)` | Appelle le constructeur de la classe mère |
| `virtual` | Sur la classe mère, indique qu'une méthode **peut** être redéfinie par une classe fille |
| `override` | Sur la classe fille, redéfinit effectivement une méthode `virtual` de la classe mère |

Une méthode non marquée `virtual` ne peut pas être redéfinie : c'est le comportement par défaut, qui garantit que le comportement d'une classe ne change pas de façon inattendue dans ses descendantes.

## Le polymorphisme

Le **polymorphisme** ("plusieurs formes") permet de manipuler des objets de classes différentes (mais reliées par héritage) de façon uniforme, via leur classe (ou interface) commune — et chacun se comporte selon **sa propre** version des méthodes :

```csharp
class Chat : Animal
{
    public Chat(string nom) : base(nom) { }

    public override void Crier()
    {
        Console.WriteLine($"{Nom} miaule : Miaou !");
    }
}
```

```csharp
List<Animal> animaux = new List<Animal>
{
    new Chien("Médor"),
    new Chat("Félix"),
};

foreach (Animal animal in animaux)
{
    animal.Crier(); // chaque animal exécute SA version de Crier(), bien que le type déclaré soit "Animal"
}
// Affiche :
// Médor aboie : Wouf !
// Félix miaule : Miaou !
```

C'est la puissance du polymorphisme : le code `foreach` ne sait pas, et n'a pas besoin de savoir, si chaque élément est un `Chien` ou un `Chat` — il appelle `Crier()` et laisse chaque objet décider de son propre comportement.

## Les classes abstraites

Une classe **abstraite** ne peut **jamais** être instanciée directement avec `new` : elle ne sert qu'à être héritée. Elle peut contenir des méthodes **abstraites**, c'est-à-dire sans corps, que chaque classe fille est **obligée** d'implémenter :

```csharp
abstract class Animal
{
    public string Nom { get; set; }

    public Animal(string nom)
    {
        Nom = nom;
    }

    public abstract void Crier(); // pas de corps : chaque classe fille DOIT le fournir
}
```

```csharp
// Animal a = new Animal("test"); // ERREUR : impossible d'instancier une classe abstraite
Chien medor = new Chien("Médor"); // OK : Chien fournit bien une implémentation de Crier()
```

On utilise une classe abstraite quand le concept de base (`Animal`) n'a pas de sens à exister "seul" — on ne crée jamais "un animal" générique sans savoir lequel, seulement des animaux concrets (`Chien`, `Chat`...).

## Les interfaces

Une **interface** définit un **contrat** : une liste de méthodes (et propriétés) que toute classe qui l'implémente doit fournir, sans imposer aucune implémentation ni aucun champ. Par convention, le nom d'une interface commence par un `I`.

```csharp
interface IVolant
{
    void Voler();
}

class Oiseau : Animal, IVolant // Oiseau hérite de Animal ET implémente IVolant
{
    public Oiseau(string nom) : base(nom) { }

    public override void Crier()
    {
        Console.WriteLine($"{Nom} chante.");
    }

    public void Voler()
    {
        Console.WriteLine($"{Nom} s'envole.");
    }
}
```

Contrairement à l'héritage (une classe ne peut hériter que d'**une seule** classe de base en C#), une classe peut implémenter **plusieurs** interfaces. C'est très utile pour exprimer des "capacités" transversales (`IVolant`, `INageur`...) indépendantes de la hiérarchie principale des classes.

```csharp
List<IVolant> volants = new List<IVolant> { new Oiseau("Piaf") };

foreach (IVolant v in volants)
{
    v.Voler(); // on ne sait même pas ici qu'il s'agit d'un Oiseau : seul le contrat IVolant compte
}
```

## Classe abstraite vs interface : que choisir ?

| | Classe abstraite | Interface |
|---|---|---|
| Héritage multiple | Non (une seule classe de base) | Oui (plusieurs interfaces) |
| Peut contenir des champs / une implémentation | Oui | Non (uniquement des signatures, en C# classique) |
| Relation exprimée | "est un" (`Chien` **est un** `Animal`) | "peut faire" (`Oiseau` **peut** `Voler`) |

**Suite : [10. Exceptions](10-exceptions.md)**
