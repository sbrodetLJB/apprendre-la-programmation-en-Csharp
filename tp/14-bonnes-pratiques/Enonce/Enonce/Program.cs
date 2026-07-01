// TP 14 : Bonnes pratiques et SOLID
//
// Ce fichier contient du code fonctionnel mais mal structuré.
// Ton travail : le refactoriser en appliquant les principes du cours.
//
// CONSIGNES :
//
// 1. NOMMAGE : Renomme les variables et méthodes cryptiques pour qu'elles
//    soient explicites (sans changer leur comportement).
//
// 2. SRP : La classe "Gestion" fait trop de choses. Découpe-la en :
//    - "CarnetAdresses" : logique métier (ajouter, lister, rechercher)
//    - "AffichageConsole" : affichage des contacts dans le terminal
//
// 3. OCP : Remplace le switch dans Notifier() par une interface
//    "INotificationService" avec deux implémentations :
//    "NotificationConsole" et "NotificationEmail".
//    Ajouter un 3e canal ne devra pas nécessiter de modifier le switch existant.
//
// 4. DIP : La classe "Gestion" crée elle-même ses dépendances avec new.
//    Transforme-la pour recevoir INotificationService par son constructeur
//    (injection de dépendances).

// ---- Code à refactoriser ----

var g = new Gestion("console");
g.Ajouter("Lucas", "lucas@ex.com");
g.Ajouter("Emma", "emma@ex.com");
g.Tout();
g.Notif("2 contacts ajoutés.");

class Gestion
{
    private List<string[]> d = new();
    private string t;

    public Gestion(string type) { t = type; }

    // TODO 1 : renommer les méthodes et variables cryptiques

    public void Ajouter(string n, string e) // n = nom, e = email
    {
        d.Add(new[] { n, e });
    }

    public void Tout()
    {
        // TODO 2 : extraire l'affichage dans une classe AffichageConsole
        foreach (var x in d)
            Console.WriteLine($"{x[0]} - {x[1]}");
    }

    public void Notif(string msg)
    {
        // TODO 3 + 4 : remplacer ce switch par INotificationService injecté
        switch (t)
        {
            case "console":
                Console.WriteLine($"[Console] {msg}");
                break;
            case "email":
                Console.WriteLine($"[Email simulé] Envoi à admin@example.com : {msg}");
                break;
            default:
                Console.WriteLine($"[Inconnu] {msg}");
                break;
        }
    }
}
