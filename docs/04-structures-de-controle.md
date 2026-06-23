# 4. Structures de contrôle

## La condition `if`

```csharp
int age = 17;

if (age >= 18)
{
    Console.WriteLine("Tu es majeur.");
}
else
{
    Console.WriteLine("Tu es mineur.");
}
```

On peut enchaîner plusieurs conditions avec `else if` :

```csharp
int note = 14;

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

Les conditions sont testées **dans l'ordre**, et dès que l'une est vraie, les suivantes ne sont même pas évaluées. C'est pour ça qu'on range les seuils du plus haut au plus bas ici : si on testait `note >= 10` en premier, on ne distinguerait jamais "Très bien" de "Passable".

## Combiner des conditions

```csharp
int age = 20;
bool aLePermis = true;

if (age >= 18 && aLePermis)
{
    Console.WriteLine("Tu peux conduire.");
}

bool estWeekend = true;
bool estFerie = false;

if (estWeekend || estFerie)
{
    Console.WriteLine("Pas cours aujourd'hui.");
}
```

## L'opérateur ternaire

Pour une condition simple qui ne fait que choisir entre deux valeurs, l'opérateur ternaire `?:` est plus concis qu'un `if/else` :

```csharp
int age = 17;
string statut = age >= 18 ? "majeur" : "mineur";
// équivalent à :
// string statut;
// if (age >= 18) { statut = "majeur"; } else { statut = "mineur"; }
```

## Le `switch`

Quand on compare une même variable à de nombreuses valeurs possibles, le `switch` est plus lisible qu'une longue chaîne de `else if` :

```csharp
int jour = 3;
string nomJour;

switch (jour)
{
    case 1:
        nomJour = "Lundi";
        break;
    case 2:
        nomJour = "Mardi";
        break;
    case 3:
        nomJour = "Mercredi";
        break;
    default:
        nomJour = "Jour inconnu";
        break;
}

Console.WriteLine(nomJour);
```

Le `break` est indispensable : il indique où s'arrête chaque cas. `default` est le cas exécuté si aucune des valeurs ne correspond (équivalent du `else` final).

### La forme moderne : `switch` expression

C# propose une syntaxe plus compacte qui renvoie directement une valeur :

```csharp
int jour = 3;

string nomJour = jour switch
{
    1 => "Lundi",
    2 => "Mardi",
    3 => "Mercredi",
    4 => "Jeudi",
    5 => "Vendredi",
    6 => "Samedi",
    7 => "Dimanche",
    _ => "Jour inconnu" // _ joue le rôle de default
};

Console.WriteLine(nomJour);
```

Cette forme évite les `break` oubliés et se lit comme une table de correspondance.

**Suite : [5. Boucles](05-boucles.md)**
