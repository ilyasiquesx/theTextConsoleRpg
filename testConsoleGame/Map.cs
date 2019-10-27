using System;
using System.Collections.Generic;
using System.Threading;

namespace testConsoleGame
{
    public class Map
    {
        Random Random;
        public event GameStateHandler PrintInfo;
        public int MaxX { get; set; }
        public int MinX { get; set; }
        public int MaxY { get; set; }
        public int MinY { get; set; }
        public int TreasuresListCap { get; set; }
        public int MonstersListCap { get; set; }
        public List<Treasure> Treasures;
        public List<Monster> Monsters;
        public Map(int X, int Y, Player p)
        {
            MapInitializationMessage();
            Random = new Random();
            MaxX = X;
            MaxY = Y;
            MinX = 0;
            MinY = 0;
            Treasures = new List<Treasure>();
            Monsters = new List<Monster>();
            TreasuresListCap = (int)((X * Y) * 0.2);
            MonstersListCap = (int)((X * Y) * 0.2);
            geneTreasure(TreasuresListCap);
            DeleteTreasureDuplicates();
            geneMonster(MonstersListCap, p);
            DeleteMonsterDuplicates();
            SyncronizeEntities();
            MapCreatedMessage();
        }

        void MapInitializationMessage()
        {
            Console.WriteLine("Подготовка карты...\n");
        }
        void MapCreatedMessage()
        {
            Console.WriteLine("Карта успешно создана. Управляйте своим героем стрелками. Найдите как можно больше интересных сражений и сокровищ!\n");
        }
        public void TreasureRefresh()
        {
            PrintInfo?.Invoke(this, new GameEventArgs("!!!ЕСТЬ НОВОСТИ!!! Для подробностей нажми на любую клавишу...", ConsoleColor.Red));
            Console.ReadKey();
            Console.Clear();
            geneTreasure(TreasuresListCap);
            DeleteTreasureDuplicates();
            SyncronizeEntities();
            PrintInfo?.Invoke(this, new GameEventArgs("[?????] - Внимание!!!\n" +
                "[?????] - Ты собрал почти все сокровища на карте.\n" +
                "[?????] - Для того, чтобы ты не скучал карта обновилась и были расставлены новые сокровища.\n", ConsoleColor.Red));
        }
        public void MonsterRefresh(Player p)
        {
            PrintInfo?.Invoke(this, new GameEventArgs("!!!ЕСТЬ НОВОСТИ!!! Для подробностей нажми на любую клавишу...", ConsoleColor.Red));
            Console.ReadKey();
            Console.Clear();
            geneMonster(TreasuresListCap, p);
            DeleteMonsterDuplicates();
            SyncronizeEntities();
            PrintInfo?.Invoke(this, new GameEventArgs("[?????] - Внимание!!!\n" +
                "[?????] - Ты так сильно хочешь захватить этот мир, что убил большинство монстров.\n" +
                "[?????] - Не так все просто, как хотелось бы. Новые монстры явились тебе противостоять.\n", ConsoleColor.Red));
        }

        void geneTreasure(int count)
        {
            int sleepValue = 15;
            Console.WriteLine($"Создание сокровищ...");
            for (int i = Treasures.Count; i < count; i++)
            {
                Thread.Sleep(sleepValue);
                Treasures.Add(new Treasure(new Position(Random.Next(1, MaxX), Random.Next(1, MaxY))));
            }
            
        }
        void geneMonster(int count, Player p)
        {
            int sleepValue = 15;
            Console.WriteLine($"Создание монстров...");
            for (int i = Monsters.Count; i<count; i++)
            {
                Thread.Sleep(sleepValue);
                Monsters.Add(new Monster(new Position(Random.Next(1, MaxX), Random.Next(1, MaxY)), p));
            }
        }
        public void DeleteTreasureDuplicates() // Алгоритм удаления дубликатов 
        {
            Console.WriteLine("Удаление дубликатов сокровищ...");
            for (int i =0; i<Treasures.Count; i++)
            {
                for(int j=0; j<Treasures.Count; j++)
                {
                    if(Treasures[i].Position.Equals(Treasures[j].Position) && i!=j)
                    {
                        Treasures[j].Position = new Position(MaxX*20, MaxY*20); //Выносим дубликаты за пределы карты в одну точку
                    }
                }
            }
            Treasures.RemoveAll( t => t.Position.X == MaxX * 20); // Удаляем дубликаты
            Console.WriteLine("Дубликаты удалены.\n");
        }
        public void DeleteMonsterDuplicates() // Алгоритм удаления дубликатов 
        {
            Console.WriteLine("Удаление дубликатов монстров...");
            for (int i = 0; i < Monsters.Count; i++)
            {
                for (int j = 0; j < Monsters.Count; j++)
                {
                    if (Monsters[i].Position.Equals(Monsters[j].Position) && i != j)
                    {
                        Monsters[j].Position = new Position(MaxX * 20, MaxY * 20); //Выносим дубликаты за пределы карты в одну точку
                    }
                }
            }
            Monsters.RemoveAll(t => t.Position.X == MaxX * 20); // Удаляем дубликаты
            Console.WriteLine("Дубликаты удалены.\n");
        }

