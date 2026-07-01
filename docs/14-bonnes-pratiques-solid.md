# 14. Bonnes pratiques et principes SOLID

## Clean code : les règles de base

Un code propre ("clean code") n'est pas seulement un code qui fonctionne — c'est un code que quelqu'un d'autre (ou toi-même dans 3 mois) peut lire, comprendre et modifier sans effort.

### Nommage explicite

Choisir un bon nom est la décision la plus impactante qu'on prend en écrivant du code. Un bon nom supprime le besoin d'un commentaire.

```csharp
// ❌ Noms cryptiques
int x = 86400;
bool f(int n) => n % 2 == 0;

// ✅ Noms qui parlent
int secondesParJour = 86400;
bool EstPair(int nombre) => nombre % 2 == 0;
```

Conventions C# :
- **PascalCase** pour les classes, méthodes, propriétés : `Contact`, `AjouterContact()`, `NomComplet`
- **camelCase** pour les variables locales et paramètres : `prenom`, `nombreCours`
- **`I` + PascalCase** pour les interfaces : `INotificationService`
- Constantes : `const string VilleParDefaut = "Paris";`

### Méthodes courtes avec une seule responsabilité

Une méthode fait **une seule chose**. Si tu as besoin d'un commentaire "// étape 1...", "// étape 2..." pour séparer les parties, c'est souvent le signe qu'elle devrait être découpée.

```csharp
// ❌ Une méthode qui fait trop
static void TraiterCommande(Commande c)
{
    // Valider
    if (c.Montant <= 0) throw new ArgumentException("Montant invalide");
    // Calculer TVA
    double ttc = c.Montant * 1.20;
    // Envoyer email
    Console.WriteLine($"Email envoyé pour {ttc}€");
    // Sauvegarder
    Console.WriteLine($"Commande {c.Id} sauvegardée.");
}

// ✅ Chaque méthode fait une chose
static void ValiderCommande(Commande c)
{
    if (c.Montant <= 0) throw new ArgumentException("Montant invalide");
}

static double CalculerTTC(double montantHT) => montantHT * 1.20;

static void EnvoyerConfirmation(double montantTTC)
{
    Console.WriteLine($"Email envoyé pour {montantTTC}€");
}
```

### Ne pas commenter ce qu'on peut nommer

```csharp
// ❌ Le commentaire répète ce que le nom dit déjà
// Incrémente le compteur de 1
compteur++;

// ❌ Le commentaire compense un mauvais nom
int d = 7; // délai en jours

// ✅ Le nom porte l'information
int delaiEnJours = 7;
```

Un commentaire est utile pour expliquer **pourquoi**, pas **quoi** : une contrainte non-évidente, un cas limite, une décision d'architecture.

---

## Les principes SOLID

SOLID est un acronyme décrivant 5 principes de conception orientée objet qui rendent le code plus facile à maintenir et à faire évoluer.

### S — Single Responsibility Principle (SRP)

**Une classe a une seule raison de changer.**

Si une classe gère à la fois les données *et* leur sauvegarde *et* leur affichage, elle a trois raisons de changer. Séparer ces responsabilités rend chaque classe plus simple et indépendante.

```csharp
// ❌ CarnetAdresses qui fait tout
class CarnetAdresses
{
    private List<Contact> contacts = new();

    public void Ajouter(Contact c) { contacts.Add(c); }

    public void Afficher()
    {
        foreach (var c in contacts)
            Console.WriteLine(c); // affichage mélangé à la logique métier
    }

    public void SauvegarderDansFichier(string chemin)
    {
        File.WriteAllLines(chemin, contacts.Select(c => c.ToString())); // persistance mélangée aussi
    }
}

// ✅ Chaque classe a une seule responsabilité
class CarnetAdresses { /* logique métier seulement */ }
class AffichageConsole { public void Afficher(IEnumerable<Contact> contacts) { ... } }
class ExportFichier { public void Exporter(IEnumerable<Contact> contacts, string chemin) { ... } }
```

### O — Open/Closed Principle (OCP)

**Ouvert à l'extension, fermé à la modification.**

Le code existant ne devrait pas avoir à être modifié pour ajouter une nouvelle fonctionnalité — on ajoute du nouveau code (une nouvelle classe, une nouvelle implémentation) sans toucher à l'ancien.

```csharp
// ❌ Ajouter un nouveau canal de notification nécessite de modifier cette méthode
static void EnvoyerNotification(string type, string message)
{
    if (type == "email") Console.WriteLine($"Email : {message}");
    else if (type == "sms") Console.WriteLine($"SMS : {message}");
    // Ajouter "push" obligerait à modifier ce if/else
}

// ✅ Chaque canal est une implémentation d'une interface commune
interface INotificationService
{
    void Envoyer(string message);
}

class NotificationEmail : INotificationService
{
    public void Envoyer(string message) => Console.WriteLine($"Email : {message}");
}

class NotificationSms : INotificationService
{
    public void Envoyer(string message) => Console.WriteLine($"SMS : {message}");
}

// Ajouter "push" = créer NotificationPush : INotificationService, sans toucher au reste
```

### D — Dependency Inversion Principle (DIP) / Injection de dépendances

**Dépendre des abstractions, pas des implémentations concrètes.**

Plutôt que de créer soi-même ses dépendances avec `new`, on les reçoit en paramètre du constructeur (c'est l'**injection de dépendances**). Cela rend le code testable et découplé.

```csharp
// ❌ Dépendance directe : impossible à tester sans envoyer de vrais emails
class GestionnaireContacts
{
    private NotificationEmail _notif = new NotificationEmail(); // couplage fort

    public void AjouterContact(Contact c)
    {
        // logique...
        _notif.Envoyer($"Contact {c.NomComplet} ajouté.");
    }
}

// ✅ Injection de dépendance : on passe l'interface, pas l'implémentation
class GestionnaireContacts
{
    private INotificationService _notif;

    public GestionnaireContacts(INotificationService notif)
    {
        _notif = notif; // on reçoit la dépendance, on ne la crée pas
    }

    public void AjouterContact(Contact c)
    {
        // logique...
        _notif.Envoyer($"Contact {c.NomComplet} ajouté.");
    }
}

// En production
var gestionnaire = new GestionnaireContacts(new NotificationEmail());

// En test : on injecte un "faux" service, sans envoyer de vrai email
var gestionnaire = new GestionnaireContacts(new NotificationConsole());
```

### L et I (pour info)

Les deux autres principes SOLID sont :
- **L — Liskov Substitution Principle** : une classe fille doit pouvoir remplacer sa classe mère sans que le comportement du programme soit altéré. En pratique : ne pas `override` une méthode pour lui faire faire quelque chose de contradictoire avec ce que la mère promettait.
- **I — Interface Segregation Principle** : mieux vaut plusieurs petites interfaces ciblées qu'une grande interface fourre-tout. Une classe ne devrait pas être forcée d'implémenter des méthodes dont elle n'a pas besoin.

---

## Quand refactoriser ?

Refactoriser = améliorer la structure du code sans en changer le comportement observable. Les tests unitaires (leçon 12) sont le filet de sécurité qui rend le refactoring serein.

Quelques signaux qui invitent à refactoriser ("code smells") :
- **Méthode trop longue** (> 20 lignes) → la découper
- **Classe qui fait tout** → appliquer le SRP
- **`if/else` ou `switch` qui grossit** avec chaque nouvelle règle → remplacer par du polymorphisme (OCP)
- **Duplication** (même logique copiée-collée en plusieurs endroits) → extraire une méthode ou une classe commune

**Suite : [15. Génériques avancés](15-generiques-avances.md)**
