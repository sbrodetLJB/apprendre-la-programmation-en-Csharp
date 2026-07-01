# Pas à pas — TP 12 : Tests unitaires avec xUnit

Relis le cours [12-tests-unitaires-xunit.md](../../docs/12-tests-unitaires-xunit.md) avant de commencer. Place-toi dans `tp/12-tests-unitaires/Enonce/`.

Ce TP est différent des précédents : il n'y a pas de `Program.cs` à compléter, mais un **projet de tests** (`Calculatrice.Tests/`) à remplir. Le code que tu vas tester est déjà écrit dans `Calculatrice/Operations.cs` — lis-le avant de commencer pour comprendre ce que chaque méthode fait.

## Comment exécuter les tests

```bash
cd tp/12-tests-unitaires/Enonce/Calculatrice.Tests
dotnet test
```

Tant que les méthodes de test sont vides (juste des `// TODO`), `dotnet test` dira "0 tests ran" — c'est normal. Chaque test que tu ajoutes augmentera ce compteur.

## Structure du fichier de tests

Ouvre `Calculatrice.Tests/OperationsTests.cs`. La classe `OperationsTests` contient des commentaires `// TODO` pour chaque test à écrire. Tous les tests iront dans cette classe.

## TODO 1 : premier `[Fact]` — `Additionner`

Le cours montre exactement ce patron. Rappelle-toi : **Arrange / Act / Assert**.

1. Arrange : crée une instance de `Operations` (`var calc = new Operations();`)
2. Act : appelle `calc.Additionner(3, 4)` et stocke le résultat
3. Assert : vérifie avec `Assert.Equal(7, resultat)`

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

Lance `dotnet test` : tu dois voir **1 test passed**. Si tu vois une erreur de compilation, relis attentivement le message — les plus fréquentes ici sont un `using` manquant ou une typo dans le nom de classe.

## TODO 2 : `[Theory]` pour `Additionner`

Un `[Theory]` + `[InlineData]` permet de jouer le même scénario avec plusieurs valeurs. Le test reçoit les valeurs comme paramètres :

```csharp
[Theory]
[InlineData(3, 4, 7)]
[InlineData(-2, 5, 3)]
[InlineData(0, 0, 0)]
public void Additionner_DiversCas_RetourneLaSomme(int a, int b, int attendu)
{
    var calc = new Operations();
    Assert.Equal(attendu, calc.Additionner(a, b));
}
```

Lance `dotnet test` : 3 tests supplémentaires (un par `[InlineData]`).

## TODO 3 : `[Theory]` pour `EstPair`

Même principe : paramètre entier + paramètre booléen attendu. Les 4 cas sont : `2→true`, `3→false`, `0→true`, `-4→true`. Construis le test sur le même modèle que le TODO 2.

## TODO 4 : `[Fact]` pour `Diviser` cas normal

Très similaire au TODO 1. Vérifie que `Diviser(10, 2)` renvoie `5.0`. Utilise `Assert.Equal(5.0, calc.Diviser(10, 2))`.

## TODO 5 : `[Fact]` pour `Diviser` par zéro — tester une exception

C'est la nouveauté de ce TP par rapport aux précédents. Le cours présente `Assert.Throws<T>` : si la lambda passée **ne lève pas** l'exception attendue, le test échoue.

```csharp
[Fact]
public void Diviser_DiviseurZero_LeveArgumentException()
{
    var calc = new Operations();

    Assert.Throws<ArgumentException>(() => calc.Diviser(10, 0));
}
```

Pourquoi une lambda `() => ...` ? Parce que si on écrivait directement `calc.Diviser(10, 0)` comme argument, l'exception serait levée **avant** que `Assert.Throws` n'ait une chance de la capturer. La lambda retarde l'exécution — `Assert.Throws` l'appelle lui-même, surveille si l'exception est levée, et échoue si elle ne l'est pas.

## TODO 6 : `[Theory]` pour `EstPalindrome`

Quatre cas : `"radar"→true`, `"bonjour"→false`, `""→true`, `"Kayak"→true` (insensible à la casse — regarde l'implémentation dans `Operations.cs` pour comprendre pourquoi `"Kayak"` est un palindrome ici).

## Vérifier son travail

Lance `dotnet test` une dernière fois. Tu dois voir **au minimum 10 tests passed** (1 + 3 + 4 + 1 + 1 + 4, selon le nombre d'`InlineData`). Compare avec `../Corrige/Calculatrice.Tests/OperationsTests.cs`.

**Suite : [TP 13 — TDD](../13-tdd/PasAPas.md)**
