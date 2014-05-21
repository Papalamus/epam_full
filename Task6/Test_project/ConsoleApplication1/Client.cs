using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.InputHelpers;
using DataObjects.Entities;
using DataObjects.DataBase.Interface;
using DataObjects.DataBase.PersonConnecters;
using NLog;
using Test_project.DataBase.PersonConnecters;

namespace ConsoleApplication1
{
    public delegate bool TryParseHandler<T>(string s,out T result);
    class Client
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public void SelectEntity()
        {
            
            try
            {
                const string msg = "Выберите тип объекта \n";
                ConsoleDialog entityChoise = new ConsoleDialog(msg);
                bool exit = false;

                entityChoise.Add("Person", () => Process(SelectConnector<Person>(), new PersonInput()));
                entityChoise.Add("Article", () => Process(SelectConnector<Article>(), new ArticleInput()));
                entityChoise.Add("Назад", () => exit = true);
                do
                {
                    entityChoise.Run();
                } while (!exit);
            }
            //finally
            //{
            //}
            catch
            (Exception e)
            {
                Console.WriteLine("Произошла ошибка приложение прекратит свою работу");
                _logger.Fatal("Ошибка {0} ",e.ToString());

            }
        }

        public void Process<T>(IPersonConnecter<T> connecter, IInputHelper<T> inputHelper)
        {
            const string msg = "";
            bool exit = false;
            
            ConsoleDialog actionChoise = new ConsoleDialog(msg);
            actionChoise.Add("Вывести все", () => PrintList(connecter.GetAll()));
            actionChoise.Add("Вставить элемент", () => connecter.Insert(inputHelper.MakeObject()));
            actionChoise.Add("Удалить элемент", () => connecter.DeletebyID(inputHelper.RequestId()));
            actionChoise.Add("Получить элемент", () => GetById(connecter,inputHelper));
            actionChoise.Add("Назад", () => exit = true);
            do
            {
                actionChoise.Run();
            } while (!exit);
        }

        private void GetById<T>(IPersonConnecter<T> connecter, IInputHelper<T> inputHelper)
        {
            object obj = connecter.GetbyID(inputHelper.RequestId());
            string msg = (obj == null) ? "Элемент с таким ID не найден":obj.ToString();
            Console.WriteLine(msg);
        }

        private void PrintList<T>(List<T> list)
        {
            foreach (var entry in list)
            {
                Console.WriteLine(entry);    
            }
            
        }


        public IPersonConnecter<T> SelectConnector<T>() where T : class,IAdoSaveable, new()
        {
            IPersonConnecter<T> connecter =null;
            const string msg = "Выберите тип базы данных \n";
            ConsoleDialog connecterChoise = new ConsoleDialog(msg);

            connecterChoise.Add("Orm", () => connecter = MyOrmBase.GetInstance().GetConnecter<T>());
            connecterChoise.Add("Простой Ado.net", () => connecter = new AdoConnecter<T>());

            connecterChoise.Run();
            return connecter;
        }

    }
    
}
