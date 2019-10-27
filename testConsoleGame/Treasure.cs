using System;


namespace testConsoleGame
{
    public class Treasure 
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Position Position { get; set; }
        Random Random { get; set; }
        string[] names = {"Сундук", "Подозрительный мешок", "Сумка с драгоценностями", "Королевские сбережения", "Гигантский ларец"};

        public  Treasure (Position p)
        {
            Random = new Random();
            Name = names[Random.Next(0, names.Length)];
            Value = Random.Next(150, 350);
            Position = p;
        }
    }
}
