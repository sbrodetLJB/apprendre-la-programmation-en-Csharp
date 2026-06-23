# Pas à pas — TP 3 : Entrées/sorties et conversions

Relis le cours [03-entrees-sorties-conversions.md](../../docs/03-entrees-sorties-conversions.md) avant de te lancer : ce TP repose entièrement sur `Console.ReadLine()` et `TryParse`. Place-toi dans `tp/03-entrees-sorties/Enonce/`.

## Étape 1 : demander le prénom et saluer

Le cours montre le duo `Console.Write` (sans retour à la ligne, pour poser la question) + `Console.ReadLine()` (qui attend la saisie de l'utilisateur et la renvoie sous forme de `string`) :

```csharp
Console.Write("Quel est ton prénom ? ");
string? prenom = Console.ReadLine();
Console.WriteLine($"Bonjour {prenom} !");
```

Pourquoi `string?` et pas juste `string` ? Le cours l'explique : le `?` signale que la valeur peut être `null` (vide), un cas rare mais possible. Pour l'instant, recopie simplement cette syntaxe sans t'en inquiéter davantage.

Teste avec `dotnet run` : le programme doit se mettre en pause, attendre que tu tapes un prénom et appuies sur Entrée, puis afficher la salutation.

## Étape 2 : le nombre de cours, avec `TryParse`

**Piège à éviter** : `Console.ReadLine()` renvoie toujours du texte, **même si l'utilisateur tape un chiffre**. L'énoncé demande explicitement `int.TryParse`, pas `Convert.ToInt32` — relis la section "Avec TryParse (plus sûr)" du cours pour comprendre pourquoi : `TryParse` ne fait jamais planter le programme, même avec une saisie invalide.

Construis-le par étapes :
1. Poser la question et lire la saisie (comme à l'étape 1) :
   ```csharp
   Console.Write("Combien de cours suis-tu cette semaine ? ");
   string? saisieCours = Console.ReadLine();
   ```
2. Essayer la conversion avec `TryParse`. Rappelle-toi de la syntaxe du cours : `int.TryParse(saisie, out int variable)` renvoie `true`/`false`, et range le résultat converti dans la variable après `out` :
   ```csharp
   if (int.TryParse(saisieCours, out int nombreCours))
   {
       Console.WriteLine($"Cela représente environ {nombreCours} heures de cours.");
   }
   else
   {
       Console.WriteLine("Saisie invalide.");
   }
   ```

Teste deux fois : une fois en tapant un nombre valide (ex. `3`), une fois en tapant n'importe quoi (ex. `abc`). Dans le second cas, le programme ne doit **pas** planter, juste afficher "Saisie invalide."

## Étape 3 : le prix avec `double.TryParse`

Même principe que l'étape 2, mais avec `double` (puisqu'un prix a des centimes) :

```csharp
Console.Write("Quel est le prix HT d'un article ? ");
string? saisiePrix = Console.ReadLine();

if (double.TryParse(saisiePrix, out double prixHT))
{
    double prixTTC = prixHT * 1.20; // +20% de TVA
    Console.WriteLine($"Prix TTC : {prixTTC} €");
}
else
{
    Console.WriteLine("Saisie invalide.");
}
```

Pourquoi `prixHT * 1.20` plutôt que `prixHT + prixHT * 0.20` comme dans le TP précédent ? Les deux donnent le même résultat mathématique — `x * 1.20` est juste une autre façon d'écrire "x plus 20% de x". Utilise la formule avec laquelle tu es le plus à l'aise.

## Étape 4 (bonus) : somme, différence, produit de deux nombres

Même mécanique de lecture + conversion, répétée deux fois :

```csharp
Console.Write("Premier nombre : ");
int.TryParse(Console.ReadLine(), out int a);
Console.Write("Second nombre : ");
int.TryParse(Console.ReadLine(), out int b);

Console.WriteLine($"Somme : {a + b}");
Console.WriteLine($"Différence : {a - b}");
Console.WriteLine($"Produit : {a * b}");
```

Remarque : ici on appelle `Console.ReadLine()` **directement à l'intérieur** de `TryParse`, sans variable intermédiaire — c'est juste plus court à écrire, mais ça fait exactement la même chose qu'aux étapes précédentes (lire, puis convertir). On ne vérifie pas le `true`/`false` renvoyé par `TryParse` ici, par souci de simplicité (si la saisie est invalide, `a` ou `b` vaudra simplement `0`).

## Vérifier son travail

Relance `dotnet run` et teste chaque question avec une saisie valide, puis (pour les questions 2 et 3) avec une saisie invalide pour t'assurer que le programme ne plante jamais. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 4 — Structures de contrôle](../04-structures-de-controle/PasAPas.md)**
