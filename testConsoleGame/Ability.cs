using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testConsoleGame
{
    public class Ability
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Damage { get { return _basedDamage * Level; } set { Damage = _basedDamage * Level; } }
        public int Cost { get; set; }

        int _basedDamage;

        string[] mageAbilities = { "Огненный шар", "Ледяная стрела", "Чародейская вспышка", "Огненная глыба" };
        int[] basedMageDamage = { 95, 80, 190, 240 };

        string[] warriorAbilities = { "Смертельный удар", "Превосходство", "Удар героя", "Мощный удар" };
        int[] basedWarriorDamage = { 110, 80, 120, 160 };

        string[] hunterAbilities = { "Укус гадюки", "Оглушающий выстрел", "Чародейский выстрел", "Атака питомца" };
        int[] basedHunterDamage = { 135, 160, 170, 210 };

        string[] rogueAbilities = { "Удар в спину", "Потрошение", "Внезапный удар", "Расправа" };
        int[] basedRogueDamage = { 85, 165, 140, 180 };


        int[] basicCost = { 30, 25, 40, 60 };

        public Ability(int index, int classId)
        {
            switch(classId)
            {
                case 0:
                    {
                        Name = warriorAbilities[index];
                        _basedDamage = basedWarriorDamage[index];
                        Level = 1;
                        Cost = basicCost[index];
                        break;
                    }
                case 1:
                    {
                        Name = mageAbilities[index];
                        _basedDamage = basedMageDamage[index];
                        Level = 1;
                        Cost = basicCost[index];
                        break;
                    }
                case 2:
                    {
                        Name = hunterAbilities[index];
                        _basedDamage = basedHunterDamage[index];
                        Level = 1;
                        Cost = basicCost[index];
                        break;
                    }
                case 3:
                    {
                        Name = rogueAbilities[index];
                        _basedDamage = basedRogueDamage[index];
                        Level = 1;
                        Cost = basicCost[index];
                        break;
                    }
            }
            
        }
        
    }
}
