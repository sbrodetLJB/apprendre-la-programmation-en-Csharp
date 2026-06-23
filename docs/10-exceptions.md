# 10. Gestion des exceptions

## Qu'est-ce qu'une exception ?

Une **exception** est une erreur survenant pendant l'exécution du programme (et non détectée à la compilation), qui interrompt le déroulement normal du code. Si elle n'est pas **gérée**, le programme s'arrête brutalement en affichant un message d'erreur.

```csharp
int[] notes = { 10, 12, 15 };
Console.WriteLine(notes[5]); // IndexOutOfRangeException : l'indice 5 n'existe pas
```

```csharp
string saisie = "vingt";
int age = Convert.ToInt32(saisie); // FormatException : "vingt" n'est pas un nombre valide
```

```csharp
int a = 10;
int b = 0;
Console.WriteLine(a / b); // DivideByZeroException : division par zéro (pour des int)
```

## Gérer une exception avec `try`/`catch`

```csharp
try
{
    Console.Write("Entre ton âge : ");
    string? saisie = Console.ReadLine();
    int age = Convert.ToInt32(saisie);
    Console.WriteLine($"L'année prochaine tu auras {age + 1} ans.");
}
catch (FormatException)
{
    Console.WriteLine("Ce que tu as saisi n'est pas un nombre valide.");
}
```

Le code à risque est placé dans le bloc `try`. Si une exception du type indiqué survient, l'exécution du `try` est interrompue et le bloc `catch` correspondant s'exécute à la place — **le programme ne plante pas**.

## Récupérer les informations de l'exception

```csharp
try
{
    int[] notes = { 10, 12, 15 };
    Console.WriteLine(notes[5]);
}
catch (Exception ex) // Exception est la classe de base : capture N'IMPORTE QUELLE exception
{
    Console.WriteLine($"Une erreur est survenue : {ex.Message}");
}
```

`Exception` est la classe **mère** de toutes les exceptions (on retrouve ici l'héritage vu à la leçon précédente !). On peut donc l'utiliser pour tout capturer, mais il est généralement préférable de capturer le type **précis** attendu (`FormatException`, `IndexOutOfRangeException`...), pour ne pas masquer accidentellement un bug totalement différent.

## Plusieurs `catch`

```csharp
try
{
    Console.Write("Entre un nombre : ");
    string? saisie = Console.ReadLine();
    int nombre = Convert.ToInt32(saisie);
    Console.WriteLine(100 / nombre);
}
catch (FormatException)
{
    Console.WriteLine("Ce n'est pas un nombre.");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Impossible de diviser par zéro.");
}
```

Les `catch` sont testés dans l'ordre, et le premier dont le type correspond à l'exception levée est exécuté.

## Le bloc `finally`

Le code placé dans `finally` s'exécute **toujours**, qu'une exception ait été levée ou non, et même si elle n'a été capturée par aucun `catch`. Il sert typiquement à libérer des ressources (fermer un fichier, une connexion...) :

```csharp
try
{
    Console.WriteLine("Traitement en cours...");
    throw new Exception("Erreur simulée");
}
catch (Exception ex)
{
    Console.WriteLine($"Erreur : {ex.Message}");
}
finally
{
    Console.WriteLine("Ce bloc s'exécute toujours.");
}
```

## Lever soi-même une exception : `throw`

On peut déclencher volontairement une exception avec `throw`, typiquement pour signaler qu'un appelant a violé une règle métier :

```csharp
static void Deposer(double montant)
{
    if (montant <= 0)
    {
        throw new ArgumentException("Le montant doit être positif.");
    }
    Console.WriteLine($"Dépôt de {montant} € effectué.");
}

try
{
    Deposer(-50);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Opération refusée : {ex.Message}");
}
```

`ArgumentException` est une exception déjà fournie par .NET, adaptée pour signaler qu'un argument reçu par une méthode est invalide. On l'utilisera dans le mini-projet final pour protéger les règles métier du carnet d'adresses (par exemple : refuser un email vide).

## Bonne pratique : ne pas gérer ce qu'on peut éviter

`try`/`catch` ne doit pas remplacer une vérification simple. Préférer `TryParse` (vu en leçon 3) à `try`/`catch` autour de `Convert.ToInt32` quand c'est possible : c'est plus rapide, et l'intention est plus claire.

```csharp
// Préférable :
if (int.TryParse(saisie, out int age))
{
    Console.WriteLine(age);
}

// Plutôt que :
try
{
    int age = Convert.ToInt32(saisie);
    Console.WriteLine(age);
}
catch (FormatException) { /* ... */ }
```

**Suite : [11. Mini-projet de synthèse : carnet d'adresses console](11-projet-carnet-adresses.md)**
