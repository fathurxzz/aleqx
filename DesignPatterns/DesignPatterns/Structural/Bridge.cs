// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Structural
{
    internal interface IBuldingCompany
    {
        void BuildFoundation();
        void BuildRoom();
        void BuildRoof();
        IWallCreator WallCreator { get; set; }
    }

    class BuldingCompany : IBuldingCompany
    {
        public void BuildFoundation()
        {
            Console.WriteLine("Foundation is built.{0}", Environment.NewLine);
        }
        public void BuildRoom()
        {
            WallCreator.BuildWallWithDoor();
            WallCreator.BuildWall();
            WallCreator.BuildWallWithWindow();
            WallCreator.BuildWall();
            Console.WriteLine("Room finished.{0}", Environment.NewLine);
        }
        public void BuildRoof()
        {
            Console.WriteLine("Roof is done.{0}", Environment.NewLine);
        }
        public IWallCreator WallCreator { get; set; }
    }

    internal interface IWallCreator
    {
        void BuildWallWithDoor();
        void BuildWallWithWindow();
        void BuildWall();
    }

    class ConcreteSlabWallCreator : IWallCreator
    {
        public void BuildWallWithDoor()
        {
            Console.WriteLine("Concrete slab wall with door.");
        }
        public void BuildWallWithWindow()
        {
            Console.WriteLine("Concrete slab wall with window.");
        }
        public void BuildWall()
        {
            Console.WriteLine("Concrete slab wall.");
        }
    }

    class BrickWallCreator : IWallCreator
    {
        public void BuildWallWithDoor()
        {
            Console.WriteLine("Brick wall with door.");
        }
        public void BuildWallWithWindow()
        {
            Console.WriteLine("Brick wall with window.");
        }
        public void BuildWall()
        {
            Console.WriteLine("Brick wall.");
        }
    }

    public class BridgeDemo
    {
        public static void Run()
        {
            // We have two wall creating crews - concrete blocks one and bricks one
            var brickWallCreator = new BrickWallCreator();
            var conctreteSlabWallCreator = new ConcreteSlabWallCreator();

            var buildingCompany = new BuldingCompany();
            buildingCompany.BuildFoundation();

            buildingCompany.WallCreator = conctreteSlabWallCreator;
            buildingCompany.BuildRoom();

            // Company can easily switch to another wall crew to continue building rooms
            // with another material
            buildingCompany.WallCreator = brickWallCreator;
            buildingCompany.BuildRoom();
            buildingCompany.BuildRoom();

            buildingCompany.BuildRoof();
        }
    }
}