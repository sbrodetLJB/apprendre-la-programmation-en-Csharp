# 3. Entrées/sorties et conversions

## Afficher du texte

```csharp
Console.WriteLine("Avec retour à la ligne");
Console.Write("Sans retour à la ligne");
```

## Lire une saisie utilisateur

```csharp
Console.Write("Quel est ton prénom ? ");
string? prenom = Console.ReadLine();
Console.WriteLine($"Bonjour {prenom} !");
```

`Console.ReadLine()` met le programme en pause, attend que l'utilisateur tape du texte au clavier et appuie sur Entrée, puis renvoie ce texte sous forme de `string`. Le `?` après `string` (`string?`) indique que la valeur peut être `null` (vide) — par exemple si l'entrée standard est fermée. On approfondira `null` plus tard ; pour l'instant, retiens juste cette syntaxe.

**`Console.ReadLine()` renvoie toujours une chaîne de caractères**, même si l'utilisateur tape un nombre. Pour faire des calculs, il faut **convertir** cette chaîne vers un type numérique.

## Convertir une chaîne vers un nombre

### Avec `Convert`

```csharp
Console.Write("Quel âge as-tu ? ");
string? saisie = Console.ReadLine();
int age = Convert.ToInt32(saisie);
Console.WriteLine($"L'année prochaine tu auras {age + 1} ans.");
```

| Méthode | Convertit vers |
|---|---|
| `Convert.ToInt32(x)` | `int` |
| `Convert.ToDouble(x)` | `double` |
| `Convert.ToBoolean(x)` | `bool` |

Problème : si l'utilisateur tape "vingt" au lieu de "20", `Convert.ToInt32` provoque une **exception** (une erreur qui arrête le programme si elle n'est pas gérée — on verra comment la gérer dans la leçon sur les exceptions).

### Avec `TryParse` (plus sûr)

`TryParse` essaie de convertir, et renvoie `true`/`false` selon que ça a réussi, sans jamais planter :

```csharp
Console.Write("Quel âge as-tu ? ");
string? saisie = Console.ReadLine();

if (int.TryParse(saisie, out int age))
{
    Console.WriteLine($"L'année prochaine tu auras {age + 1} ans.");
}
else
{
    Console.WriteLine("Ce n'est pas un nombre valide !");
}
```

Le mot-clé `out` permet à `TryParse` de "renvoyer" la valeur convertie dans la variable `age`, **en plus** de son `true`/`false` habituel. C'est la méthode à privilégier dès qu'une saisie utilisateur est en jeu, car on ne peut jamais faire confiance à 100 % à ce qu'un utilisateur tape.

## Convertir un nombre vers une chaîne

C'est l'inverse, et c'est automatique avec l'interpolation ou la concaténation :

```csharp
int age = 17;
string texte = age.ToString(); // "17"
string message = "Tu as " + age + " ans"; // conversion implicite
```

## Conversions entre types numériques

```csharp
double prix = 19.99;
int prixArrondi = (int)prix; // 19 -- cast explicite, tronque la partie décimale (pas d'arrondi !)

int quantite = 5;
double quantiteDouble = quantite; // 5.0 -- conversion implicite (int -> double), pas de perte possible
```

Passer d'un type "large" (`double`) vers un type "plus étroit" (`int`) nécessite un **cast explicite** entre parenthèses `(int)`, car on peut perdre de l'information. Passer d'un type étroit vers un type large (`int` vers `double`) se fait automatiquement, car aucune information n'est perdue.

**Suite : [4. Structures de contrôle](04-structures-de-controle.md)**
