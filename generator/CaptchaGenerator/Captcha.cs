using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace CaptchaGenerator
{
    public class Captcha
    {
        private const string SymbolsArray = "0123456789";

        public static void Create(string code, string path)
        {
            var bitmap = new Bitmap(80, 30);
            var gr = Graphics.FromImage(bitmap);

            var r = new Random();

            var bgBrush = GetRandomBrush(r, true);
            gr.FillRectangle(bgBrush, 0, 0, bitmap.Width, bitmap.Height);

            gr.DrawString(code[0].ToString(), GetRandomFont(r), GetRandomBrush(r, false), r.Next(0, 5), r.Next(1, 4));
            gr.DrawString(code[1].ToString(), GetRandomFont(r), GetRandomBrush(r, false), r.Next(15, 25), r.Next(1, 4));
            gr.DrawString(code[2].ToString(), GetRandomFont(r), GetRandomBrush(r, false), r.Next(25, 35), r.Next(1, 4));
            gr.DrawString(code[3].ToString(), GetRandomFont(r), GetRandomBrush(r, false), r.Next(40, 50), r.Next(1, 4));
            gr.DrawString(code[4].ToString(), GetRandomFont(r), GetRandomBrush(r, false), r.Next(55, 65), r.Next(1, 4));
            gr.Save();

            bitmap.Save($"{path}{code}.jpg", ImageFormat.Jpeg);
        }

        public static void CreateRandom(string path)
        {
            var r = new Random();

            var num1 = SymbolsArray[r.Next(0, SymbolsArray.Length - 1)].ToString();
            var num2 = SymbolsArray[r.Next(0, SymbolsArray.Length - 1)].ToString();
            var num3 = SymbolsArray[r.Next(0, SymbolsArray.Length - 1)].ToString();
            var num4 = SymbolsArray[r.Next(0, SymbolsArray.Length - 1)].ToString();
            var num5 = SymbolsArray[r.Next(0, SymbolsArray.Length - 1)].ToString();

            Create(num1 + num2 + num3 + num4 + num5, path);
        }


        private static Font GetRandomFont(Random r)
        {
            Font f;
            while (true)
            {
                try
                {
                    f = new Font(FontFamily.Families[r.Next(0, 10)], 20);
                    break;
                }
                catch
                {
                    // ignored
                }
            }
            return f;
        }

        private static Brush GetRandomBrush(Random rand, bool bg)
        {
            var r = rand.Next(bg ? 0 : 100, bg ? 99 : 255);
            var g = rand.Next(bg ? 0 : 100, bg ? 99 : 255);
            var b = rand.Next(bg ? 0 : 100, bg ? 99 : 255);
            return new SolidBrush(Color.FromArgb(r, g, b));
        }
    }
}
