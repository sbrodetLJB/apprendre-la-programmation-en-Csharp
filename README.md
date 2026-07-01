# Apprendre la programmation en C#

Support de cours progressif pour apprendre les bases de la programmation **procédurale** puis **orientée objet** avec le langage **C#** et la plateforme **.NET**.

Chaque leçon contient :
- un **cours** dans [docs/](docs/) (markdown, en français) ;
- un **TP** correspondant dans [tp/](tp/), avec un dossier `Enonce/` (le travail à faire, avec des `// TODO`), un fichier `PasAPas.md` (un guide pas à pas qui t'accompagne question par question, en supposant que tu ne connais que ce qui a été vu dans les leçons précédentes) et un dossier `Corrige/` (la solution complète et exécutable).

## Prérequis

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 ou plus récent)
- [Visual Studio Code](https://code.visualstudio.com/) avec l'extension **C# Dev Kit** (recommandé)

Vérifier l'installation :

```bash
dotnet --version
```

## Plan du cours

| # | Leçon | TP |
|---|---|---|
| 1 | [Introduction à C# et .NET](docs/01-introduction-csharp-dotnet.md) | [tp/01-introduction](tp/01-introduction) |
| 2 | [Variables, types et opérateurs](docs/02-variables-types-operateurs.md) | [tp/02-variables-types](tp/02-variables-types) |
| 3 | [Entrées/sorties et conversions](docs/03-entrees-sorties-conversions.md) | [tp/03-entrees-sorties](tp/03-entrees-sorties) |
| 4 | [Structures de contrôle (if/switch)](docs/04-structures-de-controle.md) | [tp/04-structures-de-controle](tp/04-structures-de-controle) |
| 5 | [Boucles](docs/05-boucles.md) | [tp/05-boucles](tp/05-boucles) |
| 6 | [Méthodes](docs/06-methodes.md) | [tp/06-methodes](tp/06-methodes) |
| 7 | [Tableaux et List\<T\>](docs/07-tableaux-et-listes.md) | [tp/07-tableaux-et-listes](tp/07-tableaux-et-listes) |
| 8 | [POO : classes, objets, encapsulation](docs/08-poo-classes-objets.md) | [tp/08-poo-classes-objets](tp/08-poo-classes-objets) |
| 9 | [Héritage, polymorphisme et interfaces](docs/09-heritage-polymorphisme-interfaces.md) | [tp/09-heritage-polymorphisme-interfaces](tp/09-heritage-polymorphisme-interfaces) |
| 10 | [Exceptions](docs/10-exceptions.md) | [tp/10-exceptions](tp/10-exceptions) |
| 11 | [Mini-projet de synthèse : carnet d'adresses console](docs/11-projet-carnet-adresses.md) | [tp/11-projet-carnet-adresses](tp/11-projet-carnet-adresses) |
| 12 | [Tests unitaires avec xUnit](docs/12-tests-unitaires-xunit.md) | [tp/12-tests-unitaires](tp/12-tests-unitaires) |
| 13 | [Test-Driven Development (TDD)](docs/13-tdd.md) | [tp/13-tdd](tp/13-tdd) |
| 14 | [Bonnes pratiques et SOLID](docs/14-bonnes-pratiques-solid.md) | [tp/14-bonnes-pratiques](tp/14-bonnes-pratiques) |

## Comment travailler

1. Lire le cours de la leçon dans `docs/`.
2. Ouvrir `tp/0X-.../Enonce/`, compléter les `// TODO`.
3. Bloqué·e ? Ouvrir `tp/0X-.../PasAPas.md`, qui guide la résolution question par question, en expliquant le "pourquoi" de chaque étape.
4. Exécuter avec :

   ```bash
   cd tp/0X-.../Enonce
   dotnet run
   ```
5. Comparer avec `tp/0X-.../Corrige/` si besoin (à n'ouvrir qu'après avoir essayé !).

## Lien avec le cours API

Le mini-projet final (leçon 11) reprend volontairement le **carnet d'adresses** du cours [apprendre_les_API](https://github.com/sbrodetLJB/apprendre_les_API) — mais cette fois en version console, orientée objet, sans API ni réseau. Une bonne occasion de voir comment le même problème se modélise différemment selon le contexte.
