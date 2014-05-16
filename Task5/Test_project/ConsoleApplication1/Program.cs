using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
using DataObjects.DataBase;
using DataObjects.Entities;
using Test_project.DataBase.PersonConnecters;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client();
            //c.SelectEntity();
            MyOrmBase n = new MyOrmBase();
            Article[] articles = new Article[1];
            articles[0]= new Article(){ArticleCode = 334,Title = "Зависим",Value =8990};
            Person p = new Person(){INN = 1234,Name = "Fool",lolo= articles};
            n.Insert(p.GetType(),p);
        }
    }
}
