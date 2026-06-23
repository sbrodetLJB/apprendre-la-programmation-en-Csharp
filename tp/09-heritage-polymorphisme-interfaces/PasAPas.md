# Pas à pas — TP 9 : Héritage, polymorphisme et interfaces

Relis le cours [09-heritage-polymorphisme-interfaces.md](../../docs/09-heritage-polymorphisme-interfaces.md) avant de commencer. Place-toi dans `tp/09-heritage-polymorphisme-interfaces/Enonce/`. Comme aux TP6 et TP8, les classes vont en bas du fichier.

## Étape 1 : la classe abstraite `Forme`

Le cours explique qu'une classe **abstraite** (mot-clé `abstract`) ne peut jamais être instanciée directement avec `new` — elle sert de modèle commun à ses classes filles. Elle peut contenir :
- des méthodes **abstraites** (`abstract`, sans corps, juste une signature) que chaque fille est **obligée** d'implémenter ;
- des méthodes **normales**, déjà écrites, héritées telles quelles par toutes les filles.

Ici, `CalculerAire()` n'a pas la même formule selon la forme (carré ou cercle) → elle est abstraite. `AfficherAire()` fait toujours la même chose (afficher le résultat) → elle est normale, et peut **appeler** la méthode abstraite sans savoir laquelle des deux versions sera réellement exécutée :

```csharp
abstract class Forme
{
    public abstract double CalculerAire();

    public void AfficherAire()
    {
        Console.WriteLine($"Aire : {CalculerAire()}");
    }
}
```

## Étape 2 : `Carre : Forme` et `Cercle : Forme`

Le `:` après le nom de la classe indique l'héritage (cours, vocabulaire). Chaque classe fille doit fournir une implémentation de `CalculerAire()`, marquée `override` (puisqu'elle redéfinit une méthode abstraite de la mère) :

```csharp
class Carre : Forme
{
    public double Cote { get; set; }

    public Carre(double cote)
    {
        Cote = cote;
    }

    public override double CalculerAire()
    {
        return Cote * Cote;
    }
}

class Cercle : Forme
{
    public double Rayon { get; set; }

    public Cercle(double rayon)
    {
        Rayon = rayon;
    }

    public override double CalculerAire()
    {
        return Math.PI * Rayon * Rayon;
    }
}
```

`Math.PI` est une constante fournie par .NET (la valeur de π) — pas besoin de la déclarer toi-même.

Teste déjà à ce stade, en haut du fichier :

```csharp
Carre carre = new Carre(4);
carre.AfficherAire(); // Aire : 16
```

`AfficherAire()` n'est définie que dans `Forme`, et pourtant elle fonctionne sur un objet `Carre` — c'est l'héritage : `Carre` reçoit automatiquement tout ce qui est défini dans `Forme`.

## Étape 3 : le polymorphisme avec `List<Forme>`

Le cours insiste sur ce point : une `List<Forme>` peut contenir des objets de classes **différentes** (`Carre`, `Cercle`), tant qu'elles héritent toutes de `Forme`. Quand on appelle `AfficherAire()` sur chacun via un `foreach`, **chaque objet exécute sa propre version** de `CalculerAire()` — c'est ça, le polymorphisme : le code du `foreach` n'a pas besoin de savoir s'il manipule un carré ou un cercle.

```csharp
List<Forme> formes = new List<Forme>
{
    new Carre(4),
    new Cercle(3),
};

foreach (Forme forme in formes)
{
    forme.AfficherAire();
}
```

Tu dois voir deux lignes "Aire : ..." avec des valeurs différentes, calculées chacune avec la bonne formule.

## Étape 4 : l'interface `IDeplacable`

Le cours précise la convention de nommage (`I` devant le nom) et la nature d'une interface : un contrat sans aucune implémentation, juste des signatures de méthodes.

```csharp
interface IDeplacable
{
    void Deplacer(int dx, int dy);
}
```

L'énoncé demande que `Carre` implémente **à la fois** `Forme` (héritage) **et** `IDeplacable` (interface) — rappelle-toi de ce que dit le cours : une classe n'hérite que d'**une seule** classe de base, mais peut implémenter **plusieurs** interfaces. La syntaxe sépare les deux par une virgule après le `:` :

```csharp
class Carre : Forme, IDeplacable
{
    public double Cote { get; set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public Carre(double cote)
    {
        Cote = cote;
        X = 0;
        Y = 0;
    }

    public override double CalculerAire()
    {
        return Cote * Cote;
    }

    public void Deplacer(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }
}
```

`X` et `Y` représentent la position du carré, initialisée à `(0, 0)` dans le constructeur. Comme au TP8 (propriété `get; private set;`), elles sont lisibles de partout mais modifiables uniquement depuis l'intérieur de la classe — ici, via `Deplacer`.

Teste le déplacement en haut du fichier :

```csharp
Carre carreMobile = new Carre(4);
Console.WriteLine($"Position avant déplacement : ({carreMobile.X}, {carreMobile.Y})");
carreMobile.Deplacer(2, 3);
Console.WriteLine($"Position après déplacement : ({carreMobile.X}, {carreMobile.Y})");
```

## Vérifier son travail

Relance `dotnet run`. Tu dois voir : l'aire du carré (16), l'aire du cercle (calculée avec π), puis la position avant `(0, 0)` et après `(2, 3)` déplacement. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 10 — Exceptions](../10-exceptions/PasAPas.md)**
