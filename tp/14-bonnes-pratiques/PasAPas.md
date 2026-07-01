# Pas à pas — TP 14 : Bonnes pratiques et SOLID

Relis le cours [14-bonnes-pratiques-solid.md](../../docs/14-bonnes-pratiques-solid.md) avant de commencer. Place-toi dans `tp/14-bonnes-pratiques/Enonce/Enonce/`.

Ce TP est un **exercice de refactoring** : le code dans `Program.cs` fonctionne, mais il viole plusieurs principes vus dans le cours. Ton travail est de l'améliorer sans changer ce qu'il fait (même sortie console).

Lance d'abord le code original pour voir ce qu'il affiche :

```bash
cd tp/14-bonnes-pratiques/Enonce/Enonce
dotnet run
```

Note la sortie attendue — elle doit rester identique après chaque étape de refactoring.

## TODO 1 : Nommage

Identifie toutes les variables, paramètres et méthodes dont le nom est cryptique ou abrégé. Renomme-les selon les conventions C# du cours (PascalCase pour les méthodes, camelCase pour les variables locales).

Exemples dans le code actuel :
- `g` → `carnet` (ou `gestion`)
- `d` (la liste) → `contacts`
- `t` (le type) → `typeNotification`
- `n`, `e` (paramètres) → `nom`, `email`
- `Tout()` → `AfficherTout()` (ou mieux encore, une fois le TODO 2 fait)
- `Notif()` → `Notifier()`

Relance `dotnet run` pour vérifier que la sortie est identique.

## TODO 2 : SRP — séparer affichage et logique métier

La classe `Gestion` s'occupe à la fois de stocker les contacts *et* de les afficher dans le terminal. Ces deux responsabilités doivent être séparées.

**Étape 2a** : Crée une classe `CarnetAdresses` qui ne garde que la logique métier (stocker, ajouter, lister les contacts) :

```csharp
class CarnetAdresses
{
    private List<(string Nom, string Email)> contacts = new();

    public void AjouterContact(string nom, string email)
    {
        contacts.Add((nom, email));
    }

    public IReadOnlyList<(string Nom, string Email)> ListerTout() => contacts;
}
```

**Étape 2b** : Crée une classe statique `AffichageConsole` dont la seule responsabilité est d'afficher :

```csharp
static class AffichageConsole
{
    public static void Afficher(IEnumerable<(string Nom, string Email)> contacts)
    {
        foreach (var (nom, email) in contacts)
            Console.WriteLine($"{nom} - {email}");
    }
}
```

**Étape 2c** : Mets à jour le code en haut du fichier pour utiliser ces deux classes séparément :

```csharp
var carnet = new CarnetAdresses(...); // on verra le paramètre au TODO 4
carnet.AjouterContact("Lucas", "lucas@ex.com");
carnet.AjouterContact("Emma", "emma@ex.com");
AffichageConsole.Afficher(carnet.ListerTout()); // l'affichage est délégué
```

Relance `dotnet run` et vérifie la sortie.

## TODO 3 : OCP — remplacer le switch par une interface

Le `switch` dans `Notifier()` grossit à chaque nouveau canal de notification : c'est une violation du principe ouvert/fermé. Remplace-le par une interface.

**Étape 3a** : Déclare l'interface (en bas du fichier, comme les classes) :

```csharp
interface INotificationService
{
    void Envoyer(string message);
}
```

**Étape 3b** : Crée deux implémentations :

```csharp
class NotificationConsole : INotificationService
{
    public void Envoyer(string message) =>
        Console.WriteLine($"[Console] {message}");
}

class NotificationEmail : INotificationService
{
    private readonly string _destinataire;
    public NotificationEmail(string destinataire) { _destinataire = destinataire; }

    public void Envoyer(string message) =>
        Console.WriteLine($"[Email simulé → {_destinataire}] {message}");
}
```

Maintenant ajouter un canal "push" = créer `NotificationPush : INotificationService`, **sans toucher** au reste du code existant — c'est le principe OCP.

## TODO 4 : DIP — injecter la dépendance

Au lieu que `CarnetAdresses` décide elle-même quel service utiliser, elle doit **recevoir** le service par son constructeur. C'est l'injection de dépendances :

```csharp
class CarnetAdresses
{
    private readonly List<(string Nom, string Email)> contacts = new();
    private readonly INotificationService notif; // dépend de l'abstraction, pas de l'implémentation

    public CarnetAdresses(INotificationService notif)
    {
        this.notif = notif;
    }

    public void AjouterContact(string nom, string email)
    {
        contacts.Add((nom, email));
        notif.Envoyer($"Contact ajouté : {nom}"); // utilise l'interface
    }

    public IReadOnlyList<(string Nom, string Email)> ListerTout() => contacts;
}
```

Mets à jour l'instanciation en haut du fichier pour passer le service choisi :

```csharp
INotificationService notifService = new NotificationConsole();
var carnet = new CarnetAdresses(notifService);
```

Relance `dotnet run` une dernière fois et compare la sortie avec celle du départ.

## Vérifier son travail

Le code refactorisé doit afficher exactement la même chose que l'original, mais avec une structure qui respecte : nommage explicite, SRP (chaque classe fait une chose), OCP (ajouter un canal sans modifier l'existant), DIP (la dépendance est injectée). Compare avec `../Corrige/Corrige/Program.cs`.

**Félicitations — tu as terminé l'intégralité du cours !**
