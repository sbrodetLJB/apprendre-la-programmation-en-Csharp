namespace Calculatrice;

public class Operations
{
    public int Additionner(int a, int b)
    {
        return a + b;
    }

    public double Diviser(double a, double b)
    {
        if (b == 0)
            throw new ArgumentException("Le diviseur ne peut pas être zéro.");
        return a / b;
    }

    public bool EstPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s)) return true;
        string lower = s.ToLower();
        string reversed = new string(lower.Reverse().ToArray());
        return lower == reversed;
    }

    public bool EstPair(int n)
    {
        return n % 2 == 0;
    }
}
