using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace ConsoleNetflix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Netflix!");
            DataContext.Login();
        }
    }
}

