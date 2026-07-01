# 13. Test-Driven Development (TDD)

## Qu'est-ce que le TDD ?

Le **Test-Driven Development** (développement piloté par les tests) est une méthode de travail qui inverse l'ordre habituel : on écrit d'abord le test, **avant même d'écrire le code** qu'il va tester. Ce test échoue immédiatement (c'est normal et attendu), puis on écrit le code minimal pour le faire passer, puis on améliore.

## Le cycle Red / Green / Refactor

Ce cycle se répète pour chaque petite fonctionnalité :

```
🔴 Red    — Écrire un test qui décrit le comportement voulu.
            Il échoue car le code n'existe pas encore.

🟢 Green  — Écrire le code le plus simple possible pour faire passer le test.
            Pas d'optimisation, pas d'anticipation : juste ce qu'il faut.

🔵 Refactor — Améliorer le code (lisibilité, suppression de duplication...)
              sans en changer le comportement. Les tests garantissent qu'on
              ne casse rien pendant cette étape.
```

Puis on recommence avec la fonctionnalité suivante.

## Pourquoi cette discipline ?

- **Conception** : écrire le test d'abord force à réfléchir à l'interface (comment appelle-t-on cette méthode ? que renvoie-t-elle ?) avant de se plonger dans l'implémentation.
- **Filet de sécurité** : à tout moment, tous les tests passent. Si un refactoring casse quelque chose, on le sait immédiatement.
- **Documentation vivante** : les tests décrivent le comportement attendu du code, et ils restent vrais — contrairement aux commentaires qui peuvent devenir obsolètes.

## Exemple : une méthode `Max`

**Étape 1 — Red** : on écrit un test qui décrit ce qu'on veut.

```csharp
[Fact]
public void Max_PremierPlusGrand_RetournePremier()
{
    Assert.Equal(7, Calculs.Max(7, 3));
}
```

Le compilateur se plaint : `Calculs` n'existe pas. C'est normal. On crée la classe avec une méthode vide (qui va faire échouer le test, pas le compilateur) :

```csharp
public static class Calculs
{
    public static int Max(int a, int b) => 0; // implémentation volontairement fausse
}
```

`dotnet test` → test 🔴 **fail** : `Assert.Equal(7, 0)` échoue.

**Étape 2 — Green** : on écrit le minimum pour passer.

```csharp
public static int Max(int a, int b) => a > b ? a : b;
```

`dotnet test` → test 🟢 **pass**.

**Étape 3 — Refactor** : ici il n'y a rien à améliorer, on passe à la fonctionnalité suivante.

**Étape 1 (bis) — Red** : nouveau test, nouveau cas.

```csharp
[Fact]
public void Max_Egaux_RetourneUneDesDeuxValeurs()
{
    Assert.Equal(5, Calculs.Max(5, 5));
}
```

`dotnet test` → 🟢 passe déjà (l'implémentation couvre ce cas). On continue.

## TDD et le carnet d'adresses

Pour le TP, on va appliquer ce cycle sur la classe `CarnetAdresses` (vue en leçon 11) — mais cette fois on commencera avec des stubs (des méthodes qui ne font rien / retournent une valeur par défaut), et on les fera évoluer une à une en suivant le cycle Red/Green/Refactor.

L'objectif n'est pas de tout réécrire from scratch : c'est de **ressentir** la discipline TDD sur un code qu'on connaît déjà.

**Suite : [14. Bonnes pratiques et SOLID](14-bonnes-pratiques-solid.md)**