        public void SyncronizeEntities()
        {
            Console.WriteLine("Синхронизация сокровищ и монстров.");
            if (Monsters.Count > Treasures.Count)
            {
                for (int i = 0; i < Treasures.Count; i++)
                {
                    for (int j = 0; j < Monsters.Count; j++)
                    {
                        if (Treasures[i].Position.Equals(Monsters[j].Position))
                        {
                            Monsters[j].Position = new Position(MaxX * 20, MaxY * 20); //Выносим дубликаты за пределы карты в одну точку
                        }
                    }
                }
                Monsters.RemoveAll(t => t.Position.X == MaxX * 20);
            }
            else
            {
                for (int i = 0; i < Monsters.Count; i++)
                {
                    for (int j = 0; j < Treasures.Count; j++)
                    {
                        if (Monsters[i].Position.Equals(Treasures[j].Position))
                        {
                            Treasures[j].Position = new Position(MaxX * 20, MaxY * 20); //Выносим дубликаты за пределы карты в одну точку
                        }
                    }
                }
                Treasures.RemoveAll(t => t.Position.X == MaxX * 20);
            }
            Console.WriteLine($"Синхронизация завершена. На карте расположены монстры в количестве: {Monsters.Count} и сокровища в количестве: {Treasures.Count}.\n");
        }

        public void NavigationHelp()
        {
            int randomTreasureIndex = Random.Next(0, Treasures.Count);
            if(Treasures.Count!=0)
            {
                PrintInfo?.Invoke(this, new GameEventArgs($"\nНекто осведомленный говорит: \nМне известно, что одно из сокровищ под названием: \"{Treasures[randomTreasureIndex].Name}\" " +
                $"находится по координатам: X={Treasures[randomTreasureIndex].Position.X}, Y={Treasures[randomTreasureIndex].Position.Y}. " +
                $"\nКто знает, что ты сможешь встретить по пути к нему...\n", ConsoleColor.Green));
            }
        }
        
        public void MonsterFound(Player p)
        {
            for(int i =0; i<Monsters.Count; i++)
            {
                bool fightAccepted = false;
                if (p.Position.Equals(Monsters[i].Position))
                {
                    while(!fightAccepted)
                    {
                        Console.Clear();
                        PrintInfo?.Invoke(this, new GameEventArgs($"Вы встретили врага! Перед вами {Monsters[i].Name} {Monsters[i].Level} уровня. Хотите ли вы начать сражение? [Y/N]",
                            ConsoleColor.Yellow)) ;
                        ConsoleKeyInfo k = Console.ReadKey();
                        switch (k.Key)
                        {
                            case ConsoleKey.Y:
                                {
                                    fightAccepted = true;
                                    Console.Clear();
                                    Fight fight = new Fight(p, Monsters[i]);
                                    fight.FightStart(p, this);
                                    break;
                                }
                            case ConsoleKey.N:
                                {

                                    fightAccepted = true;
                                    Console.Clear();
                                    PrintInfo?.Invoke(this, new GameEventArgs("Вы бежали с поля боя.", ConsoleColor.DarkMagenta));
                                    Monsters.Remove(Monsters[i]);
                                    break;
                                }
                            default:
                                {
                                    Console.Clear();
                                    PrintInfo?.Invoke(this, new GameEventArgs("Попробуйте ответить на вопрос снова. Нажмите любую клавишу...", ConsoleColor.Green));
                                    Console.ReadKey();
                                    break;
                                }
                        }
                    }
                }
            }
        }

        public void Founded(Player p)
        {
            for (int i = 0; i < Treasures.Count; i++)
            {
                bool isOpen = false;
                if (p.Position.Equals(Treasures[i].Position))
                {
                    while (!isOpen)
                    {
                        Console.Clear();
                        PrintInfo?.Invoke(this, new GameEventArgs("Вы нашли сокровище! Если вы хотите его открыть нажмите Y, если же хотите пропустить его нажмите N [Y/N]", 
                            ConsoleColor.Yellow));
                        ConsoleKeyInfo k = Console.ReadKey();
                        switch (k.Key)
                        {
                            case ConsoleKey.Y:
                                {
                                    isOpen = true;
                                    Console.Clear();
                                    p.Experience += Treasures[i].Value;
                                    PrintInfo?.Invoke(this, new GameEventArgs($"Вы открыли \"{Treasures[i].Name}\" и получили {Treasures[i].Value} очков опыта." +
                                        $"\n\nПродолжайте своё приключение\n", ConsoleColor.Cyan));
                                    Treasures.Remove(Treasures[i]);
                                    break;
                                }
                            case ConsoleKey.N:
                                {
                                    isOpen = true;
                                    Console.Clear();
                                    PrintInfo?.Invoke(this, new GameEventArgs("Вы упустили сокровище, так и не узнав, что внутри.", ConsoleColor.DarkMagenta));
                                    Treasures.Remove(Treasures[i]);
                                    break;
                                }
                            default:
                                {
                                    Console.Clear();
                                    PrintInfo?.Invoke(this, new GameEventArgs("Попробуйте ответить на вопрос снова. Нажмите любую клавишу...", ConsoleColor.Green));
                                    Console.ReadKey();
                                    break;
                                }
                        }

                    }
                }
            }

        }
        
    }
}
