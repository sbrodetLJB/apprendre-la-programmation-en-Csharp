// TP 2 : Variables, types et opérateurs -- Corrigé

int annee = 2026;
string prenom = "Lucas";
int anneeNaissance = 2009;
int age = annee - anneeNaissance;

Console.WriteLine($"{prenom} a environ {age} ans.");

double prixHT = 49.90;
double tauxTva = 0.20;
double prixTTC = prixHT + prixHT * tauxTva;
Console.WriteLine($"Prix TTC : {prixTTC} €");

bool estEnPromo = true;
string message = estEnPromo ? "Cet article est en promotion !" : "Cet article est au prix normal.";
Console.WriteLine(message);
