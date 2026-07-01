// TP 13 : TDD — Carnet d'adresses -- Corrigé

using CarnetAdresses;

namespace CarnetAdresses.Tests;

public class CarnetAdressesTests
{
    // =========================================================
    // AJOUTER
    // =========================================================

    [Fact]
    public void Ajouter_ContactValide_RetourneLeContact()
    {
        // Arrange
        var carnet = new CarnetAdresses();

        // Act
        Contact contact = carnet.Ajouter("Lucas", "Martin", "lucas@example.com");

        // Assert
        Assert.NotNull(contact);
        Assert.Equal("Lucas", contact.Prenom);
        Assert.Equal("lucas@example.com", contact.Email);
    }

    [Fact]
    public void Ajouter_DeuxContacts_IdsIncrementaux()
    {
        var carnet = new CarnetAdresses();

        Contact c1 = carnet.Ajouter("Lucas", "Martin", "lucas@example.com");
        Contact c2 = carnet.Ajouter("Emma", "Bernard", "emma@example.com");

        Assert.Equal(1, c1.Id);
        Assert.Equal(2, c2.Id);
    }

    [Fact]
    public void Ajouter_EmailVide_LeveArgumentException()
    {
        var carnet = new CarnetAdresses();

        Assert.Throws<ArgumentException>(() => carnet.Ajouter("Lucas", "Martin", ""));
    }

    // =========================================================
    // LISTERTOUT
    // =========================================================

    [Fact]
    public void ListerTout_CarnetVide_RetourneListeVide()
    {
        var carnet = new CarnetAdresses();

        List<Contact> contacts = carnet.ListerTout();

        Assert.NotNull(contacts);
        Assert.Empty(contacts);
    }

    [Fact]
    public void ListerTout_DeuxContactsAjoutes_RetourneDeux()
    {
        var carnet = new CarnetAdresses();
        carnet.Ajouter("Lucas", "Martin", "lucas@example.com");
        carnet.Ajouter("Emma", "Bernard", "emma@example.com");

        Assert.Equal(2, carnet.ListerTout().Count);
    }

    // =========================================================
    // SUPPRIMER
    // =========================================================

    [Fact]
    public void Supprimer_IdExistant_RetourneTrue()
    {
        var carnet = new CarnetAdresses();
        carnet.Ajouter("Lucas", "Martin", "lucas@example.com");

        bool resultat = carnet.Supprimer(1);

        Assert.True(resultat);
        Assert.Empty(carnet.ListerTout());
    }

    [Fact]
    public void Supprimer_IdInexistant_RetourneFalse()
    {
        var carnet = new CarnetAdresses();

        bool resultat = carnet.Supprimer(999);

        Assert.False(resultat);
    }

    // =========================================================
    // RECHERCHERPARNOM
    // =========================================================

    [Fact]
    public void RechercherParNom_MotCleMatchPrenom_RetourneLeContact()
    {
        var carnet = new CarnetAdresses();
        carnet.Ajouter("Lucas", "Martin", "lucas@example.com");
        carnet.Ajouter("Emma", "Bernard", "emma@example.com");

        List<Contact> resultats = carnet.RechercherParNom("Lucas");

        Assert.Single(resultats);
        Assert.Equal("Lucas", resultats[0].Prenom);
    }

    [Fact]
    public void RechercherParNom_InsensibleCasse_TrouveLeContact()
    {
        var carnet = new CarnetAdresses();
        carnet.Ajouter("Lucas", "Martin", "lucas@example.com");

        List<Contact> resultats = carnet.RechercherParNom("martin");

        Assert.Single(resultats);
    }

    [Fact]
    public void RechercherParNom_AucuneCorrespondance_RetourneListeVide()
    {
        var carnet = new CarnetAdresses();
        carnet.Ajouter("Lucas", "Martin", "lucas@example.com");

        List<Contact> resultats = carnet.RechercherParNom("Dupont");

        Assert.Empty(resultats);
    }
}
