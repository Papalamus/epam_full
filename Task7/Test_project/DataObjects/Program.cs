using System;
using DataObjects.DataBase;
using System.Reflection;
using DataObjects.DataBase.Interface;
using DataObjects.DataBase.PersonConnecters;
using DataObjects.Entities;
using Test_project.DataBase.PersonConnecters;

namespace DataObjects
{
    class Program
    {
        
        static void Main(string[] args)
        {

            MyOrmBase m = new MyOrmBase();
            MappedType pm = new MappedType(typeof (Person));
            MappedType am = new MappedType(typeof (Article));
            m.Schema.Request(pm);
            m.Schema.Request(am);
            
            //DbCommandMaker db = new DbCommandMaker(typeof (Person), m);
            //db.LoadInstance();
            Person test = CreateTest();
            m.Insert(typeof(Person),test);
            Person nou= new Person();
            nou = (Person)m.Get(typeof(Person), 6);

            Console.WriteLine(nou);
            Console.ReadKey();
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

        public static Person CreateTest()
        {
            Person result = new Person();
            result.Adress = "Daleko";
            result.Age = 7;
            result.INN = 6;
            result.Name = "Peka";
            result.Position = "Baker";
            result.Salary = 322;
            result.Surname = "YoDog";
            Article f= new Article(){ArticleCode = 4, Title = "Omg",Value = 228};
            Article t = new Article() { ArticleCode = 10, Title = "Lol", Value = 1488 };
            result.lolo = new Article[]{f,t};
            return result;
        }
    }
}
