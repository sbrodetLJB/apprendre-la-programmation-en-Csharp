// TP 12 : Tests unitaires avec xUnit
//
// Le code à tester se trouve dans le projet Calculatrice (Operations.cs).
// Exécute les tests avec : dotnet test
//
// CONSIGNES :
//
// 1. [Fact] Additionner
//    Écris un test vérifiant que Additionner(3, 4) renvoie 7.
//    Nomme-le : Additionner_DeuxPositifs_RetourneLaSomme
//
// 2. [Theory] Additionner avec plusieurs cas
//    Écris un test [Theory] pour Additionner avec au moins 3 [InlineData] :
//    un cas avec deux positifs, un avec un négatif, un avec deux zéros.
//
// 3. [Theory] EstPair avec plusieurs cas
//    Écris un test [Theory] pour EstPair couvrant : 2 (pair), 3 (impair), 0 (pair), -4 (pair).
//
// 4. [Fact] Diviser cas normal
//    Vérifie que Diviser(10, 2) renvoie 5.
//
// 5. [Fact] Diviser par zéro
//    Vérifie que Diviser(10, 0) lève bien une ArgumentException.
//    Utilise Assert.Throws<ArgumentException>(() => ...).
//
// 6. [Theory] EstPalindrome avec plusieurs cas
//    Teste : "radar" (true), "bonjour" (false), "" (true), "Kayak" (true, ignorant la casse).

using Calculatrice;

namespace Calculatrice.Tests;

public class OperationsTests
{
    // TODO 1 : Additionner_DeuxPositifs_RetourneLaSomme

    // TODO 2 : Additionner_Theory (au moins 3 InlineData)

    // TODO 3 : EstPair_Theory (au moins 4 InlineData)

    // TODO 4 : Diviser_CasNormal_RetourneLeBonResultat

    // TODO 5 : Diviser_DiviseurZero_LeveArgumentException

    // TODO 6 : EstPalindrome_Theory (au moins 4 InlineData)
}
