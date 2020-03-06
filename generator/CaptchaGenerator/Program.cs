using System;
using System.Linq;

namespace CaptchaGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var codes = Enumerable
                .Range(0, 99999)
                .OrderBy(s => Guid.NewGuid())
                .Take(1000)
                .Select(s => s.ToString().PadLeft(5, '0'))
                .ToList();

            foreach (var code in codes)
            {
                Captcha.Create(code, "");
            }

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
