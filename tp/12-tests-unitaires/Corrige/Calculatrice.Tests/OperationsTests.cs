// TP 12 : Tests unitaires avec xUnit -- Corrigé

using Calculatrice;

namespace Calculatrice.Tests;

public class OperationsTests
{
    // --- Additionner ---

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

    [Theory]
    [InlineData(3, 4, 7)]
    [InlineData(-2, 5, 3)]
    [InlineData(0, 0, 0)]
    public void Additionner_DiversCas_RetourneLaSomme(int a, int b, int attendu)
    {
        var calc = new Operations();
        Assert.Equal(attendu, calc.Additionner(a, b));
    }

    // --- EstPair ---

    [Theory]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(0, true)]
    [InlineData(-4, true)]
    public void EstPair_DiversCas_RetourneLaBonneValeur(int nombre, bool attendu)
    {
        var calc = new Operations();
        Assert.Equal(attendu, calc.EstPair(nombre));
    }

    // --- Diviser ---

    [Fact]
    public void Diviser_CasNormal_RetourneLeBonResultat()
    {
        var calc = new Operations();
        Assert.Equal(5.0, calc.Diviser(10, 2));
    }

    [Fact]
    public void Diviser_DiviseurZero_LeveArgumentException()
    {
        var calc = new Operations();
        Assert.Throws<ArgumentException>(() => calc.Diviser(10, 0));
    }

    // --- EstPalindrome ---

    [Theory]
    [InlineData("radar", true)]
    [InlineData("bonjour", false)]
    [InlineData("", true)]
    [InlineData("Kayak", true)]
    public void EstPalindrome_DiversCas_RetourneLaBonneValeur(string s, bool attendu)
    {
        var calc = new Operations();
        Assert.Equal(attendu, calc.EstPalindrome(s));
    }
}
