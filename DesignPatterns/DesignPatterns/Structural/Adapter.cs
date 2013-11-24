// Book "������ ������� - ������, �� ����" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// �� ���� ��������� � ��������� ����.
// ������ ����� ��� �� ��� ���, ����� �� ��������� ��� �������� �� ������ ������� �� ��� �����.
using System;

namespace DesignPatterns.Structural
{
    // ���� ���� ���� ��������������
    class OldElectricitySystem
    {
        public string MatchThinSocket()
        {
            return "220V";
        }
    }
    // ���������������������� ���������
    interface INewElectricitySystem
    {
        string MatchWideSocket();
    }
    class NewElectricitySystem : INewElectricitySystem
    {
        public string MatchWideSocket()
        {
            return "220V";
        }
    }
    // �������
    class Adapter : INewElectricitySystem
    {
        private readonly OldElectricitySystem _adaptee;
        public Adapter(OldElectricitySystem adaptee)
        {
            _adaptee = adaptee;
        }
        // � ��� ������ ��� ���� 
        // ��� ������� ��������� �� ����, �� �� (���) �� ������ ����������� ����� � �� �� �� ������
        public string MatchWideSocket()
        {
            return _adaptee.MatchThinSocket();
        }
    }

    class ElectricityConsumer
    {
        // �������� ������� ����쳺 ����� ���� �������
        public static void ChargeNotebook(INewElectricitySystem electricitySystem)
        {
            Console.WriteLine(electricitySystem.MatchWideSocket());
        }
    }

    public class AdapterDemo
    {
        public static void Run()
        {
            // 1)
            // �� ������ � ����� ����������� ����� ����� ��������
            var newElectricitySystem = new NewElectricitySystem();
            ElectricityConsumer.ChargeNotebook(newElectricitySystem);

            // 2)
            // �� ������ ������������ �� ����� �������, �������������� �������
            var oldElectricitySystem = new OldElectricitySystem();
            var adapter = new Adapter(oldElectricitySystem);
            ElectricityConsumer.ChargeNotebook(adapter);
        }
    }
}