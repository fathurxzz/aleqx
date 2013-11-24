// Book "������ ������� - ������, �� ����" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// �� ���� ��������� � ��������� ����.
// ������ ����� ��� �� ��� ���, ����� �� ��������� ��� �������� �� ������ ������� �� ��� �����.
using System;

namespace DesignPatterns.Behavioural
{
    class MessagesSearcher
    {
        protected DateTime DateSent;
        protected String PersonName;
        protected int ImportanceLevel;

        public MessagesSearcher(DateTime dateSent, String personName, int importanceLevel)
        {
            DateSent = dateSent;
            PersonName = personName;
            ImportanceLevel = importanceLevel;
        }

        //����� �������� (primitive operations)
        protected virtual void CreateDateCriteria()
        {
            Console.WriteLine("Standard date criteria has been applied.");
        }
        protected virtual void CreateSentPersonCriteria()
        {
            Console.WriteLine("Standard person criteria has been applied.");
        }
        protected virtual void CreateImportanceCriteria()
        {
            Console.WriteLine("Standard importance criteria has been applied.");
        }

        //��������� �����
        public String Search()
        {
            CreateDateCriteria();
            CreateSentPersonCriteria();
            Console.WriteLine("Template method does some verification accordingly to search algo.");
            CreateImportanceCriteria();
            Console.WriteLine("Template method verifies if message could be so important or useless from person provided in criteria.");
            Console.WriteLine();
            return "Some list of messages...";
        }
    }

    class ImportantMessagesSearcher : MessagesSearcher
    {
        public ImportantMessagesSearcher(DateTime dateSent, String personName)
            : base(dateSent, personName, 3) // 3 ������ �� ����������� �������
        {
        }

        //���� �������� ����������� (IMPORTANT � ����)
        protected override void CreateImportanceCriteria()
        {
            Console.WriteLine("Special importance criteria has been formed: IMPORTANT");
        }
    }

    class UselessMessagesSearcher : MessagesSearcher
    {
        public UselessMessagesSearcher(DateTime dateSent, String personName)
            : base(dateSent, personName, 1) //1 ������ � ���� ������ �����
        {
        }
        //���� �������� ����������� (USELESS � ����)
        protected override void CreateImportanceCriteria()
        {
            Console.WriteLine("Special importance criteria has been formed: USELESS");
        }
    }

    class TemplateMethodDemo
    {
        public static void Run()
        {
            MessagesSearcher searcher = new UselessMessagesSearcher(DateTime.Today, "Sally");
            searcher.Search();
            searcher = new ImportantMessagesSearcher(DateTime.Today, "Killer");
            searcher.Search();
        }
    }
}