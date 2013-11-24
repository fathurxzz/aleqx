// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Creational
{
    public class CalendarPrototype
    {
        public virtual CalendarPrototype Clone()
        {
            var copyOfPrototype = (CalendarPrototype)this.MemberwiseClone();
            return copyOfPrototype;
        }
    }

    public class CalendarEvent : CalendarPrototype
    {
        public Attendee[] Attendees { get; set; }
        public Priority Priority { get; set; }
        public DateTime StartDateAndTime { get; set; }

        public override CalendarPrototype Clone()
        {
            var copy = (CalendarEvent)base.Clone();

            // this allows us have another list, but same attendees there
            var copiedAttendees = (Attendee[])Attendees.Clone();
            copy.Attendees = copiedAttendees;

            // we also would like to copy priority
            copy.Priority = (Priority)Priority.Clone();

            return copy;
        }
    }

    public class Priority : ICloneable
    {
        private int _priority;

        private Priority(int priority)
        {
            _priority = priority;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static Priority High()
        {
            return new Priority(1);
        }

        public static Priority Low()
        {
            return new Priority(-1);
        }

        public void SetPriorityValue(int priority)
        {
            _priority = priority;
        }

        public bool IsHigh()
        {
            return _priority > 0;
        }
    }

    public class Attendee
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }

    public class PrototypeDemo
    {
        public static CalendarEvent GetExistingEvent()
        {
            var beerParty = new CalendarEvent();
            var friends = new Attendee[1];
            var andriy = new Attendee { FirstName = "Andriy", LastName = "Buday" };
            friends[0] = andriy;
            beerParty.Attendees = friends;
            beerParty.StartDateAndTime = new DateTime(2010, 7, 23, 19, 0, 0);
            beerParty.Priority = Priority.High();

            return beerParty;
        }

        public static void Run()
        {
            var beerParty = GetExistingEvent();
            var nextFridayEvent = (CalendarEvent)beerParty.Clone();
            nextFridayEvent.StartDateAndTime = new DateTime(2010, 7, 30, 19, 0, 0);

            // про цей код побалакаємо трішки нижче
            nextFridayEvent.Attendees[0].EmailAddress = "andriybuday@liamg.com";
            nextFridayEvent.Priority.SetPriorityValue(0);

            if (beerParty.Attendees != nextFridayEvent.Attendees)
            {
                Console.WriteLine("GOOD: Each event has own list of attendees.");
            }
            if (beerParty.Attendees[0].EmailAddress == nextFridayEvent.Attendees[0].EmailAddress)
            {
                //В цьому випадку добре мати поверхневу копію кожного із учасників
                //таким чином моя адреса, ім'я і персональні дані залишаються тими ж
                Console.WriteLine("GOOD: If I updated my e-mail address it will be updated in all events.");
            }
            if (beerParty.Priority.IsHigh() != nextFridayEvent.Priority.IsHigh())
            {
                Console.WriteLine("GOOD: Each event should have own priority object, fully-copied.");
            }
        }
    }

}