using System;

namespace Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            string con = "/Data/index.html";
            List<string> vpaths = new List<string>() { "/Data/index.html", "/Data/about.html", "/Images/eye.jpg", "/Images/moon.jpg" };
            Console.WriteLine(vpaths.Single(x => con.Contains(x)));
            foreach (var i in vpaths)
                Console.WriteLine(i);
        }
    }
}