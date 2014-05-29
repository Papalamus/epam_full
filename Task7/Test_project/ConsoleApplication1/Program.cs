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
            c.SelectEntity();
        
        }
    }
}
