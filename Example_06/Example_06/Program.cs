using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example_06.ChainOfResponsibility;

namespace Example_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var bancomat = new HomeworkBancomat();
            var result = bancomat.Cash("2050 рублей");
            Console.WriteLine(result);
            result = bancomat.Cash("2033$");
            Console.WriteLine(result);
            result = bancomat.Cash("2080 eur");
            Console.WriteLine(result);
        }
    }
}
