// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioural
{
    class Soldier
    {
        public String Name;
        public int Health;
        private const int SoldierHealthPoints = 100;
        protected virtual int MaxHealthPoints { get { return SoldierHealthPoints; } }

        public Soldier(String name)
        {
            Name = name;
        }
        public void Treat()
        {
            Health = MaxHealthPoints;
            Console.WriteLine(Name);
        }
    }
    class Hero : Soldier
    {
        private const int HeroHealthPoints = 500;
        protected override int MaxHealthPoints { get { return HeroHealthPoints; } }

        public Hero(String name)
            : base(name)
        {
        }
    }


    class SoldiersIterator
    {
        private readonly Army _army;
        private bool _heroIsIterated;
        private int _currentGroup;
        private int _currentGroupSoldier;

        public SoldiersIterator(Army army)
        {
            _army = army;
            _heroIsIterated = false;
            _currentGroup = 0;
            _currentGroupSoldier = 0;
        }

        public bool HasNext()
        {
            if (!_heroIsIterated) return true;
            if (_currentGroup < _army.ArmyGroups.Count) return true;
            if (_currentGroup == _army.ArmyGroups.Count - 1)
                if (_currentGroupSoldier < _army.ArmyGroups[_currentGroup].Soldiers.Count) return true;

            return false;
        }

        public Soldier Next()
        {
            Soldier nextSoldier;
            // we still not iterated all soldiers in current group
            if (_currentGroup < _army.ArmyGroups.Count)
            {
                if (_currentGroupSoldier < _army.ArmyGroups[_currentGroup].Soldiers.Count)
                {
                    nextSoldier = _army.ArmyGroups[_currentGroup].Soldiers[_currentGroupSoldier];
                    _currentGroupSoldier++;
                }
                // moving to next group
                else
                {
                    _currentGroup++;
                    _currentGroupSoldier = 0;
                    return Next();
                }
            }
            // hero is the last who left the battlefield
            else if (!_heroIsIterated)
            {
                _heroIsIterated = true;
                return _army.ArmyHero;
            }
            else
            {
                // THROW EXCEPTION HERE
                throw new Exception("End of colletion");
                //or set all counters to 0 and start again, but not recommended
            }
            return nextSoldier;
        }
    }

    class Army
    {
        public Hero ArmyHero;
        public List<Group> ArmyGroups { get; private set; }

        public Army(Hero armyHero)
        {
            ArmyHero = armyHero;
            ArmyGroups = new List<Group>();
        }

        public void AddArmyGroup(Group group)
        {
            ArmyGroups.Add(group);
        }
    }

    class Group
    {
        public List<Soldier> Soldiers { get; private set; }

        public Group()
        {
            Soldiers = new List<Soldier>();
        }

        public void AddNewSoldier(Soldier soldier)
        {
            Soldiers.Add(soldier);
        }
    }

    class IteratorDemo
    {
        public static void Run()
        {
            var andriybuday = new Hero("Andriy Buday");
            var earthArmy = new Army(andriybuday);

            var groupA = new Group();
            for (int i = 1; i < 4; ++i)
                groupA.AddNewSoldier(new Soldier("Alpha:" + i));

            var groupB = new Group();
            for (int i = 1; i < 3; ++i)
                groupB.AddNewSoldier(new Soldier("Beta:" + i));

            var groupC = new Group();
            for (int i = 1; i < 2; ++i)
                groupC.AddNewSoldier(new Soldier("Gamma:" + i));

            earthArmy.AddArmyGroup(groupB);
            earthArmy.AddArmyGroup(groupA);
            earthArmy.AddArmyGroup(groupC);

            var iterator = new SoldiersIterator(earthArmy);
            while (iterator.HasNext())
            {
                var currSoldier = iterator.Next();
                currSoldier.Treat();
            }
        }
    }
}
