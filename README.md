Acceptanstester
Testplan
Jag har identifierat tre affärskritiska delar av koden som behöver testas:

Konvertering från SEK till Yen

Risker: Felaktig växelkurs, felaktigt belopp
Konvertering från Yen till SEK

Risker: Felaktig växelkurs, felaktigt belopp
Konvertering från SEK till EURO

Risker: Felaktig växelkurs, felaktigt belopp
Testfall
1. Konvertering från SEK till Yen
Testfall 1.1: Konvertering med korrekt belopp och växelkurs
Förväntat resultat: 100 SEK ska konverteras till 1400 Yen
Kod:
csharp
Kopiera kod
[TestMethod]
public void FromSekToYen_100_SEK_Return_14000_Yen()
{
    // Arrange
    Converter systemUnderTesting = new Converter()
    {
        Yen = 14.0M
    };

    // Act
    var actual = systemUnderTesting.FromSekToYen(100);
    var expected = 1400;

    // Assert
    Assert.AreEqual(expected, actual);
}
2. Konvertering från Yen till SEK
Testfall 2.1: Konvertering med korrekt belopp och växelkurs
Förväntat resultat: 1400 Yen ska konverteras till 100 SEK
Kod:
csharp
Kopiera kod
[TestMethod]
public void FromYenToSek_14000_Yen_Return_100_SEK()
{
    // Arrange
    Converter systemUnderTesting = new Converter()
    {
        Yen = 14.0M
    };

    // Act
    var actual = systemUnderTesting.FromYenToSek(1400);
    var expected = 100;

    // Assert
    Assert.AreEqual(expected, actual);
}
3. Konvertering från SEK till EURO
Testfall 3.1: Konvertering med korrekt belopp och växelkurs
Förväntat resultat: 100 SEK ska konverteras till 8.9 EURO
Kod:
csharp
Kopiera kod
[TestMethod]
public void FromSekToEURO_100_SEK_Return_8_9_EURO()
{
    // Arrange
    Converter systemUnderTesting = new Converter()
    {
        Euro = 0.089M
    };

    // Act
    var actual = systemUnderTesting.FromSekToEur(100);
    var expected = 8.9M;

    // Assert
    Assert.AreEqual(expected, actual);
}
