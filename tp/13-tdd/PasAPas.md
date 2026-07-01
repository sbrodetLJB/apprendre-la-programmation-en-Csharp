# Pas à pas — TP 13 : TDD — Carnet d'adresses

Relis le cours [13-tdd.md](../../docs/13-tdd.md) avant de commencer. Place-toi dans `tp/13-tdd/Enonce/`.

Ce TP applique le cycle **Red / Green / Refactor** sur le `CarnetAdresses` du TP11 — mais dans l'ordre inverse : tu vas écrire les tests **avant** d'implémenter le code.

La bibliothèque contient deux fichiers :
- `CarnetAdresses/Contact.cs` — déjà complet (même classe que le TP11)
- `CarnetAdresses/CarnetAdresses.cs` — les méthodes lancent toutes `NotImplementedException` : elles sont volontairement vides

Les tests sont à écrire dans `CarnetAdresses.Tests/CarnetAdressesTests.cs`.

```bash
cd tp/13-tdd/Enonce/CarnetAdresses.Tests
dotnet test
```

## La discipline TDD à respecter

Pour chaque TODO :
1. **Écris le test** (🔴 il doit échouer — si la méthode lève `NotImplementedException`, xUnit le détecte comme un échec)
2. **Implémente le minimum** dans `CarnetAdresses.cs` pour le faire passer (🟢)
3. **Relis le code** : est-il lisible ? Y a-t-il une duplication à éliminer ? (🔵 Refactor)
4. Relance `dotnet test` pour confirmer que tout est toujours vert, puis passe au TODO suivant

## TODOs 1-3 : `Ajouter`

### TODO 1 — `Ajouter_ContactValide_RetourneLeContact`

Arrange : crée un nouveau `CarnetAdresses`. Act : appelle `carnet.Ajouter("Lucas", "Martin", "lucas@example.com")`. Assert : vérifie que le résultat n'est pas null, et que `Prenom` et `Email` correspondent.

```csharp
[Fact]
public void Ajouter_ContactValide_RetourneLeContact()
{
    var carnet = new CarnetAdresses();

    Contact contact = carnet.Ajouter("Lucas", "Martin", "lucas@example.com");

    Assert.NotNull(contact);
    Assert.Equal("Lucas", contact.Prenom);
    Assert.Equal("lucas@example.com", contact.Email);
}
```

Lance `dotnet test` : 🔴 il échoue (`NotImplementedException`). Maintenant implémente `Ajouter` dans `CarnetAdresses.cs` :

```csharp
public Contact Ajouter(string prenom, string nom, string email, string? telephone = null)
{
    var contact = new Contact(prochainId, prenom, nom, email, telephone);
    contacts.Add(contact);
    prochainId++;
    return contact;
}
```

Relance : 🟢 passe.

### TODO 2 — `Ajouter_DeuxContacts_IdsIncrementaux`

Ajoute deux contacts et vérifie `c1.Id == 1` et `c2.Id == 2`. `Ajouter` est déjà implémentée : ce test devrait passer immédiatement — c'est normal en TDD, ça confirme que le code déjà écrit couvre ce cas.

### TODO 3 — `Ajouter_EmailVide_LeveArgumentException`

Utilise `Assert.Throws<ArgumentException>(() => carnet.Ajouter("Lucas", "Martin", ""))`. La `ArgumentException` est levée par le constructeur de `Contact` (déjà dans `Contact.cs`, comme au TP11). Ce test devrait aussi passer sans modification supplémentaire.

## TODOs 4-5 : `ListerTout`

### TODO 4 — `ListerTout_CarnetVide_RetourneListeVide`

Lance le test avant d'implémenter. Il échouera sur `NotImplementedException`. Ensuite :

```csharp
public List<Contact> ListerTout()
{
    return contacts;
}
```

Relance : 🟢. Vérifie `Assert.NotNull(contacts)` et `Assert.Empty(contacts)`.

### TODO 5 — `ListerTout_DeuxContactsAjoutes_RetourneDeux`

Ajoute deux contacts, puis vérifie `carnet.ListerTout().Count == 2`. Doit passer sans autre modification.

## TODOs 6-7 : `Supprimer`

### TODO 6 — `Supprimer_IdExistant_RetourneTrue`

Ajoute un contact (id=1), puis appelle `carnet.Supprimer(1)`. Vérifie `Assert.True(resultat)` et `Assert.Empty(carnet.ListerTout())`.

Implémentation minimale quand le test est rouge :

```csharp
public bool Supprimer(int id)
{
    var contact = contacts.FirstOrDefault(c => c.Id == id);
    if (contact == null) return false;
    contacts.Remove(contact);
    return true;
}
```

### TODO 7 — `Supprimer_IdInexistant_RetourneFalse`

Même méthode, id inexistant `999`. Doit passer sans modification supplémentaire.

## TODOs 8-10 : `RechercherParNom`

### TODO 8 — `RechercherParNom_MotCleMatchPrenom_RetourneLeContact`

Arrange : deux contacts. Act : `RechercherParNom("Lucas")`. Assert : `Assert.Single(resultats)` (exactement 1 résultat) et `resultats[0].Prenom == "Lucas"`.

Implémentation quand le test est rouge :

```csharp
public List<Contact> RechercherParNom(string motCle)
{
    return contacts
        .Where(c => c.NomComplet.Contains(motCle, StringComparison.OrdinalIgnoreCase))
        .ToList();
}
```

### TODO 9 — `RechercherParNom_InsensibleCasse_TrouveLeContact`

Recherche `"martin"` (minuscules) et vérifie qu'on trouve le contact `"Martin"`. Doit passer sans modification — le `OrdinalIgnoreCase` s'en charge déjà.

### TODO 10 — `RechercherParNom_AucuneCorrespondance_RetourneListeVide`

Recherche un mot-clé absent. Vérifie `Assert.Empty(resultats)`.

## Vérifier son travail

À la fin, `dotnet test` doit afficher **10 tests passed, 0 failed**. Ouvre `../Corrige/CarnetAdresses.Tests/CarnetAdressesTests.cs` pour comparer — la structure des tests et les assertions doivent être quasi-identiques.

**Suite : [TP 14 — Bonnes pratiques et SOLID](../14-bonnes-pratiques/PasAPas.md)**
