// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Behavioural
{
    public static class MediatorDemo
    {
        public static void Run()
        {
            var human = new Brain();

            string line;
            AskForInput();
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                switch (line)
                {
                    case "Ear":
                        human.Ear.HearSomething();
                        break;
                    case "Eye":
                        human.Eye.SeeSomething();
                        break;
                    case "Hand":
                        human.Hand.FeelSomething();
                        break;
                }
                AskForInput();
            }
        }

        private static void AskForInput()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter body part ('Ear','Eye','Hand' or empty to exit):");
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }

    // Mediator
    class Brain
    {
        public Brain()
        {
            CreateBodyParts();
        }

        private void CreateBodyParts()
        {
            Ear = new Ear(this);
            Eye = new Eye(this);
            Face = new Face(this);
            Hand = new Hand(this);
            Leg = new Leg(this);
        }

        public Ear Ear { get; private set; }
        public Eye Eye { get; private set; }
        public Face Face { get; private set; }
        public Hand Hand { get; private set; }
        public Leg Leg { get; private set; }

        public void SomethingHappenedToBodyPart(BodyPart bodyPart)
        {
            if (bodyPart is Ear)
            {
                string heardSounds = ((Ear)bodyPart).GetSounds();

                if (heardSounds.Contains("stupid"))
                {
                    // attacking offender
                    Leg.StepForward();
                    Hand.HitPersonNearYou();
                    Leg.Kick();
                }
                else if (heardSounds.Contains("cool"))
                {
                    Face.Smile();
                }
            }
            else if (bodyPart is Eye)
            {
                // brain can analyze what you see and
                // can react appropriately using different body parts
            }
            else if (bodyPart is Hand)
            {
                var hand = (Hand)bodyPart;

                bool hurtingFeeling = hand.DoesItHurt();
                if (hurtingFeeling)
                {
                    Leg.StepBack();
                }

                bool itIsNice = hand.IsItNice();
                if (itIsNice)
                {
                    Leg.StepForward();
                    Hand.Embrace();
                }
            }
            else if (bodyPart is Leg)
            {
                // leg can also feel something if you would like it to
            }
        }
    }

    // Collegue
    class BodyPart
    {
        private readonly Brain _brain;

        public BodyPart(Brain brain)
        {
            _brain = brain;
        }

        public void Changed()
        {
            _brain.SomethingHappenedToBodyPart(this);
        }
    }

    class Ear : BodyPart
    {
        private string _sounds = string.Empty;

        public Ear(Brain brain)
            : base(brain)
        {
        }
        public void HearSomething()
        {
            Console.WriteLine("Enter what you hear:");
            _sounds = Console.ReadLine();

            Changed();
        }
        public string GetSounds()
        {
            return _sounds;
        }
    }

    class Face : BodyPart
    {
        public Face(Brain brain)
            : base(brain)
        {
        }
        public void Smile()
        {
            Console.WriteLine("FACE: Smiling...");
        }
    }

    class Eye : BodyPart
    {
        private string _thingsAround = string.Empty;
        public Eye(Brain brain)
            : base(brain)
        {
        }

        public void SeeSomething()
        {
            Console.WriteLine("Enter what you see:");
            _thingsAround = Console.ReadLine();

            Changed();
        }
    }

    class Hand : BodyPart
    {
        private bool _isSoft;
        private bool _isHurting;

        public Hand(Brain brain)
            : base(brain)
        {
        }
        public void FeelSomething()
        {
            Console.WriteLine("What you feel is soft? (Yes/No)");
            var answer = Console.ReadLine();
            if (answer != null && answer[0] == 'Y')
            {
                _isSoft = true;
            }
            Console.WriteLine("What you feel is hurting? (Yes/No)");
            answer = Console.ReadLine();
            if (answer != null && answer[0] == 'Y')
            {
                _isHurting = true;
            }

            Changed();
        }
        public void HitPersonNearYou()
        {
            Console.WriteLine("HAND: Just hit offender...");
        }
        public void Embrace()
        {
            Console.WriteLine("HAND: Embracing what is in front of you...");
        }
        public bool DoesItHurt()
        {
            return _isHurting;
        }
        public bool IsItNice()
        {
            return !_isHurting && _isSoft;
        }
    }

    class Leg : BodyPart
    {
        public Leg(Brain brain)
            : base(brain)
        {
        }
        public void Kick()
        {
            Console.WriteLine("LEG: Just kicked offender in front of you..");
        }
        public void StepBack()
        {
            Console.WriteLine("LEG: Stepping back...");
        }
        public void StepForward()
        {
            Console.WriteLine("LEG: Stepping forward...");
        }
    }
}