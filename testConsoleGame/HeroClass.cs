using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testConsoleGame
{
    public class HeroClass
    {
        public static string[] NameOfClass = { "Воин", "Маг", "Охотник", "Разбойник" };
        int _abilityQuantity = 4;
        public string Name { get;set; }
        public List<Ability> AvaliableAbilities;
        public HeroClass(int choise)
        {
            Name = NameOfClass[choise];
            AvaliableAbilities = new List<Ability>();
            SetAbil(choise);
        }

        void SetAbil(int choise)
        {
            switch(choise)
            {
                case 0:
                    {
                        for (int i = 0; i < _abilityQuantity; i++)
                        {
                            AvaliableAbilities.Add(new Ability(i, choise));
                        }
                        break;
                    }
                case 1:
                    {
                        for(int i =0; i<_abilityQuantity; i++)
                        {
                            AvaliableAbilities.Add(new Ability(i, choise));
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < _abilityQuantity; i++)
                        {
                            AvaliableAbilities.Add(new Ability(i, choise));
                        }
                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < _abilityQuantity; i++)
                        {
                            AvaliableAbilities.Add(new Ability(i, choise));
                        }
                        break;
                    }
            }
        }

        public void ShowAvalAb()
        {
            foreach(Ability a in AvaliableAbilities)
            {
                Console.WriteLine(a.Name);
            }
        }
    }
}
