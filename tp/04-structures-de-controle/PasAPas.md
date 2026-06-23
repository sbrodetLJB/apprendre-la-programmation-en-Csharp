# Pas à pas — TP 4 : Structures de contrôle

Relis le cours [04-structures-de-controle.md](../../docs/04-structures-de-controle.md) avant de commencer. Place-toi dans `tp/04-structures-de-controle/Enonce/`.

## Étape 1 : la note avec if / else if / else

D'abord, demande la note (tu sais déjà faire depuis le TP 3) :

```csharp
Console.Write("Quelle note as-tu obtenue (sur 20) ? ");
int.TryParse(Console.ReadLine(), out int note);
```

Le cours donne un exemple quasi identique avec ces mêmes seuils (16/14/10). Le point important, expliqué dans le cours : les conditions sont testées **dans l'ordre, du plus haut seuil au plus bas**. Si tu testais `note >= 10` en premier, une note de 18 serait aussi déclarée "Passable" (car 18 >= 10 est vrai), et le programme ne testerait jamais les seuils plus élevés.

```csharp
if (note >= 16)
{
    Console.WriteLine("Très bien");
}
else if (note >= 14)
{
    Console.WriteLine("Bien");
}
else if (note >= 10)
{
    Console.WriteLine("Passable");
}
else
{
    Console.WriteLine("Insuffisant");
}
```

Teste avec plusieurs notes différentes (ex. 18, 15, 12, 5) pour vérifier que chaque cas s'affiche correctement.

## Étape 2 : l'âge ET le permis

Cette fois on combine deux conditions avec l'opérateur logique `&&` (ET), vu dans le cours : les deux doivent être vraies pour que le `if` s'exécute.

Demande d'abord les deux informations :

```csharp
Console.Write("Quel est ton âge ? ");
int.TryParse(Console.ReadLine(), out int age);
Console.Write("As-tu le permis ? (oui/non) ");
string? reponsePermis = Console.ReadLine();
```

Puis combine-les. Remarque : `reponsePermis == "oui"` compare une chaîne à une autre avec `==`, exactement comme on compare des nombres :

```csharp
if (age >= 18 && reponsePermis == "oui")
{
    Console.WriteLine("Tu peux conduire.");
}
else
{
    Console.WriteLine("Tu ne peux pas conduire.");
}
```

Teste les 4 combinaisons possibles (majeur+oui, majeur+non, mineur+oui, mineur+non) pour vérifier que seul le premier cas affiche "Tu peux conduire."

## Étape 3 : le jour de la semaine avec `switch`

Demande le numéro du jour :

```csharp
Console.Write("Numéro du jour (1-7) ? ");
int.TryParse(Console.ReadLine(), out int jour);
```

L'énoncé te laisse le choix entre les deux formes de `switch` présentées dans le cours. La forme moderne ("switch expression") est plus courte — c'est celle utilisée ici, mais la forme classique avec `case`/`break` fonctionne tout aussi bien si tu la préfères :

```csharp
string nomJour = jour switch
{
    1 => "Lundi",
    2 => "Mardi",
    3 => "Mercredi",
    4 => "Jeudi",
    5 => "Vendredi",
    6 => "Samedi",
    7 => "Dimanche",
    _ => "Jour inconnu" // _ = "tous les autres cas", équivalent de default
};
Console.WriteLine(nomJour);
```

Si tu choisis la forme classique, rappelle-toi du `break;` après chaque `case` — sans lui, le cours explique que le comportement devient incorrect.

## Étape 4 (bonus) : pair ou impair, avec le ternaire

Le modulo `%` (vu en TP 2 / cours leçon 2) renvoie le reste d'une division. Un nombre est pair si le reste de sa division par 2 est 0 :

```csharp
Console.Write("Donne un nombre : ");
int.TryParse(Console.ReadLine(), out int nombre);
Console.WriteLine(nombre % 2 == 0 ? "pair" : "impair");
```

Décompose si besoin : la condition est `nombre % 2 == 0`, la valeur si vrai est `"pair"`, la valeur si faux est `"impair"`.

## Vérifier son travail

Relance `dotnet run` et teste chaque question avec plusieurs valeurs différentes, en particulier les valeurs "limites" (ex. note = 16 exactement, âge = 18 exactement) pour vérifier que tes conditions `>=` sont correctes. Compare avec `../Corrige/Program.cs`.

**Suite : [TP 5 — Boucles](../05-boucles/PasAPas.md)**
