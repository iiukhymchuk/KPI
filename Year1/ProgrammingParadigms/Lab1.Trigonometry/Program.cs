// y = sqrt(abs(sin(a) - (4 * ln(b)) / (c pow d) ))
// a = -1.49, b = 23.4, c = 1.23, d = 2.542

namespace Lab1.Trigonometry
{
    using static System.Math;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = -1.49;
            var b = 23.4;
            var c = 1.23;
            var d = 2.542;

            var y = Sqrt(Abs(Sin(a) - ((4 * Log(b)) / Pow(c, d))));
            System.Console.WriteLine("Given y = sqrt(abs(sin(a) - (4 * ln(b)) / (c pow d) ))");
            System.Console.WriteLine($"y equals {y:0.####} for a = {a}, b = {b}, c = {c}, d = {d}");
        }
    }
}
