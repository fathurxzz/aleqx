// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioural
{
    class CommandDemo
    {
        public static void Run()
        {
            var customer = new Customer();
            // із певних міркуваня, босс завжди знає, що грошей стає тільки
            // oна бригаду Z
            var team = new Team("Z");
            // також отримав список вимог, що треба буде передати бригаді
            var requirements = new List<Requirement>() { new Requirement("Cool web site"), new Requirement("Ability to book beer on site") };
            // yви повинні бути готові бути викликаним замовником
            ICommand commandX = new YouAsProjectManagerCommand(team, requirements);

            customer.AddCommand(commandX);

            // в компанії також є програміст-герой, що кодує на швидкості світла
            var heroDeveloper = new HeroDeveloper();
            // босс вирішив віддати йому проект A
            ICommand commandA = new HeroDeveloperCommand(heroDeveloper, "A");

            customer.AddCommand(commandA);

            // як тільки замовник підписує контракт із вашим боссом
            // ваша бригада і програміст-герой готові виконати все що треба
            customer.SignContractWithBoss();
        }
    }

    class Customer
    {
        protected List<ICommand> Commands { get; set; }
        public Customer()
        {
            Commands = new List<ICommand>();
        }
        public void AddCommand(ICommand command)
        {
            Commands.Add(command);
        }
        public void SignContractWithBoss()
        {
            foreach (var command in Commands)
            {
                command.Execute();
            }
        }
    }


    interface ICommand
    {
        void Execute();
    }
    class YouAsProjectManagerCommand : ICommand
    {
        public YouAsProjectManagerCommand(Team team, List<Requirement> requirements)
        {
            Team = team;
            Requirements = requirements;
        }
        public void Execute()
        {
            // реалізація делегує роботу до потрібного отримувача
            Team.CompleteProject(Requirements);
        }
        protected Team Team { get; set; }
        protected List<Requirement> Requirements { get; set; }
    }
    class HeroDeveloperCommand : ICommand
    {
        public HeroDeveloperCommand(HeroDeveloper heroDeveloper, string projectName)
        {
            HeroDeveloper = heroDeveloper;
            ProjectName = projectName;
        }
        public void Execute()
        {
            // реалізація делегує роботу до потрібного отримувача
            HeroDeveloper.DoAllHardWork(ProjectName);
        }
        protected HeroDeveloper HeroDeveloper { get; set; }
        public string ProjectName { get; set; }
    }

    class HeroDeveloper
    {
        public void DoAllHardWork(string projectName)
        {
            Console.WriteLine("Hero developer completed project ({0}) without requirements in manner of couple of hours!", projectName);
        }
    }

    class Requirement
    {
        public string UserStory { get; private set; }

        public Requirement(string userStory)
        {
            UserStory = userStory;
        }
    }

    class Team
    {
        public string Name { get; private set; }

        public Team(string name)
        {
            Name = name;
        }

        public void CompleteProject(List<Requirement> requirements)
        {
            AnalyzeRequirements(requirements);
            foreach (var requirement in requirements)
            {
                WorkOnRequirement(requirement);
            }
        }

        private void WorkOnRequirement(Requirement requirement)
        {
            Console.WriteLine("User Story ({0}) has been completed", requirement.UserStory);
        }

        private void AnalyzeRequirements(List<Requirement> requirements)
        {
            foreach (var requirement in requirements)
            {
                if (string.IsNullOrEmpty(requirement.UserStory))
                {
                    throw new ArgumentException("not enought information on some of the requirements...");
                }
            }
        }
    }
}