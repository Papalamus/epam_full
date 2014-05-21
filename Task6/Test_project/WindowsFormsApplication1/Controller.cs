using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataObjects.DataBase.Interface;
using DataObjects.DataBase.PersonConnecters;
using DataObjects.Entities;

namespace WindowsFormsApplication1
{
    class Controller
    {
        public enum ConnecterType
        {
            MyORM, ADO
        }
        public enum EntityType
        {
            Person, Article
        }

        
        public ConnecterType ChosenConnecterType{ get; set; }

        public EntityType ChosenEntityType { get; set; }

        public void GetAll(DataGridView gridView)
        {
            switch (ChosenEntityType)
            {
                case EntityType.Article:
                    ShowAll(gridView, MakeConnecter<Article>());
                    break;
                case EntityType.Person:
                    ShowAll(gridView, MakeConnecter<Person>());
                    break;
            }
        }

        public void Select(DataGridView gridView,object ID)
        {
            switch (ChosenEntityType)
            {
                case EntityType.Article:
                    ShowById(gridView, MakeConnecter<Article>(),ID);
                    break;
                case EntityType.Person:
                    ShowById(gridView, MakeConnecter<Person>(),ID);
                    break;
            }
        }
        public void Delete(object ID)
        {
            switch (ChosenEntityType)
            {
                case EntityType.Article:
                    Delete(MakeConnecter<Article>(), ID);
                    break;
                case EntityType.Person:
                    Delete(MakeConnecter<Person>(), ID);
                    break;
            }
        }
        public void Insert(object obj)
        {
            switch (ChosenEntityType)
            {
                case EntityType.Article:
                    Article art = obj as Article;
                    Insert(MakeConnecter<Article>(), art);
                    break;
                case EntityType.Person:
                    Person pers = obj as Person;
                    Insert(MakeConnecter<Person>(), pers);
                    break;
            }
        }

        private void Insert<T>(IPersonConnecter<T> connecter, T obj)
        {
            connecter.Insert(obj);
        }
        private void Delete<T>(IPersonConnecter<T> connecter, object ID)
        {
            connecter.DeletebyID(ID);
        }

        private void ShowById<T>(DataGridView gridView, IPersonConnecter<T> connecter, object ID)
        {
            gridView.DataSource = connecter.GetbyID(ID);
        }

        private void ShowAll<T>(DataGridView gridView, IPersonConnecter<T> connecter)
        {
            gridView.DataSource = connecter.GetAll();
        }

        public List<object> GetConnecter()
        {
            Helper h = new Helper(MakeConnecter<Person>(),typeof(Person));
            IList result = h.GetAll();
            switch (ChosenConnecterType)
            {
                //case ConnecterType.ADO:
                //    return ;
                //case ConnecterType.MyORM:
                //    return new MyOrmConnecter<T>();
            }
            return null;
        }

        private IPersonConnecter<T> MakeConnecter<T>() where T : class, IAdoSaveable, new()
        {
            //TODO Исправить ошибку
            //switch (ChosenConnecterType)
            //{
            //    case ConnecterType.ADO:
            //        return new AdoConnecter<T>();
            //    case ConnecterType.MyORM:
            //       return new MyOrmConnecter<T>();
            //}
            return null;
        }

    }
}
