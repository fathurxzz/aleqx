// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Structural
{
    public class ProxyDemo
    {
        public static void Run()
        {
            int opNum = 0;
            try
            {
                var proxy = new RobotBombDefuserProxy(41);
                proxy.WalkStraightForward(100);
                opNum++;
                proxy.TurnRight();
                opNum++;
                proxy.WalkStraightForward(5);
                opNum++;
                proxy.DefuseBomb();
                opNum++;

                Console.WriteLine();
            }
            catch (BadConnectionException e)
            {
                Console.WriteLine("Exception has been caught with message: ({0}). Decided to have human operate robot there.", e.Message);

                PlanB(opNum);
            }
        }

        private static void PlanB(int nextOperationNum)
        {
            RobotBombDefuser humanOperatingRobotDirectly = new RobotBombDefuser();

            if (nextOperationNum == 0)
            {
                humanOperatingRobotDirectly.WalkStraightForward(100);
                nextOperationNum++;
            }
            if (nextOperationNum == 1)
            {
                humanOperatingRobotDirectly.TurnRight();
                nextOperationNum++;
            }
            if (nextOperationNum == 2)
            {
                humanOperatingRobotDirectly.WalkStraightForward(5);
                nextOperationNum++;
            }
            if (nextOperationNum == 3)
            {
                humanOperatingRobotDirectly.DefuseBomb();
            }
        }
    }

    public class RobotBombDefuser
    {
        private Random _random = new Random();
        private int _robotConfiguredWavelength = 41;
        private bool _isConnected = false;

        public void ConnectWireless(int communicationWaveLength)
        {
            if (communicationWaveLength == _robotConfiguredWavelength)
            {
                _isConnected = IsConnectedImmitatingConnectivitiyIssues();
            }
        }
        public bool IsConnected()
        {
            _isConnected = IsConnectedImmitatingConnectivitiyIssues();
            return _isConnected;
        }
        private bool IsConnectedImmitatingConnectivitiyIssues()
        {
            return _random.Next(0, 10) < 4; // immitates 40% good connection, aka. very bad
        }

        public virtual void WalkStraightForward(int steps)
        {
            Console.WriteLine("Did {0} steps forward...", steps);
        }

        public virtual void TurnRight()
        {
            Console.WriteLine("Turned right...");
        }

        public virtual void TurnLeft()
        {
            Console.WriteLine("Turned left...");
        }

        public virtual void DefuseBomb()
        {
            Console.WriteLine("Cut red or green or blue wire...");
        }
    }

    public class RobotBombDefuserProxy : RobotBombDefuser
    {
        private RobotBombDefuser _robotBombDefuser;
        private int _communicationWaveLength;
        private int _connectionAttempts = 3;

        public RobotBombDefuserProxy(int communicationWaveLength)
        {
            _robotBombDefuser = new RobotBombDefuser();
            _communicationWaveLength = communicationWaveLength;
        }

        public override void WalkStraightForward(int steps)
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.WalkStraightForward(steps);
        }

        public override void TurnRight()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.TurnRight();
        }

        public override void TurnLeft()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.TurnLeft();
        }

        public override void DefuseBomb()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.DefuseBomb();
        }

        private void EnsureConnectedWithRobot()
        {
            if (_robotBombDefuser == null)
            {
                _robotBombDefuser = new RobotBombDefuser();
                _robotBombDefuser.ConnectWireless(_communicationWaveLength);
            }

            for (int i = 0; i < _connectionAttempts; i++)
            {
                if (_robotBombDefuser.IsConnected() != true)
                {
                    _robotBombDefuser.ConnectWireless(_communicationWaveLength);
                }
                else
                {
                    break;
                }
            }
            if (_robotBombDefuser.IsConnected() != true)
            {
                throw new BadConnectionException("No connection with remote bomb diffuser robot could be made after few attempts.");
            }
        }
    }

    class BadConnectionException : Exception
    {
        public BadConnectionException(string message)
            : base(message)
        {
        }
    }
}