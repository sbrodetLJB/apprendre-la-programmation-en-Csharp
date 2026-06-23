# 2. Variables, types et opérateurs

## Déclarer une variable

Une variable est un espace mémoire nommé qui contient une valeur. En C#, on déclare une variable en précisant son **type**, puis son **nom**, puis (optionnellement) sa valeur initiale :

```csharp
int age = 17;
double moyenne = 12.5;
string prenom = "Lucas";
bool estMajeur = false;
char initiale = 'L';
```

## Les types primitifs principaux

| Type | Contient | Exemple |
|---|---|---|
| `int` | Un nombre entier | `42`, `-7` |
| `double` | Un nombre à virgule (flottant) | `3.14`, `-0.5` |
| `decimal` | Un nombre à virgule de haute précision (idéal pour l'argent) | `19.99m` |
| `string` | Une chaîne de caractères | `"Bonjour"` |
| `char` | Un seul caractère | `'A'` |
| `bool` | Une valeur vraie/fausse | `true`, `false` |

C# est **fortement typé** : une fois qu'une variable est déclarée avec un type, elle ne peut plus contenir qu'une valeur de ce type.

```csharp
int age = 17;
age = "dix-sept"; // ERREUR DE COMPILATION : on ne peut pas mettre un string dans un int
```

## Le mot-clé `var`

On peut laisser le compilateur déduire le type à partir de la valeur, avec `var` :

```csharp
var age = 17;        // déduit : int
var prenom = "Lucas"; // déduit : string
```

`var` ne rend pas C# "faiblement typé" : le type est bien fixé à la compilation, seulement on ne l'écrit pas explicitement. On utilise `var` quand le type est évident à la lecture, et le type explicite (`int`, `string`...) sinon.

## Constantes

Une valeur qui ne doit jamais changer se déclare avec `const` :

```csharp
const double Pi = 3.14159;
```

## Les opérateurs arithmétiques

```csharp
int a = 10;
int b = 3;

Console.WriteLine(a + b); // 13  addition
Console.WriteLine(a - b); // 7   soustraction
Console.WriteLine(a * b); // 30  multiplication
Console.WriteLine(a / b); // 3   division ENTIÈRE (le reste est tronqué)
Console.WriteLine(a % b); // 1   modulo (reste de la division)
```

Attention : la division entre deux `int` donne un `int` (la partie décimale est perdue). Pour une division avec virgule, il faut au moins un `double` :

```csharp
double resultat = 10.0 / 3; // 3.333...
```

## Les opérateurs de comparaison

```csharp
Console.WriteLine(5 == 5);  // true   égal à
Console.WriteLine(5 != 3);  // true   différent de
Console.WriteLine(5 > 3);   // true   supérieur à
Console.WriteLine(5 >= 5);  // true   supérieur ou égal à
Console.WriteLine(5 < 3);   // false  inférieur à
```

## Les opérateurs logiques

```csharp
bool a = true;
bool b = false;

Console.WriteLine(a && b); // false  ET logique (les deux doivent être vrais)
Console.WriteLine(a || b); // true   OU logique (au moins un est vrai)
Console.WriteLine(!a);     // false  NON logique (inverse la valeur)
```

## Raccourcis d'affectation

```csharp
int score = 10;
score += 5;  // équivalent à : score = score + 5;  -> 15
score -= 3;  // -> 12
score *= 2;  // -> 24
score /= 4;  // -> 6

int compteur = 0;
compteur++;  // incrémente de 1 -> 1
compteur--;  // décrémente de 1 -> 0
```

## La concaténation et l'interpolation de chaînes

```csharp
string prenom = "Lucas";
int age = 17;

// Concaténation avec +
Console.WriteLine("Bonjour " + prenom + ", tu as " + age + " ans.");

// Interpolation (recommandé : plus lisible), avec le préfixe $
Console.WriteLine($"Bonjour {prenom}, tu as {age} ans.");
```

L'interpolation de chaînes (`$"..."`) permet d'insérer directement des variables, voire des expressions, entre accolades `{ }` dans une chaîne. C'est la méthode à privilégier.

**Suite : [3. Entrées/sorties et conversions](03-entrees-sorties-conversions.md)**
