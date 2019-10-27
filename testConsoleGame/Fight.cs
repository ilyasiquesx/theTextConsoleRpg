using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testConsoleGame
{
    public class Fight
    {
        int _playersHealth;
        int _playersBasicAttack;
        int _enemiesHealth;
        int _enemiesBasicAttack;
        Random Random;
        int _playerResource;
        Monster thisMonster;
        public Fight(Player p, Monster m)
        {
            Random = new Random();
            thisMonster = m;
            _playersHealth = (p.Level * 120);
            _enemiesHealth = (m.Level * 140);
            _playersBasicAttack = (p.Level * 50);
            _enemiesBasicAttack = (p.Level * 25);
            _playerResource = 100;
        }
        void YourTurn(Player p)
        {
            Console.WriteLine("\n?????[!] - Ваша очередь атаковать, выберите способ атаки (Нажмите на соответствующую цифру на клавиатуре)\n" +
                "?????[!] - Нажатие на несоответствующую клавишу приведёт к использованию случайного заклинания!\n");
            bool turnEnded = false;
            int damageToEnemy;
            while (!turnEnded)
            {
                Console.WriteLine("\nXXXXX[?] - Доступные способности в бою:\n");
                Console.WriteLine($"XXXXX[1] - Базовая атака холодным оружием [Не требует маны] / Наносит {(int)(_playersBasicAttack * 0.8)}-{(int)(_playersBasicAttack * 1.4)} единиц урона.");
                for (int i = 0; i < p.Class.AvaliableAbilities.Count; i++)
                {
                    Ability a = p.Class.AvaliableAbilities[i];
                    Console.WriteLine($"XXXXX[{i + 2}] - " + a.Name + 
                        $" [{a.Cost} маны] / Наносит {(int)(a.Damage*0.8)}-{(int)(a.Damage * 1.7)} единиц урона.");
                }
                ConsoleKeyInfo key;
                key = Console.ReadKey();
                bool isParsed = Int32.TryParse(key.KeyChar.ToString(), out int parsedKey);
                if (key.Key == ConsoleKey.D1)
                {
                    damageToEnemy = Random.Next(_playersBasicAttack - (int)(_playersBasicAttack * 0.8), _playersBasicAttack + (int)(_playersBasicAttack * 1.4));
                    _enemiesHealth -= damageToEnemy;
                    Console.Clear();
                    Console.WriteLine($"+++++Вы атакуете вашего врага в ближнем бою, нанося {damageToEnemy} единиц урона\n");
                    turnEnded = true;
                }
                else if (isParsed && parsedKey < 6 && parsedKey > 1)
                {
                    Ability temp = p.Class.AvaliableAbilities[Int32.Parse(key.KeyChar.ToString()) - 2];
                    if(temp.Cost<=_playerResource)
                    {
                        _playerResource -= temp.Cost;
                        damageToEnemy = Random.Next((int)(temp.Damage * 0.8), (int)(temp.Damage * 1.9));
                        _enemiesHealth -= damageToEnemy;
                        Console.Clear();
                        Console.WriteLine($"+++++Вы атакуете вашего врага, используя заклинание {temp.Name} и наносите {damageToEnemy} единиц урона!\n");
                        turnEnded = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"\nXXXXX Недостаточно маны. Ваш запас маны: [{_playerResource}]");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\nXXXXX Вы ошиблись при вводе используемой способности. Попробуйте ещё раз!\n");
                }
            }
        }

        int EnemyTurn(Monster m)
        {
            int damageToHero = Random.Next(_enemiesBasicAttack - (int)(_enemiesBasicAttack * 0.7), _enemiesBasicAttack + (int)(_enemiesBasicAttack * 1.3));
            Console.WriteLine($"-----{m.Name} атакует вашего героя, нанося {damageToHero}\n");
            _playersHealth -= damageToHero;
            return damageToHero;
        }
        public void FightStart(Player p, Map map)
        {
            bool someOneIsDead = false;
            Console.WriteLine($"\n*****[?] - Ваш противник: {thisMonster.Name} {thisMonster.Level} уровня с уровнем здоровья {_enemiesHealth} единиц. Желаем удачи!");
            Console.WriteLine($"*****[?] - Небольшие сведения о Вас: {p.Class.Name} {p.Level} уровня. Ваш базовый урон в ближнем бою: {_playersBasicAttack} единиц.");
            Console.WriteLine($"*****[?] - Вы вступаете в бой имея {_playersHealth} единиц здоровья и {_playerResource} единиц ресурса.\n" +
                $"*****[?] - Уровень ресура расходуется на использование заклинаний");
            while (!someOneIsDead)
            {
                Console.WriteLine($"\n\nВАШЕ ЗДОРОВЬЕ: {_playersHealth}          ВАШ ЗАПАС МАНЫ: {_playerResource}          ЗДОРОВЬЕ ПРОТИВНИКА: {_enemiesHealth}\n");
                if (_playersHealth>0)
                {
                    YourTurn(p);
                    if(_enemiesHealth<=0)
                    {
                        someOneIsDead = true;
                        _enemiesHealth = 0;
                        p.Experience += thisMonster.Value;
                        Console.WriteLine($"Вы победели в бою с противником {thisMonster.Name}. Вы получаете {thisMonster.Value} очков опыта. Продолжайте Ваше путешествие!\n");
                        map.Monsters.Remove(thisMonster);
                    }
                }
                if(_enemiesHealth>0)
                {
                    EnemyTurn(thisMonster);
                    if(_playersHealth<=0)
                    {
                        someOneIsDead = true;
                        _playersHealth = 0;
                        thisMonster.Position = new Position((Random.Next(0, map.MaxX)), (Random.Next(0, map.MaxY)));
                        Console.WriteLine($"\n{thisMonster.Name} оделел Вас, но Вы его знатно напугали и он сбежал. Теперь он прячется в другом месте.\n");
                    }
                }
            }
        }
    }
}
