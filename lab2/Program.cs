using System;
using System.Linq;

class Tetrahedron
{
    private double[] A;
    private double[] B;
    private double[] C; //зберігаємо координати 4 вершин тетраедра
    private double[] D;

    public Tetrahedron(double[] a, double[] b, double[] c, double[] d)
    {
        A = a; B = b; C = c; D = d;   //приймання 4 точок у вигляді масивів  х y z 
    }

    private double[] Vector(double[] p1, double[] p2)
        => new double[] { p2[0] - p1[0], p2[1] - p1[1], p2[2] - p1[2] };  

    private double[] CrossProduct(double[] v1, double[] v2)   //векторний добуток 
        => new double[]
        {
            v1[1] * v2[2] - v1[2] * v2[1],
            v1[2] * v2[0] - v1[0] * v2[2],  //координати  x y z 
            v1[0] * v2[1] - v1[1] * v2[0]
        };

    private double DotProduct(double[] v1, double[] v2) //скалярний добуток 
        => v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2];

    public double Volume()
    {
        double[] AB = Vector(A, B);
        double[] AC = Vector(A, C);
        double[] AD = Vector(A, D);
        double[] cross = CrossProduct(AC, AD);
        double scalar = DotProduct(AB, cross);
        return Math.Abs(scalar) / 6.0; //змішаний добуток поділений на 6
    }
}

class Program
{
    static double[] ReadPoint(string name)
    {
        while (true)
        {
            Console.Write($"{name} (x y z): ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) { Console.WriteLine("Введіть три числа."); continue; }
            var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) { Console.WriteLine("Потрібно ввести 3 числа, спробуйте ще раз."); continue; }
            try { return parts.Select(p => double.Parse(p)).ToArray(); }
            catch { Console.WriteLine("Некоректні числа, спробуйте ще раз."); }
        }
    }

    static void Main()
    {
        Console.WriteLine("Обчислення об'єму тетраедра.");
        double[] A = ReadPoint("A");
        double[] B = ReadPoint("B");
        double[] C = ReadPoint("C"); //зчитуємо 4 вершини які будуть задані
        double[] D = ReadPoint("D");

        var tetra = new Tetrahedron(A, B, C, D);
        Console.WriteLine($"Об'єм тетраедра = {tetra.Volume()}");
        Console.WriteLine("Натисніть Enter щоб вийти...");
        Console.ReadLine();
    }
}
