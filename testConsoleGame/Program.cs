using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testConsoleGame
{

    class Program
    {
        
        static void Main(string[] args)
        {
            Game ng = new Game(); // создание игры
            ng.InputInfo += ShowMessage;
            ng.Run();
            Console.Read();

        }
        
        static void ShowMessage(object sender, GameEventArgs e)
        {
            Console.ForegroundColor = e.MsgColor;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}
