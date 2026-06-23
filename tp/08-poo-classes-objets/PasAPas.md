# Pas à pas — TP 8 : POO — classes, objets, encapsulation

Relis le cours [08-poo-classes-objets.md](../../docs/08-poo-classes-objets.md) avant de commencer : c'est le tournant du cours, où on passe de la programmation procédurale à la POO. Place-toi dans `tp/08-poo-classes-objets/Enonce/`.

Comme au TP6, tes classes se déclarent **en bas** du fichier, le code qui les utilise reste **en haut**.

## Étape 1 : la classe `Livre`

Le cours montre la structure d'une classe avec des propriétés et un constructeur. Ici, on veut trois propriétés (`Titre`, `Auteur`, `NombrePages`) initialisées via le constructeur — utilise la syntaxe `{ get; set; }` (propriétés), recommandée par le cours plutôt que des champs `public` nus :

```csharp
class Livre
{
    public string Titre { get; set; }
    public string Auteur { get; set; }
    public int NombrePages { get; set; }

    public Livre(string titre, string auteur, int nombrePages)
    {
        Titre = titre;
        Auteur = auteur;
        NombrePages = nombrePages;
    }
}
```

Remarque sur le constructeur : il porte le **même nom que la classe** (`Livre`), n'a **pas de type de retour** (pas même `void`), et chaque paramètre est recopié dans la propriété correspondante (`Titre = titre;` : à gauche la propriété de l'objet, à droite le paramètre reçu).

## Étape 2 : trois objets `Livre` dans une `List<Livre>`

On crée des objets avec `new`, comme dans le cours. Ici, l'énoncé demande de les stocker directement dans une liste à la création, avec la syntaxe d'initialisation vue au TP7 :

```csharp
List<Livre> livres = new List<Livre>
{
    new Livre("Le Petit Prince", "Antoine de Saint-Exupéry", 96),
    new Livre("1984", "George Orwell", 328),
    new Livre("Dune", "Frank Herbert", 412),
};
```

Place cette ligne **en haut** du fichier (avant la classe `Livre`, qui elle reste en bas). Puis affiche chaque livre avec un `foreach` (TP7), en accédant aux propriétés avec un point `.` :

```csharp
foreach (Livre livre in livres)
{
    Console.WriteLine($"{livre.Titre} - {livre.Auteur} ({livre.NombrePages} pages)");
}
```

Teste avec `dotnet run` avant de passer à la suite.

## Étape 3 : la classe `CompteBancaire`, avec encapsulation

C'est l'exemple central du cours sur l'encapsulation — relis-le si besoin. L'idée : le champ `solde` est `private` (inaccessible depuis l'extérieur de la classe), et on ne peut le modifier qu'à travers des méthodes qui **vérifient des règles** (pas de dépôt négatif, pas de retrait supérieur au solde).

```csharp
class CompteBancaire
{
    private double solde;

    public CompteBancaire(double soldeInitial)
    {
        solde = soldeInitial;
    }

    public void Deposer(double montant)
    {
        if (montant > 0)
        {
            solde += montant;
        }
    }

    public void Retirer(double montant)
    {
        if (montant > 0 && montant <= solde)
        {
            solde -= montant;
        }
        else
        {
            Console.WriteLine("Retrait impossible.");
        }
    }

    public double ConsulterSolde()
    {
        return solde;
    }
}
```

Remarque : `ConsulterSolde()` est la **seule** façon, depuis l'extérieur de la classe, de connaître la valeur de `solde` — c'est exactement le principe d'encapsulation : on contrôle l'accès aux données via des méthodes publiques, plutôt que de laisser n'importe qui modifier `solde` directement.

Utilise-la en haut du fichier, avec un retrait volontairement trop important pour vérifier le message d'erreur :

```csharp
CompteBancaire compte = new CompteBancaire(100);
compte.Deposer(50);
compte.Retirer(30);
compte.Retirer(1000); // doit afficher "Retrait impossible." (1000 > solde restant)
Console.WriteLine($"Solde final : {compte.ConsulterSolde()}");
```

Calcule à la main ce que devrait valoir le solde final (100 + 50 - 30 = 120) et vérifie que `dotnet run` affiche bien ce résultat.

## Étape 4 (bonus) : transformer `solde` en propriété

Le cours présente une alternative plus moderne aux champs `private` + méthode `Consulter...()` manuelle : une propriété avec un `set` restreint, `{ get; private set; }`. Elle reste **lisible** depuis l'extérieur (`get` public), mais **modifiable seulement depuis l'intérieur** de la classe (`set` private) :

```csharp
class CompteBancaire
{
    public double Solde { get; private set; }

    public CompteBancaire(double soldeInitial)
    {
        Solde = soldeInitial;
    }

    public void Deposer(double montant)
    {
        if (montant > 0)
        {
            Solde += montant;
        }
    }

    public void Retirer(double montant)
    {
        if (montant > 0 && montant <= Solde)
        {
            Solde -= montant;
        }
        else
        {
            Console.WriteLine("Retrait impossible.");
        }
    }

    public double ConsulterSolde()
    {
        return Solde;
    }
}
```

Remarque : on garde quand même `ConsulterSolde()` ici, pour ne pas casser le code qui l'utilise déjà en haut du fichier — mais tu pourrais aussi écrire directement `compte.Solde` à la place de `compte.ConsulterSolde()`, puisque `Solde` est désormais directement lisible.

## Vérifier son travail

Relance `dotnet run` : tu dois voir les 3 livres affichés, puis le message "Retrait impossible." suivi du solde final (120). Compare avec `../Corrige/Program.cs` (qui intègre directement la version "bonus" avec la propriété).

**Suite : [TP 9 — Héritage, polymorphisme et interfaces](../09-heritage-polymorphisme-interfaces/PasAPas.md)**
