using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testConsoleGame
{
    public delegate void GameStateHandler(object sender, GameEventArgs e);
    class Game
    {
        public event GameStateHandler InputInfo;
        string _gameIntroduce = "Привет. Это пошаговая текстовая игра в стиле РПГ.";
        string _gameInstructions = "\nЛаконично расскажу тебе о правилах: Ты - герой. Принадлежишь определенному классу, который выберешь сам. Тут их 4.\n" +
            "Твоё героическое тело передвигается по этому миру с помощью стрелок на клавиатуре.\n" +
            "(Клавиши: Вверх, Вниз - перемещение по оси Y в большую и меньшую сторону соответственно;\n" +
            "Влево, Вправо - перемемещение по оси X в меньшую и большую сторону соотвественно)\n" +
            "Надеюсь, с управлением у тебя не возникнет проблем и ты быстро разберешься.\n" +
            "На карте расположены сокровища и монстры в случайном порядке. \n" +
            "Как только ты пересечешься с ними, ты сразу об этом узнаешь.\n" +
            "У меня есть знакомый, который согласился  подсказывать тебе путь, если ты не сможешь найти что-нибудь интересное.\n" +
            "Уверен, что Вы подружитесь.\n";
        public void Run()
        {
            GameIntroduce();
            Player player = new Player();
            player.PrintInfo += ShowMessage;
            Map Map = new Map(20, 20, player);
            Map.PrintInfo += ShowMessage;
            int n = 0;
            while (true)
            {
                if (n == 15)
                {
                    n = 0;
                    Map.NavigationHelp();
                }
                if(Map.Treasures.Count<=Map.TreasuresListCap*0.25)
                {
                    Map.TreasureRefresh();
                }
                if(Map.Monsters.Count<=Map.MonstersListCap*0.25)
                {
                    Map.MonsterRefresh(player);
                }
                player.Moving(Map);
                Map.Founded(player) ;
                Map.MonsterFound(player);
                player.Progress();
                n++;
            }
        }

        void GameIntroduce()
        {
            InputInfo.Invoke(this, new GameEventArgs(_gameIntroduce, ConsoleColor.DarkGreen));
            InputInfo.Invoke(this, new GameEventArgs(_gameInstructions, ConsoleColor.DarkGreen));
        }

        static void ShowMessage(object sender, GameEventArgs e)
        {
            Console.ForegroundColor = e.MsgColor;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}
