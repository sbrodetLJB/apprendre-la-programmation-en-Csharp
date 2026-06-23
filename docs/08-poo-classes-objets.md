# 8. La programmation orientée objet : classes, objets, encapsulation

Jusqu'ici, on a fait de la **programmation procédurale** : une suite de variables et de méthodes qui s'exécutent dans l'ordre. La **programmation orientée objet** (POO) propose une autre façon d'organiser le code : on modélise le programme comme un ensemble d'**objets** qui collaborent, chaque objet regroupant à la fois des **données** et les **comportements** qui agissent sur ces données.

## Classe et objet

Une **classe** est un modèle (un plan) qui décrit ce que sait contenir et faire un type d'objet. Un **objet** (ou **instance**) est une réalisation concrète de ce modèle, avec ses propres valeurs.

Analogie : la classe `Voiture` est comme un plan de fabrication ; chaque voiture qui sort de l'usine (chaque objet) est une instance de ce plan, avec sa propre couleur, son propre kilométrage...

```csharp
class Contact
{
    public string Prenom;
    public string Nom;
    public string Email;
}
```

On crée un objet (une instance) avec le mot-clé `new` :

```csharp
Contact c1 = new Contact();
c1.Prenom = "Lucas";
c1.Nom = "Martin";
c1.Email = "lucas.martin@example.com";

Contact c2 = new Contact();
c2.Prenom = "Emma";
c2.Nom = "Bernard";
c2.Email = "emma.bernard@example.com";

Console.WriteLine($"{c1.Prenom} {c1.Nom}"); // Lucas Martin
Console.WriteLine($"{c2.Prenom} {c2.Nom}"); // Emma Bernard
```

`c1` et `c2` sont deux objets **distincts**, chacun avec ses propres valeurs, créés à partir de la **même** classe `Contact`.

Les `Prenom`, `Nom`, `Email` sont appelés des **champs** (fields) : ce sont les données portées par chaque objet.

## Le constructeur

Un **constructeur** est une méthode spéciale, appelée automatiquement lors du `new`, qui sert à initialiser l'objet. Il porte le même nom que la classe et n'a pas de type de retour :

```csharp
class Contact
{
    public string Prenom;
    public string Nom;
    public string Email;

    public Contact(string prenom, string nom, string email)
    {
        Prenom = prenom;
        Nom = nom;
        Email = email;
    }
}
```

Avec ce constructeur, on est désormais **obligé** de fournir ces trois informations à la création :

```csharp
Contact c1 = new Contact("Lucas", "Martin", "lucas.martin@example.com");
```

## L'encapsulation : `public` et `private`

L'**encapsulation** est le principe qui consiste à protéger les données d'un objet contre des modifications incohérentes, en ne donnant accès à ces données qu'à travers des méthodes contrôlées.

- `public` : accessible depuis n'importe où en dehors de la classe.
- `private` : accessible uniquement depuis l'intérieur de la classe elle-même.

```csharp
class CompteBancaire
{
    private double solde; // personne en dehors de la classe ne peut lire ou modifier solde directement

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

```csharp
CompteBancaire compte = new CompteBancaire(100);
compte.Deposer(50);
compte.Retirer(30);
Console.WriteLine(compte.ConsulterSolde()); // 120

// compte.solde = 1000000; // ERREUR : solde est private, inaccessible depuis l'extérieur
```

Sans encapsulation, n'importe quel code pourrait faire `compte.solde = -500;` et mettre l'objet dans un état incohérent. Avec `private` + des méthodes comme `Deposer`/`Retirer` qui vérifient les règles métier, c'est impossible.

## Les propriétés (`get`/`set`)

C# propose une syntaxe dédiée, plus idiomatique que des champs `private` + méthodes manuelles, pour exposer une donnée de façon contrôlée : les **propriétés**.

```csharp
class Contact
{
    public string Prenom { get; set; }
    public string Nom { get; set; }

    // Propriété en lecture seule, calculée à partir des deux autres
    public string NomComplet => $"{Prenom} {Nom}";
}
```

```csharp
Contact c = new Contact();
c.Prenom = "Lucas"; // appelle implicitement le "set"
c.Nom = "Martin";
Console.WriteLine(c.NomComplet); // "Lucas Martin" -- appelle le "get"
```

On peut restreindre l'écriture tout en gardant la lecture publique, ce qui est très courant :

```csharp
class Contact
{
    public string Prenom { get; private set; } // lisible de partout, modifiable seulement dans la classe

    public Contact(string prenom)
    {
        Prenom = prenom; // autorisé : on est dans la classe
    }
}
```

```csharp
Contact c = new Contact("Lucas");
Console.WriteLine(c.Prenom); // autorisé : "Lucas"
// c.Prenom = "Emma"; // ERREUR : le set est private
```

C'est la pratique recommandée en C# moderne : préférer des propriétés (`{ get; set; }`) à des champs `public` nus, dès qu'on veut exposer une donnée à l'extérieur de la classe.

**Suite : [9. Héritage, polymorphisme et interfaces](09-heritage-polymorphisme-interfaces.md)**
