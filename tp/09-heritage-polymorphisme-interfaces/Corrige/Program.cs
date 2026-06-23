// TP 9 : Héritage, polymorphisme et interfaces -- Corrigé

List<Forme> formes = new List<Forme>
{
    new Carre(4),
    new Cercle(3),
};

foreach (Forme forme in formes)
{
    forme.AfficherAire();
}

Carre carre = new Carre(4);
Console.WriteLine($"Position avant déplacement : ({carre.X}, {carre.Y})");
carre.Deplacer(2, 3);
Console.WriteLine($"Position après déplacement : ({carre.X}, {carre.Y})");

abstract class Forme
{
    public abstract double CalculerAire();

    public void AfficherAire()
    {
        Console.WriteLine($"Aire : {CalculerAire()}");
    }
}

interface IDeplacable
{
    void Deplacer(int dx, int dy);
}

class Carre : Forme, IDeplacable
{
    public double Cote { get; set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public Carre(double cote)
    {
        Cote = cote;
        X = 0;
        Y = 0;
    }

    public override double CalculerAire()
    {
        return Cote * Cote;
    }

    public void Deplacer(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }
}

class Cercle : Forme
{
    public double Rayon { get; set; }

    public Cercle(double rayon)
    {
        Rayon = rayon;
    }

    public override double CalculerAire()
    {
        return Math.PI * Rayon * Rayon;
    }
}
