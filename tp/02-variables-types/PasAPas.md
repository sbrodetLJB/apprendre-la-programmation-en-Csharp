# Pas à pas — TP 2 : Variables, types et opérateurs

Relis le cours [02-variables-types-operateurs.md](../../docs/02-variables-types-operateurs.md) avant de commencer : tu vas y retrouver chaque notion utilisée ici. Place-toi dans `tp/02-variables-types/Enonce/`.

## Étape 1 : une variable "annee"

Le cours montre qu'on déclare une variable en écrivant **type, puis nom, puis valeur** :

```csharp
int annee = 2026;
```

`int` est le type pour un nombre entier (voir le tableau des types primitifs dans le cours). Remplace `2026` par l'année en cours si tu veux. Teste avec `dotnet run` — pour l'instant rien ne s'affiche encore, c'est normal, on n'a encore rien demandé à `Console.WriteLine`.

## Étape 2 : "prenom" et "anneeNaissance"

Même logique, avec les types adaptés :

```csharp
string prenom = "Lucas";       // string pour du texte, entre guillemets
int anneeNaissance = 2009;     // int pour un entier
```

Vérifie que tu mets bien des guillemets autour du texte pour `string`, mais pas pour les nombres.

## Étape 3 : calculer l'âge

Le cours liste les opérateurs arithmétiques, dont la soustraction `-`. Tu veux stocker le résultat dans une nouvelle variable `age` :

```csharp
int age = annee - anneeNaissance;
```

À ce stade ton fichier devrait contenir 4 lignes (annee, prenom, anneeNaissance, age). Aucune ne s'affiche encore — c'est volontaire, on prépare les données avant de les afficher à l'étape suivante.

## Étape 4 : afficher la phrase avec interpolation

Le cours présente l'**interpolation de chaînes** : `$"..."` permet d'insérer une variable directement entre accolades `{ }` dans le texte.

```csharp
Console.WriteLine($"{prenom} a environ {age} ans.");
```

Lance `dotnet run`. Tu dois voir s'afficher quelque chose comme `Lucas a environ 17 ans.`. Si le `$` est oublié devant les guillemets, les accolades `{prenom}` s'afficheront littéralement au lieu d'être remplacées — c'est l'erreur la plus fréquente ici.

## Étape 5 : prix HT, taux de TVA, prix TTC

Deux nouvelles variables, cette fois de type `double` (nombre à virgule) :

```csharp
double prixHT = 49.90;
double tauxTva = 0.20; // 20%
```

Le calcul du TTC est donné dans l'énoncé : `prixHT + prixHT * tauxTva`. Respecte l'ordre des opérations — en C# comme en maths, la multiplication `*` est calculée avant l'addition `+`, donc pas besoin de parenthèses ici, mais tu peux en mettre si ça te rassure :

```csharp
double prixTTC = prixHT + prixHT * tauxTva;
Console.WriteLine($"Prix TTC : {prixTTC} €");
```

Relance `dotnet run` pour vérifier que le résultat affiché correspond bien à ce que tu calculerais à la main.

## Étape 6 : `estEnPromo` avec l'opérateur ternaire

Le cours présente le `bool` (vrai/faux) et l'opérateur ternaire `condition ? valeurSiVrai : valeurSiFaux`, qui remplace un `if/else` quand on choisit juste entre deux valeurs.

D'abord la variable :

```csharp
bool estEnPromo = true; // ou false, au choix
```

L'énoncé demande **explicitement** d'utiliser le ternaire, pas de `if`. Construis-le en deux temps si besoin :
1. Quelle est la condition ? `estEnPromo`
2. Que veut-on afficher dans chaque cas ? Un message différent.

```csharp
string message = estEnPromo ? "Cet article est en promotion !" : "Cet article est au prix normal.";
Console.WriteLine(message);
```

## Vérifier son travail

Relance `dotnet run` une dernière fois. Tu dois voir s'afficher, dans l'ordre : la phrase sur l'âge, le prix TTC, puis le message de promotion. Compare ensuite avec `../Corrige/Program.cs` — les valeurs numériques seront différentes des tiennes (c'est normal, l'énoncé ne les impose pas), mais la structure (types utilisés, opérateurs, ternaire) doit être la même.

**Suite : [TP 3 — Entrées/sorties et conversions](../03-entrees-sorties/PasAPas.md)**
