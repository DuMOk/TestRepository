using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlServerCe;
using System.Configuration;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------List of Personal----------------");
            while (true)
            {
                Console.WriteLine("Select the type of access:");
                Console.WriteLine("1 - Memory | 2 - File | 3 - ADO.Net | 4 - MyORM | 5 - Quit)");
                int n = int.Parse(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        MemoryAccess();
                        break;
                    case 2:
                        FileAccess();
                        break;
                    case 3:
                        ADONETAccess();
                        break;
                    case 4:
                        MyORMAccess();
                        break;
                    case 5:
                        return;
                    default: Console.WriteLine("Incorrect input, Try again!");
                        break;
                }
            }
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
                        Console.WriteLine("{0}", MemAcc.GetByName(nameperson));
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

        static void ADONETAccess()
        {
            ADONetAccessors ADOAcc = new ADONetAccessors();
            ADOAcc.OpenConnection();
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
                        ADOAcc.GetAll();
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the name of person : ");
                        nameperson = Console.ReadLine();
                        Console.WriteLine("{0}", ADOAcc.GetByName(nameperson));
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the name of person : ");
                        nameperson = Console.ReadLine();
                        ADOAcc.DeleteByName(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Enter the name  of person : ");
                        nameperson = Console.ReadLine();
                        ADOAcc.Insert(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5:
                        ADOAcc.CloseConnection();
                        return;
                    default: Console.WriteLine("Incorrect input, Try again!");
                        break;
                }
            }
        }

        static void MyORMAccess()
        {
            MyORM MyORMAcc = new MyORM();
            MyORMAcc.OpenConnection();
            Console.WriteLine("Select the table:");
            Console.WriteLine("1 - Person | 2 - Employees");
            Console.Write("Enter number of table : ");
            int table = int.Parse(Console.ReadLine());
            object tbl;
            if (table == 1)
                tbl = new ClsPerson();
            else
                tbl = new ClsEmpl();
            MyORMAcc.obj = tbl;
            MyORMAcc.OpenConnection();
            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");
                int op = int.Parse(Console.ReadLine());
                string nameFld = null;
                switch (op)
                {
                    case 1:
                        MyORMAcc.GetAll();
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the name of person : ");
                        nameFld = Console.ReadLine();
                        Console.WriteLine("{0}", MyORMAcc.GetByName(nameFld));
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the name of person : ");
                        nameFld = Console.ReadLine();
                        MyORMAcc.DeleteByName(nameFld);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Enter the name  of person : ");
                        nameFld = Console.ReadLine();
                        MyORMAcc.Insert(nameFld);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5:
                        MyORMAcc.CloseConnection();
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
                        Console.WriteLine("{0}", FileAcc.GetByName(nameperson));
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
