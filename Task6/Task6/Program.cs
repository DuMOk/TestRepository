using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SqlServerCe;
using System.IO;

namespace Task6
{
    class Program
    {   
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------List of Personal----------------");

            IIoCContainer ninjectContainer = new MyNinjectContainer();

            while (true)
            {
                Console.WriteLine("Select the type of access:");
                Console.WriteLine("1 - Memory | 2 - File | 3 - ADO.Net | 4 - MyORM | 5 - Quit)");

                int n = int.Parse(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        ninjectContainer.Register<IDataAccessors<string>, MemoryAccessors<string>>();
                        MemoryAccess(ninjectContainer); 
                        break;
                    case 2:
                        ninjectContainer.Register<IDataAccessors<string>, DataFileAccessors<string>>();
                        FileAccess(ninjectContainer); 
                        break;
                    case 3:
                        ninjectContainer.Register<IDataAccessors<Student>, ADONetAccessors<Student>>();
                        ADONETAccess(ninjectContainer); 
                        break;
                    case 4:
                        Console.WriteLine("Select the table:");
                        Console.WriteLine("1 - Student | 2 - Group");
                        Console.Write("Enter number of table : ");
                        int table = int.Parse(Console.ReadLine());
                        
                        switch (table)
                        {
                            case 1:
                                ninjectContainer.Register<IDataAccessors<Student>, MyORM<Student>>();
                                MyORMAccessStudent(ninjectContainer); 
                                break;
                            case 2:
                                ninjectContainer.Register<IDataAccessors<Group>, MyORM<Group>>();
                                MyORMAccessGroup(ninjectContainer); 
                                break;
                            default: Console.WriteLine("Incorrect input, Try again!"); break;
                        }
                        break;
                    case 5: return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void MemoryAccess(IIoCContainer container)
        {
            var memAcc = container.Resolve<IDataAccessors<string>>();

            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");

                int op = int.Parse(Console.ReadLine());
                string namePerson = null;

                switch (op)
                {
                    case 1:
                        Console.WriteLine("All persons:");
                        foreach (string name in memAcc.GetAll())
                            Console.WriteLine("->{0}", name);

                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the name of person : ");
                        namePerson = Console.ReadLine();

                        Console.Write("Get person by name : ");
                        if (memAcc.GetByName(namePerson) == null)
                            Console.WriteLine("person with name {0} does not exist!", namePerson);
                        else
                            Console.WriteLine("{0}", memAcc.GetByName(namePerson));

                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the name of the person you want to delete : ");
                        namePerson = Console.ReadLine();

                        memAcc.DeleteByName(namePerson);

                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Enter the name  of person you want to insert : ");
                        namePerson = Console.ReadLine();

                        memAcc.Insert(namePerson);

                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void FileAccess(IIoCContainer container)
        {
            var fileAcc = container.Resolve<IDataAccessors<string>>();

            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");

                int op = int.Parse(Console.ReadLine());
                string namePerson = null;

                switch (op)
                {
                    case 1:
                        Console.WriteLine("All persons:");
                        List<string> persons = new List<string>();
                        
                        try
                        {
                            persons = fileAcc.GetAll();
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
                        namePerson = Console.ReadLine();

                        try
                        {
                            Console.Write("Get person by name : ");
                            if (fileAcc.GetByName(namePerson) == null)
                                Console.WriteLine("person with name {0} does not exist!", namePerson);
                            else
                                Console.WriteLine("{0}", fileAcc.GetByName(namePerson).ToString());
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
                        namePerson = Console.ReadLine();

                        try
                        {
                            fileAcc.DeleteByName(namePerson);
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
                        namePerson = Console.ReadLine();

                        try
                        {
                            fileAcc.Insert(namePerson);
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

        static void ADONETAccess(IIoCContainer container)
        {
            var adoAcc = container.Resolve<IDataAccessors<Student>>();

            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");

                int op = int.Parse(Console.ReadLine());
                string nameStudent = null;

                switch (op)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Id Student | FIO Student | Id Group");
                            foreach (Student stud in adoAcc.GetAll())
                                Console.WriteLine("   {0,-7} | {1,-11} |    {2}", stud.field1, stud.field2, stud.field3);
                        
                            Console.WriteLine("Press any key ...");
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Data Base was not connected! Exit!");
                            return;
                        }
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the FIO of student : ");
                        nameStudent = Console.ReadLine();
                        try
                        {
                            Console.WriteLine("Get person by name : ");
                            if (adoAcc.GetByName(nameStudent) == null)
                                Console.WriteLine("Student with FIO {0} does not exist!", nameStudent);
                            else
                            {
                                Student stud = adoAcc.GetByName(nameStudent);
                                Console.WriteLine("Id Student {0} \nFIO Student {1}\nId Group {2}",
                                    stud.field1, stud.field2, stud.field3);
                            }
                            Console.WriteLine("Press any key ...");
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Data Base was not connected! Exit!");
                            return;
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the FIO of student : ");
                        nameStudent = Console.ReadLine();

                        try
                        {
                            adoAcc.DeleteByName(nameStudent);
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
                            adoAcc.Insert(student);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record was not added!\n");
                            break;
                        }

                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void MyORMAccessStudent(IIoCContainer container)
        {
            var myORMAcc = container.Resolve<IDataAccessors<Student>>();
            
            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");

                int op = int.Parse(Console.ReadLine());
                string nameStud = null;

                switch (op)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Id Student | FIO Student | Id Group");
                            foreach (var stud in myORMAcc.GetAll())
                                Console.WriteLine("   {0,-7} | {1,-11} |    {2}", stud.field1, stud.field2, stud.field3);
                            
                            Console.WriteLine("Press any key ...");
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Data Base was not connected! Exit!");
                            return;
                        }
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the FIO of student : ");
                        nameStud = Console.ReadLine();
                        try
                        {
                            Console.Write("Get student by FIO : ");
                            if (myORMAcc.GetByName(nameStud) == null)
                                Console.WriteLine("Student with FIO {0} does not exist!", nameStud);
                            else
                            {
                                Student stud = myORMAcc.GetByName(nameStud);
                                Console.WriteLine("Id Student {0} \nFIO Student {1}\nId Group {2}",
                                    stud.field1, stud.field2, stud.field3);
                            }
                            Console.WriteLine("Press any key ...");
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Data Base was not connected! Exit!");
                            return;
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the FIO of student : ");
                        nameStud = Console.ReadLine();

                        try
                        {
                            myORMAcc.DeleteByName(nameStud);
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
                            myORMAcc.Insert(student);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record was no added!\n");
                            break;
                        }

                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }

        static void MyORMAccessGroup(IIoCContainer container)
        {
            var myORMAcc = container.Resolve<IDataAccessors<Group>>();

            while (true)
            {
                Console.WriteLine("Select required operation:");
                Console.WriteLine("1 - GetAll\n2 - GetByName\n3 - DeleteByName\n4 - Insert\n5 - Exit");
                Console.Write("Enter number of operation : ");

                int op = int.Parse(Console.ReadLine());
                string nameGrp = null;

                switch (op)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Id Group | Name Group");
                            foreach (Group grp in myORMAcc.GetAll())
                                Console.WriteLine("   {0,-7} | {1,-11} ", grp.field1, grp.field2);
                            Console.WriteLine("Press any key ...");
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Data Base was not connected! Exit!");
                            return;
                        }
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Enter the Name of group : ");
                        nameGrp = Console.ReadLine();
                        try
                        {
                            Console.Write("Get student by FIO : ");
                            if (myORMAcc.GetByName(nameGrp) == null)
                                Console.WriteLine("Student with FIO {0} does not exist!", nameGrp);
                            else
                            {
                                Group grp = myORMAcc.GetByName(nameGrp);
                                Console.WriteLine("Id Group {0} \nName Group {1}", grp.field1, grp.field2);
                            }
                            Console.WriteLine("Press any key ...");
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Data Base was not connected! Exit!");
                            return;
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Enter the Name of group : ");
                        nameGrp = Console.ReadLine();

                        try
                        {
                            myORMAcc.DeleteByName(nameGrp);
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
                            myORMAcc.Insert(group);
                        }
                        catch (SqlCeException e)
                        {
                            Console.WriteLine("Record was no added!\n");
                            break;
                        }

                        Console.WriteLine("Press any key ...");
                        Console.ReadKey();
                        break;
                    case 5: return;
                    default: Console.WriteLine("Incorrect input, Try again!"); break;
                }
            }
        }
    }
}
