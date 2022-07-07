namespace Calculadora.CA
{
    class Calculadora
    {
        public static void Main(string[] arg)
        {
            Calcular();
        }

        public static void Calcular()
        {
            while (true)
            {
                Console.WriteLine("_____________________________________________");
                Console.WriteLine(" Ingrese Calculo o presione enter para salir \n(Solo Suba, resta, multiplicacion y divicion)");
                Console.WriteLine("---------------------------------------------");
                var Res = Console.ReadLine();
                if (String.IsNullOrEmpty(Res)) break;
                Console.WriteLine(CA.Calculadora.DataHandling(Res));
            }
        }

        public static bool IsValid(string a)
        {
            // Valida el string
            var CalculosValidos = new List<Char>() { '*', '+', '-', '/', '.'};
            char[] input = a.ToCharArray();

            if (a.Any(Char.IsLetter)) return false;
            foreach (char c in CalculosValidos) if (c == a.First()) return false;
            foreach (char c in input) if (!CalculosValidos.Contains(c) && !Char.IsDigit(c)) return false;
            return true;
        }

        public static string DataHandling(string a)
        {
            // Recive y convierte el String

            var calculos = new List<char>() { '*', '+', '-', '/' };
            List<char> delimiter = new();

            if (!CA.Calculadora.IsValid(a)) return "No valido";

            //Mover a lista
            foreach (char c in a) if (calculos.Contains(c)) delimiter.Add(c);
            List<double> numeros = new (Array.ConvertAll(a.Split(delimiter.ToArray()), double.Parse));


            //Cacular y return
            return $"El resultado es: {CA.Calculadora.Calculo(delimiter, numeros)}.";
        }

        public static double Calculo(List<char> x, List<double> z)
        {
            //identifica el calculo que se quiere hacer y lo realiza

            double a = 0;

            while (0 < x.Count)
            {
                if (x.Contains('*'))
                {
                    var b = x.IndexOf('*');
                    a = z[b] * z[b + 1];
                    x.RemoveAt(b); z.RemoveRange(b, 2); z.Insert(b, a);
                }
                if (x.Contains('/'))
                {
                    var b = x.IndexOf('/');
                    a = z[b] / z[b + 1];
                    x.RemoveAt(b); z.RemoveRange(b, 2); z.Insert(b, a);
                }
                if (x.Contains('+'))
                {
                    var b = x.IndexOf('+');
                    a = z[b] + z[b + 1];
                    x.RemoveAt(b); z.RemoveRange(b, 2); z.Insert(b, a);
                }
                if (x.Contains('-'))
                {
                    var b = x.IndexOf('-');
                    a = z[b] - z[b + 1];
                    x.RemoveAt(b); z.RemoveRange(b, 2); z.Insert(b, a);
                }
                
            }
            return a;
        }
    }
}