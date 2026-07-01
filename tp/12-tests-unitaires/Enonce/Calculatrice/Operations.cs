namespace Calculatrice;

public class Operations
{
    /// <summary>Additionne deux entiers.</summary>
    public int Additionner(int a, int b)
    {
        return a + b;
    }

    /// <summary>
    /// Divise a par b.
    /// Lève ArgumentException si b == 0.
    /// </summary>
    public double Diviser(double a, double b)
    {
        if (b == 0)
            throw new ArgumentException("Le diviseur ne peut pas être zéro.");
        return a / b;
    }

    /// <summary>Renvoie true si la chaîne est un palindrome (ignorant la casse).</summary>
    public bool EstPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s)) return true;
        string lower = s.ToLower();
        string reversed = new string(lower.Reverse().ToArray());
        return lower == reversed;
    }

    /// <summary>Retourne true si n est pair.</summary>
    public bool EstPair(int n)
    {
        return n % 2 == 0;
    }
}
