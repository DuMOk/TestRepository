using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------List of Personal----------------");
            Console.WriteLine("Select the type of access\n (1 - Memory or 2 - File)");
            int n = int.Parse(Console.ReadLine());
            if (n == 1)
                MemoryAccess();
            if (n == 2)
                FileAccess();
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
        static void MemoryAccess()
        {
            MemoryAccessors MemAcc = new MemoryAccessors();
            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");
                int op = int.Parse(Console.ReadLine());
                string nameperson = null;
                switch (op)
                {
                    case 1:
                        MemAcc.GetAll();
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the name of person : ");
                        nameperson = Console.ReadLine();
                        MemAcc.GetByName(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the name of person : ");
                        nameperson = Console.ReadLine();
                        MemAcc.DeleteByName(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Enter the name  of person : ");
                        nameperson = Console.ReadLine();
                        MemAcc.Insert(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5:
                        return;
                    default: Console.WriteLine("Incorrect input, Try again!");
                        break;
                }
            }
        }
        static void FileAccess()
        {
            DataFileAccessors FileAcc = new DataFileAccessors();
            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");
                int op = int.Parse(Console.ReadLine());
                string nameperson = null;
                switch (op)
                {
                    case 1:
                        FileAcc.GetAll();
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the name of person : ");
                        nameperson = Console.ReadLine();
                        FileAcc.GetByName(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the name of person : ");
                        nameperson = Console.ReadLine();
                        FileAcc.DeleteByName(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Enter the name  of person : ");
                        nameperson = Console.ReadLine();
                        FileAcc.Insert(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5:
                        return;
                    default: Console.WriteLine("Incorrect input, Try again!");
                        break;
                }
            }
        }
    }
}
