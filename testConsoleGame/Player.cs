using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testConsoleGame
{
    public class GameEventArgs
    {
        public string Message { get; set; }
        public ConsoleColor MsgColor { get; set; }
        public GameEventArgs(string m, ConsoleColor c)
        {
            Message = m;
            MsgColor = c;
        }
    }

    public class Player
    {
        public HeroClass Class;

        public event GameStateHandler PrintInfo;
        public Position Position { get; set; }
        public int Level { get; set; }

        public int Experience { get; set; }

        public double ExperienceCap { get; set; }

        int SetClass()
        {

            Console.WriteLine(">>>В игре представленые следующие классы:\n");
            ShowClasses();
            Console.WriteLine("<<<Нажатием на соответствующую клавишу выберете тот, за который хотите начать игру.");
            ConsoleKeyInfo k = Console.ReadKey();
            bool parsed = Int32.TryParse(k.KeyChar.ToString(), out int parsedValue);
            while (!parsed || parsedValue > HeroClass.NameOfClass.Length || parsedValue <= 0)
            {
                Console.Clear();
                Console.WriteLine("Ошибка при выборе класса. Указан несуществующий класс. Попробуйте снова.\n");
                ShowClasses();
                k = Console.ReadKey();
                parsed = Int32.TryParse(k.KeyChar.ToString(), out parsedValue);
            }
            return parsedValue-1;
        }

        void ShowClassInfo()
        {
            Console.Clear();
            Console.WriteLine($"Вы выбрали класс: {Class.Name}. Небольшая справка о Ваших способностях.\n");
            Console.WriteLine($"В вашем арсенале следующий набор умений:\n");
            for (int i= 0; i < Class.AvaliableAbilities.Count; i++)
            {
                Console.WriteLine($"[>>] [{Class.AvaliableAbilities[i].Name}] ");
            }
            Console.WriteLine();
        }

        void ShowClasses()
        {
            for(int i =0; i<HeroClass.NameOfClass.Length; i++)
            {
                Console.WriteLine($"          [{i+1}] - {HeroClass.NameOfClass[i]}");
            }
            Console.WriteLine();
        }
        public Player()
        {
            Level = 1;
            Experience = 0;
            ExperienceCap = 300;
            Position = new Position(0, 0);
            Class = new HeroClass(SetClass());
            ShowClassInfo();

        }
        string movingError = "КОМПАС: Вы пытаетесь покинуть пределы карты. Двигайтесь в другом направлении.";

        public void Moving(Map m)
        {

            PrintInfo?.Invoke(this, new GameEventArgs($"КОМПАС: Вы находитесь в позиции х = {Position.X}, y = {Position.Y}.", ConsoleColor.White));
            ConsoleKeyInfo key;
            key = Console.ReadKey();
            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    {
                        if (Position.Y < m.MaxY)
                        {
                            Position.Y++;
                            break;
                        }
                        else
                        {
                            PrintInfo?.Invoke(this, new GameEventArgs(movingError, ConsoleColor.Red));
                            break;
                        }
                    }
                case ConsoleKey.DownArrow:
                    {
                        if (Position.Y > m.MinY)
                        {
                            Position.Y--;
                            break;
                        }
                        else
                        {
                            PrintInfo?.Invoke(this, new GameEventArgs(movingError, ConsoleColor.Red));
                            break;
                        }
                        
                    }
                case ConsoleKey.LeftArrow:
                    {
                        if(Position.X>m.MinX)
                        {
                            Position.X--;
                            break;
                        }
                        else
                        {
                            PrintInfo?.Invoke(this, new GameEventArgs(movingError, ConsoleColor.Red));
                            break;
                        }
                    }
                case ConsoleKey.RightArrow:
                    {
                        if(Position.X<m.MaxX)
                        {
                            Position.X++;
                            break;
                        }
                        else
                        {
                            PrintInfo?.Invoke(this, new GameEventArgs(movingError, ConsoleColor.Red));
                            break;
                        }
                    }
                default:
                    {
                        Console.Clear();
                        PrintInfo?.Invoke(this, new GameEventArgs("Упрвляйте собой стрелками на клавиатуре!", ConsoleColor.Red));
                        break;
                    }
            }
        }

        public void Progress()
        {
            if(Experience>=ExperienceCap)
            {
                Level++;
                Experience -= (int)ExperienceCap;
                ExperienceCap = (int)(ExperienceCap*1.2);
                PrintInfo?.Invoke(this, new GameEventArgs($"Вы достигли уровня: {Level}, Ваш опыт: {Experience}, Вам необходимо {ExperienceCap} опыта для " +
                    $"достижения следующего уровня\n", ConsoleColor.Yellow));
            }
            //if((Level+1)%2 == 0&&Level!=2)
            //{
            //    int unusedTalnt = 1;
            //    while (unusedTalnt!=0)
            //    {
            //        Console.Clear();
            //        Console.WriteLine("Вам предложено повысить уровень одной из способностей!");
            //    }
            //}
        }
        public void ShowInfo()
        {
            PrintInfo?.Invoke(this, new GameEventArgs($"Слушай, я думаю, что тебе зачем-то понадобилась эта информация.\n" +
                $"Знай, что сейчас твой уровень равен: {Level}, до следующего уровня тебе нехватает {ExperienceCap - Experience} очков опыта.", ConsoleColor.DarkYellow));
        }


    }
}
