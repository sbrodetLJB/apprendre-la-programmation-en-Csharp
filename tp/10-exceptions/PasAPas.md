# Pas à pas — TP 10 : Gestion des exceptions

Relis le cours [10-exceptions.md](../../docs/10-exceptions.md) avant de commencer. Place-toi dans `tp/10-exceptions/Enonce/`.

## Étape 1 : division avec `try`/`catch`

D'abord, lis les deux nombres (mécanique déjà connue depuis le TP3) :

```csharp
Console.Write("Premier nombre : ");
int.TryParse(Console.ReadLine(), out int a);
Console.Write("Second nombre : ");
int.TryParse(Console.ReadLine(), out int b);
```

Le cours montre que diviser un `int` par zéro provoque une `DivideByZeroException` qui arrête le programme si elle n'est pas gérée. Le code "à risque" (la division) se place dans un bloc `try`, et le `catch` correspondant gère le cas où `b` vaut 0 :

```csharp
try
{
    Console.WriteLine($"Résultat : {a / b}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Impossible de diviser par zéro.");
}
```

Teste deux fois : avec `b` différent de 0 (le résultat s'affiche normalement), puis avec `b = 0` (le message d'erreur s'affiche, **sans que le programme plante**).

## Étape 2 : `CalculerMoyenne(int[] notes)` qui lève une exception

Le cours présente `throw` pour déclencher volontairement une exception, typiquement quand un appelant viole une règle (ici : un tableau vide n'a pas de moyenne calculable).

Construis la méthode en deux temps :
1. Vérifier la règle, et lever l'exception si elle est violée :
   ```csharp
   if (notes.Length == 0)
   {
       throw new ArgumentException("Le tableau de notes ne peut pas être vide.");
   }
   ```
2. Sinon, calculer la moyenne normalement (même logique qu'au TP7) :
   ```csharp
   static double CalculerMoyenne(int[] notes)
   {
       if (notes.Length == 0)
       {
           throw new ArgumentException("Le tableau de notes ne peut pas être vide.");
       }

       int somme = 0;
       foreach (int note in notes)
       {
           somme += note;
       }
       return (double)somme / notes.Length;
   }
   ```

Place cette méthode en bas du fichier (même convention qu'au TP6/8/9). Puis, en haut, appelle-la avec un tableau vide **dans un `try`/`catch`**, pour vérifier que l'exception est bien interceptée plutôt que de planter le programme :

```csharp
try
{
    double moyenne = CalculerMoyenne(Array.Empty<int>());
    Console.WriteLine(moyenne);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erreur : {ex.Message}");
}
```

`Array.Empty<int>()` crée un tableau de `int` vide (0 élément) — une façon pratique de tester volontairement le cas limite. `ex.Message` (cours, section "Récupérer les informations de l'exception") contient le texte que tu as passé à `throw new ArgumentException("...")`.

## Étape 3 : `Deposer(double solde, double montant)` qui lève une exception

Même logique que l'étape 2, mais pour une règle métier différente (montant doit être positif) :

```csharp
static void Deposer(double solde, double montant)
{
    if (montant <= 0)
    {
        throw new ArgumentException("Le montant doit être positif.");
    }
    Console.WriteLine($"Nouveau solde : {solde + montant}");
}
```

Teste-la avec un montant négatif, dans un `try`/`catch` :

```csharp
try
{
    Deposer(100, -50);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erreur : {ex.Message}");
}
```

## Étape 4 (bonus) : ajouter un `finally`

Le cours explique que le bloc `finally` s'exécute **toujours**, qu'une exception soit levée ou non. Ajoute-le à l'un de tes `try`/`catch` existants, par exemple celui de l'étape 1 :

```csharp
try
{
    Console.WriteLine($"Résultat : {a / b}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Impossible de diviser par zéro.");
}
finally
{
    Console.WriteLine("Opération terminée.");
}
```

Teste avec `b = 0` et avec `b` différent de 0 : dans les deux cas, "Opération terminée." doit s'afficher.

## Vérifier son travail

Relance `dotnet run`. Teste systématiquement les cas qui déclenchent l'exception (b = 0, tableau vide, montant négatif) **et** les cas normaux, pour t'assurer que le programme ne plante jamais et affiche les bons messages dans chaque cas. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 11 — Mini-projet de synthèse : carnet d'adresses console](../11-projet-carnet-adresses/PasAPas.md)**
