﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Accessors;
using System.Configuration;
using System.Data.SqlServerCe;
using System.IO;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core;
using Castle.Core.Resource;

namespace ConUI
{
    class Program
    {       
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------List of Personal----------------");
            
            IWindsorContainer container = new WindsorContainer();
            container.Register(
                Component.For<IDataAccessors<string>>().ImplementedBy<MemoryAccessors<string>>().Named("MemAcc"),
                Component.For<IDataAccessors<string>>().ImplementedBy<DataFileAccessors<string>>().Named("FileAcc"),
                Component.For<IDataAccessors<Student>>().ImplementedBy<ADONetAccessors<Student>>().Named("ADOAcc"),
                Component.For<IDataAccessors<Student>>().ImplementedBy<MyORM<Student>>().Named("MyORMAccStud"),
                Component.For<IDataAccessors<Group>>().ImplementedBy<MyORM<Group>>().Named("MyORMAccGroup"));

            while (true)
            {
                Console.WriteLine("Select the type of access:");
                Console.WriteLine("1 - Memory | 2 - File | 3 - ADO.Net | 4 - MyORM | 5 - Quit)");
                
                int n = int.Parse(Console.ReadLine());
                
                switch (n)
                {
                    case 1: MemoryAccess(container); break;
                    case 2: FileAccess(container); break;
                    case 3: ADONETAccess(container); break;
                    case 4:
                        Console.WriteLine("Select the table:");
                        Console.WriteLine("1 - Student | 2 - Group");
                        Console.Write("Enter number of table : ");
                        int table = int.Parse(Console.ReadLine());
                        switch (table)
                        {
                            case 1: MyORMAccessStudent(container); break;
                            case 2: MyORMAccessGroup(container); break;
                            default: Console.WriteLine("Incorrect input, Try again!"); break;
                        }
                        break;
                    case 5: return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void MemoryAccess(IWindsorContainer container)
        {
            var MemAcc = container.Resolve<MemoryAccessors<string>>("MemAcc");

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
                        Console.WriteLine("All persons:");
                        foreach (string name in MemAcc.GetAll())
                            Console.WriteLine("->{0}", name);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the name of person : ");
                        nameperson = Console.ReadLine();
                        Console.Write("Get person by name : ");
                        if (MemAcc.GetByName(nameperson) == null)
                            Console.WriteLine("person with name {0} does not exist!", nameperson);
                        else
                            Console.WriteLine("{0}", MemAcc.GetByName(nameperson));
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the name of the person you want to delete : ");
                        nameperson = Console.ReadLine();
                        MemAcc.DeleteByName(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Enter the name  of person you want to insert : ");
                        nameperson = Console.ReadLine();
                        MemAcc.Insert(nameperson);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: container.Release(MemAcc); return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void FileAccess(IWindsorContainer container)
        {
            var FileAcc = container.Resolve<DataFileAccessors<string>>("FileAcc");
            var str = ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString;
            FileAcc.Path = str;
            
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
                        Console.WriteLine("All persons:");
                        List<string> persons = new List<string>();
                        try
                        {
                            persons = FileAcc.GetAll();
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine("File not found! Exit!");
                            return;
                        }
                        foreach (string name in persons)
                            Console.WriteLine("->{0}", name);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the name of person : ");
                        nameperson = Console.ReadLine();
                        try
                        {
                            Console.Write("Get person by name : ");
                            if (FileAcc.GetByName(nameperson) == null)
                                Console.WriteLine("person with name {0} does not exist!", nameperson);
                            else
                                Console.WriteLine("{0}", FileAcc.GetByName(nameperson).ToString());
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine("File not found! Exit!");
                            return;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the name of the person you want to delete : ");
                        nameperson = Console.ReadLine();
                        try
                        {
                            FileAcc.DeleteByName(nameperson);
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine("File not found! Exit!");
                            return;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Enter the name  of person you want to insert : ");
                        nameperson = Console.ReadLine();
                        try
                        {
                            FileAcc.Insert(nameperson);
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine("File not found! Exit!");
                            return;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void ADONETAccess(IWindsorContainer container)
        {
            var ADOAcc = container.Resolve<ADONetAccessors<Student>>("ADOAcc");
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            
            try
            {
                ADOAcc.OpenConnection(str);
            }
            catch (SqlCeException e)
            {
                Console.WriteLine("Data Base was not connected! Exit!");
                return;
            }
            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");
                
                int op = int.Parse(Console.ReadLine());
                string namestud = null;
                
                switch (op)
                {
                    case 1:
                        Console.WriteLine("Id Student | FIO Student | Id Group");
                        foreach (Student stud in ADOAcc.GetAll())
                            Console.WriteLine("   {0,-7} | {1,-11} |    {2}", stud.field1, stud.field2, stud.field3);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the FIO of student : ");
                        namestud = Console.ReadLine();
                        Console.WriteLine("Get person by name : ");
                        if (ADOAcc.GetByName(namestud) == null)
                            Console.WriteLine("Student with FIO {0} does not exist!", namestud);
                        else
                        {
                            Student stud = ADOAcc.GetByName(namestud);
                            Console.WriteLine("Id Student {0} \nFIO Student {1}\nId Group {2}",
                                stud.field1, stud.field2, stud.field3);
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the FIO of student : ");
                        namestud = Console.ReadLine();
                        try
                        {
                            ADOAcc.DeleteByName(namestud);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record failed to delete!\n");
                            break;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Student student = new Student();
                        
                        Console.Write("Enter the Id of student : ");
                        student.field1 = int.Parse(Console.ReadLine());
                        Console.Write("Enter the FIO of student : ");
                        student.field2 = Console.ReadLine();
                        Console.Write("Enter the Id Group of student : ");
                        student.field3 = int.Parse(Console.ReadLine());
                        
                        try
                        {
                            ADOAcc.Insert(student);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record was not added!\n");
                            break;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: ADOAcc.CloseConnection(); return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void MyORMAccessStudent(IWindsorContainer container)
        {
            var MyORMAcc = container.Resolve<MyORM<Student>>("MyORMAccStud");
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            
            try
            {
                MyORMAcc.OpenConnection(str);
            }
            catch (SqlCeException e)
            {
                Console.WriteLine("Data Base was not connected! Exit!");
                return;
            }
            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");
                
                int op = int.Parse(Console.ReadLine());
                string namestud = null;
                
                switch (op)
                {
                    case 1:
                        Console.WriteLine("Id Student | FIO Student | Id Group");
                        foreach (var stud in MyORMAcc.GetAll())
                            Console.WriteLine("   {0,-7} | {1,-11} |    {2}", stud.field1, stud.field2, stud.field3);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the FIO of student : ");
                        namestud = Console.ReadLine();
                        Console.Write("Get student by FIO : ");
                        if (MyORMAcc.GetByName(namestud) == null)
                            Console.WriteLine("Student with FIO {0} does not exist!", namestud);
                        else
                        {
                            Student stud = MyORMAcc.GetByName(namestud);
                            Console.WriteLine("Id Student {0} \nFIO Student {1}\nId Group {2}",
                                stud.field1, stud.field2, stud.field3);
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the FIO of student : ");
                        namestud = Console.ReadLine();
                        try
                        {
                            MyORMAcc.DeleteByName(namestud);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record failed to delete!\n");
                            break;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Student student = new Student();
                        
                        Console.Write("Enter the Id of student : ");
                        student.field1 = int.Parse(Console.ReadLine());
                        Console.Write("Enter the FIO of student : ");
                        student.field2 = Console.ReadLine();
                        Console.Write("Enter the Id Group of student : ");
                        student.field3 = int.Parse(Console.ReadLine());
                        
                        try
                        {
                            MyORMAcc.Insert(student);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record was no added!\n");
                            break;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: MyORMAcc.CloseConnection(); return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void MyORMAccessGroup(IWindsorContainer container)
        {
            var MyORMAcc = container.Resolve<MyORM<Group>>("MyORMAccGroup");
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            
            try
            {
                MyORMAcc.OpenConnection(str);
            }
            catch (SqlCeException e)
            {
                Console.WriteLine("Data Base was not connected! Exit!");
                return;
            }
            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");
                
                int op = int.Parse(Console.ReadLine());
                string namegrp = null;
                
                switch (op)
                {
                    case 1:
                        Console.WriteLine("Id Group | Name Group");
                        foreach (Group grp in MyORMAcc.GetAll())
                            Console.WriteLine("   {0,-7} | {1,-11} ", grp.field1, grp.field2);
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the Name of group : ");
                        namegrp = Console.ReadLine();
                        Console.Write("Get student by FIO : ");
                        if (MyORMAcc.GetByName(namegrp) == null)
                            Console.WriteLine("Student with FIO {0} does not exist!", namegrp);
                        else
                        {
                            Group grp = MyORMAcc.GetByName(namegrp);
                            Console.WriteLine("Id Group {0} \nName Group {1}", grp.field1, grp.field2);
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the Name of group : ");
                        namegrp = Console.ReadLine();
                        try
                        {
                            MyORMAcc.DeleteByName(namegrp);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record failed to delete!\n");
                            break;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Group group = new Group();
                        
                        Console.Write("Enter the Id of group : ");
                        group.field1 = int.Parse(Console.ReadLine());
                        Console.Write("Enter the Name of group : ");
                        group.field2 = Console.ReadLine();
                        
                        try
                        {
                            MyORMAcc.Insert(group);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record was no added!\n");
                            break;
                        }
                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: MyORMAcc.CloseConnection(); return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }
    }
}
