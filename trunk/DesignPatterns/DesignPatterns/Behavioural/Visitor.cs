// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioural
{
    interface IVisitor
    {
        void Visit(OfficeBuilding building);
        void Visit(Floor floor);
        void Visit(Room room);
    }

    class ElectricitySystemValidator : IVisitor
    {
        public void Visit(OfficeBuilding building)
        {
            var electricityState = (building.ElectricitySystemId > 1000) ? "Good" : "Bad";
            Console.WriteLine(string.Format("Main electric shield in building {0} is in {1} state.", building.BuildingName, electricityState));
        }
        public void Visit(Floor floor)
        {
            Console.WriteLine(string.Format("Diagnosting electricity on floor {0}.", floor.FloorNumber));
        }
        public void Visit(Room room)
        {
            Console.WriteLine(string.Format("Diagnosting electricity in room {0}.", room.RoomNumber));
        }
    }

    class PlumbingSystemValidator : IVisitor
    {
        public void Visit(OfficeBuilding building)
        {
            var plumbingState = (building.Age < 30) ? "Good" : "Bad";
            var buildingAge = (building.Age < 30) ? "New" : "Old";
            Console.WriteLine(string.Format("Plumbing state of building {0} probably is in {1} condition, since builing is {2}.", building.BuildingName, plumbingState, buildingAge));
        }
        public void Visit(Floor floor)
        {
            Console.WriteLine(string.Format("Diagnosting plumbing on floor {0}.", floor.FloorNumber));
        }
        public void Visit(Room room)
        {
            // we do nothing, since rooms do not have separated plumbing systems
        }
    }

    interface IElement
    {
        void Accept(IVisitor visitor);
    }

    class OfficeBuilding : IElement
    {
        private readonly IList<Floor> _floors = new List<Floor>();
        public string BuildingName { get; private set; }
        public int Age { get; set; }
        public int ElectricitySystemId { get; set; }

        public IEnumerable<Floor> Floors { get { return _floors; } }

        public OfficeBuilding(string buildingName, int age, int electricitySystemId)
        {
            BuildingName = buildingName;
            Age = age;
            ElectricitySystemId = electricitySystemId;
        }
        public void AddFloor(Floor floor)
        {
            _floors.Add(floor);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var floor in Floors)
            {
                floor.Accept(visitor);
            }
        }
    }

    class Floor : IElement
    {
        private readonly IList<Room> _rooms = new List<Room>();
        public int FloorNumber { get; private set; }
        public IEnumerable<Room> Rooms { get { return _rooms; } }

        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
        }
        public void AddRoom(Room room)
        {
            _rooms.Add(room);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var room in Rooms)
            {
                room.Accept(visitor);
            }
        }
    }

    class Room : IElement
    {
        public int RoomNumber { get; private set; }

        public Room(int roomNumber)
        {
            RoomNumber = roomNumber;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class VisitorDemo
    {
        public static void Run()
        {
            var floor1 = new Floor(1);
            floor1.AddRoom(new Room(100));
            floor1.AddRoom(new Room(101));
            floor1.AddRoom(new Room(102));
            var floor2 = new Floor(2);
            floor2.AddRoom(new Room(200));
            floor2.AddRoom(new Room(201));
            floor2.AddRoom(new Room(202));
            var myFirmOffice = new OfficeBuilding("[Design Patterns Center]", 25, 990);
            myFirmOffice.AddFloor(floor1);
            myFirmOffice.AddFloor(floor2);

            var electrician = new ElectricitySystemValidator();
            myFirmOffice.Accept(electrician);

            var plumber = new PlumbingSystemValidator();
            myFirmOffice.Accept(plumber);
        }
    }
}