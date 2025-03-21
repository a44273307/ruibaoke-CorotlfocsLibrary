using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorotlfocsLibrary;

namespace consleusp
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            Console.WriteLine("3 + 5 = " + calc.Add(3, 5));
            Console.ReadKey();
        }
    }
}
