// TP 13 : TDD — Carnet d'adresses
//
// Consigne : n'implémente PAS les méthodes de CarnetAdresses avant d'avoir d'abord
// écrit et vu échouer (🔴 Red) le test correspondant. Ensuite seulement,
// implémente le minimum pour le faire passer (🟢 Green), puis améliore (🔵 Refactor).
//
// Exécute les tests avec : dotnet test
//
// Les tests sont regroupés par méthode. Complète-les dans l'ordre.

using CarnetAdresses;

namespace CarnetAdresses.Tests;

public class CarnetAdressesTests
{
    // =========================================================
    // AJOUTER
    // =========================================================

    // TODO TDD 1 — [Fact]
    // Ajouter un contact valide doit retourner un contact non-null
    // dont le Prenom et l'Email correspondent à ce qu'on a passé.
    // Nom de test suggéré : Ajouter_ContactValide_RetourneLeContact

    // TODO TDD 2 — [Fact]
    // Ajouter deux contacts doit incrémenter l'Id : le premier a Id=1, le second Id=2.
    // Nom de test suggéré : Ajouter_DeuxContacts_IdsIncrementaux

    // TODO TDD 3 — [Fact]
    // Ajouter un contact avec un email vide doit lever ArgumentException.
    // Nom de test suggéré : Ajouter_EmailVide_LeveArgumentException

    // =========================================================
    // LISTERTOUT
    // =========================================================

    // TODO TDD 4 — [Fact]
    // Un carnet vide doit retourner une liste vide (pas null, pas une exception).
    // Nom de test suggéré : ListerTout_CarnetVide_RetourneListeVide

    // TODO TDD 5 — [Fact]
    // Après avoir ajouté 2 contacts, ListerTout doit retourner 2 éléments.
    // Nom de test suggéré : ListerTout_DeuxContactsAjoutes_RetourneDeux

    // =========================================================
    // SUPPRIMER
    // =========================================================

    // TODO TDD 6 — [Fact]
    // Supprimer un id existant doit retourner true et réduire le Count à 0.
    // Nom de test suggéré : Supprimer_IdExistant_RetourneTrue

    // TODO TDD 7 — [Fact]
    // Supprimer un id inexistant doit retourner false.
    // Nom de test suggéré : Supprimer_IdInexistant_RetourneFalse

    // =========================================================
    // RECHERCHERPARNOM
    // =========================================================

    // TODO TDD 8 — [Fact]
    // Rechercher avec un mot-clé qui correspond au prénom d'un contact
    // doit retourner exactement ce contact.
    // Nom de test suggéré : RechercherParNom_MotCleMatchPrenom_RetourneLeContact

    // TODO TDD 9 — [Fact]
    // La recherche doit être insensible à la casse ("martin" trouve "Martin").
    // Nom de test suggéré : RechercherParNom_InsensibleCasse_TrouveLeContact

    // TODO TDD 10 — [Fact]
    // Rechercher avec un mot-clé sans correspondance doit retourner une liste vide.
    // Nom de test suggéré : RechercherParNom_AucuneCorrespondance_RetourneListeVide
}
