// TP 14 : Bonnes pratiques et SOLID -- Corrigé

// Utilisation avec NotificationConsole (injection de dépendance)
INotificationService notifService = new NotificationConsole();
var carnet = new CarnetAdresses(notifService);

carnet.AjouterContact("Lucas", "lucas@ex.com");
carnet.AjouterContact("Emma", "emma@ex.com");

AffichageConsole.Afficher(carnet.ListerTout());

// Changer de canal = changer l'implémentation injectée, sans toucher à CarnetAdresses
INotificationService notifEmail = new NotificationEmail("admin@example.com");
var carnet2 = new CarnetAdresses(notifEmail);
carnet2.AjouterContact("Nathan", "nathan@ex.com");

// ---- Interfaces et implémentations (OCP + DIP) ----

// 3. Interface INotificationService : contrat commun à tous les canaux
interface INotificationService
{
    void Envoyer(string message);
}

// Canal console
class NotificationConsole : INotificationService
{
    public void Envoyer(string message) =>
        Console.WriteLine($"[Console] {message}");
}

// Canal email simulé
class NotificationEmail : INotificationService
{
    private readonly string _destinataire;

    public NotificationEmail(string destinataire)
    {
        _destinataire = destinataire;
    }

    public void Envoyer(string message) =>
        Console.WriteLine($"[Email simulé → {_destinataire}] {message}");
}

// ---- Classe CarnetAdresses (SRP : logique métier uniquement) ----

// 2. CarnetAdresses : responsable de la logique métier seulement
class CarnetAdresses
{
    private readonly List<(string Nom, string Email)> _contacts = new();
    private readonly INotificationService _notif; // 4. DIP : dépendance injectée

    public CarnetAdresses(INotificationService notif)
    {
        _notif = notif;
    }

    public void AjouterContact(string nom, string email)
    {
        _contacts.Add((nom, email));
        _notif.Envoyer($"Contact ajouté : {nom}");
    }

    public IReadOnlyList<(string Nom, string Email)> ListerTout() => _contacts;
}

// ---- Classe AffichageConsole (SRP : affichage uniquement) ----

// 2. AffichageConsole : responsable uniquement de l'affichage
static class AffichageConsole
{
    public static void Afficher(IEnumerable<(string Nom, string Email)> contacts)
    {
        foreach (var (nom, email) in contacts)
            Console.WriteLine($"{nom} - {email}");
    }
}
