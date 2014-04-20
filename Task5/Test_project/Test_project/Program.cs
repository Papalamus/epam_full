using System;
using DataObjects.DataBase;
using System.Reflection;
using DataObjects.DataBase.Interface;
using DataObjects.DataBase.PersonConnecters;

namespace DataObjects
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Test.MakeDbFile(@"Data.txt", Test.getTestList());
            //IPersonConnecter<Person> DB = new AdoConnecter();
            ////foreach (var element in DB.GetAll())
            //{
            //    Console.WriteLine(element);
            //}

            //NUnitTests n1 = new NUnitTests();
            //IPersonConnecter<Person> DB = new MyOrmConnecter<Person>();
            
           
            //foreach (var person in DB.GetAll())
            //{
            //    Console.WriteLine(person.ToString());
            //}
            //Console.WriteLine("____________________________");
            //DB.Insert(n1.T1[0]);

            //foreach (var person in DB.GetAll())
            //{
            //    Console.WriteLine(person.ToString());
            //}
            //DB.DeletebyID(n1.T1[0].INN);
            //Console.WriteLine("____________________________");
            //foreach (var person in DB.GetAll())
            //{
            //    Console.WriteLine(person.ToString());
            //}

            //Console.WriteLine();
            //Console.ReadKey();            
            
        //    NUnitTests n1 = new NUnitTests();
        //    foreach (var person in n1.T1)
        //    {
        //        DB.Insert(person);
        //    }
        //    foreach (var element in DB.GetAll())
        //    {
        //        Console.WriteLine(element);
        //    }
        //    Console.WriteLine("______________________________________________");
        //    foreach (var person in n1.T1)
        //    {
        //        DB.DeletebyName(person.Name);
        //    }
        //    foreach (var element in DB.GetAll())
        //    {
        //        Console.WriteLine(element);
        //    }

        //    Console.ReadKey();
        }
         
    }
}
