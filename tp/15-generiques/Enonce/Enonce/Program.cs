// TP 15 : Génériques avancés
//
// 1. Écris une méthode générique "static void Echanger<T>(ref T a, ref T b)"
//    qui échange les valeurs de deux variables. Teste-la avec des int, puis des string.
//
// 2. Écris une classe générique "Pile<T>" implémentant une pile LIFO (Last In, First Out) avec :
//    - void Empiler(T element)
//    - T Depiler()          (lève InvalidOperationException si vide)
//    - T Sommet { get; }    (propriété, lève InvalidOperationException si vide)
//    - int Count { get; }
//    - bool EstVide { get; }
//    Crée une Pile<int>, empile 1, 2, 3, et dépile en affichant chaque valeur.
//
// 3. Écris une méthode générique "static T Max<T>(T a, T b) where T : IComparable<T>"
//    qui renvoie le plus grand des deux. Teste avec des int et des string.
//
// 4. Crée un Dictionary<string, List<string>> "annuaire" associant une ville à une liste
//    de prénoms. Ajoute au moins 2 villes avec 2-3 prénoms chacune.
//    Affiche chaque ville et ses prénoms avec foreach.
//    Utilise TryGetValue pour récupérer la liste d'une ville sans risque d'exception.

Console.WriteLine("TODO : remplace cette ligne par ton code");

// TODO : déclare tes méthodes et classes génériques ici (en dessous)
