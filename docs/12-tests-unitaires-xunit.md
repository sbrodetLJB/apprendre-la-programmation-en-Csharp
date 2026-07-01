# 12. Tests unitaires avec xUnit

## Pourquoi tester son code ?

Un programme peut sembler fonctionner "à la main" mais rater dans des cas qu'on n'a pas pensé à tester. Les **tests unitaires** automatisent cette vérification : on écrit du code qui vérifie le comportement d'un autre code, et on peut rejouer ces vérifications à tout moment (après un refactoring, après une modification, avant de livrer).

Un test unitaire cible la plus petite unité testable : en général, **une seule méthode**, dans un seul scénario. Si la méthode est bien isolée (elle ne dépend pas de la base de données, du réseau, de l'heure...), le test est rapide, fiable et répétable.

## Mettre en place xUnit

xUnit est le framework de tests unitaires le plus utilisé en .NET. Un projet de tests est un projet .NET à part, qui **référence** le projet contenant le code à tester.

```bash
# Créer le projet de code à tester (bibliothèque de classes)
dotnet new classlib -n MaBibliotheque

# Créer le projet de tests
dotnet new xunit -n MaBibliotheque.Tests

# Relier les deux
cd MaBibliotheque.Tests
dotnet add reference ../MaBibliotheque/MaBibliotheque.csproj
```

Pour exécuter tous les tests :

```bash
dotnet test
```

## Le patron AAA : Arrange / Act / Assert

Tout test unitaire bien écrit suit trois étapes, dans l'ordre :

| Étape | Rôle |
|---|---|
| **Arrange** | Préparer les données et les objets nécessaires au test |
| **Act** | Appeler la méthode qu'on veut tester |
| **Assert** | Vérifier que le résultat correspond à ce qu'on attendait |

```csharp
[Fact]
public void Additionner_DeuxPositifs_RetourneLaSomme()
{
    // Arrange
    var calc = new Operations();

    // Act
    int resultat = calc.Additionner(3, 4);

    // Assert
    Assert.Equal(7, resultat);
}
```

## Nommer ses tests

Un bon nom de test décrit exactement ce qu'il vérifie. La convention la plus répandue en .NET est :

```
NomMethode_Scenario_ResultatAttendu
```

Exemples :
- `Additionner_DeuxPositifs_RetourneLaSomme`
- `Diviser_DiviseurZero_LeveException`
- `EstPalindrome_ChaineVide_RetourneTrue`

Ce nom devient le message d'erreur affiché quand le test échoue : un bon nom permet de comprendre immédiatement ce qui a cassé, sans même ouvrir le code.

## `[Fact]` et `[Theory]`

**`[Fact]`** décore un test qui s'exécute une seule fois, avec des données fixes :

```csharp
[Fact]
public void EstPair_NombreImpair_RetourneFalse()
{
    var calc = new Operations();
    Assert.False(calc.EstPair(7));
}
```

**`[Theory]`** avec **`[InlineData]`** permet de jouer le même test avec plusieurs jeux de données différents, sans dupliquer le code :

```csharp
[Theory]
[InlineData(2, true)]
[InlineData(3, false)]
[InlineData(0, true)]
[InlineData(-4, true)]
public void EstPair_DiversCas_RetourneLaBonneValeur(int nombre, bool attendu)
{
    var calc = new Operations();
    bool resultat = calc.EstPair(nombre);
    Assert.Equal(attendu, resultat);
}
```

Chaque ligne `[InlineData]` génère un test distinct — ici, 4 tests en une seule déclaration.

## Les assertions courantes

```csharp
Assert.Equal(attendu, obtenu);          // égalité
Assert.NotEqual(nonAttendu, obtenu);    // différence
Assert.True(condition);                  // doit être vrai
Assert.False(condition);                 // doit être faux
Assert.Null(objet);                      // doit être null
Assert.NotNull(objet);                   // ne doit pas être null
Assert.Empty(collection);               // collection vide
Assert.Contains(element, collection);   // élément présent
```

## Tester qu'une méthode lève une exception

On utilise `Assert.Throws<T>` : si la méthode appelée à l'intérieur ne lève **pas** l'exception attendue, le test échoue.

```csharp
[Fact]
public void Diviser_DiviseurZero_LeveArgumentException()
{
    var calc = new Operations();

    Assert.Throws<ArgumentException>(() => calc.Diviser(10, 0));
}
```

`() => calc.Diviser(10, 0)` est une lambda qui encapsule l'appel à tester. xUnit l'exécute et vérifie qu'elle lève bien `ArgumentException`.

## Ce qu'un test unitaire NE doit PAS faire

- Accéder à une base de données, à un fichier, au réseau.
- Dépendre de l'heure système (`DateTime.Now`) ou d'un état global.
- Tester plusieurs comportements à la fois dans un seul `[Fact]` — un `[Fact]` = un seul scénario.

Ces contraintes peuvent sembler restrictives, mais elles garantissent que les tests sont **rapides** (quelques millisecondes), **déterministes** (même résultat à chaque exécution) et **indépendants** les uns des autres.

**Suite : [13. Test-Driven Development (TDD)](13-tdd.md)**
