using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataObjects.DataBase.Interface;
using DataObjects.DataBase.PersonConnecters;
using DataObjects.Entities;
using Test_project.DataBase.PersonConnecters;

namespace WebClient
{
    public enum ConnecterType
    {
        MyORM, ADO
    }
    public enum EntityType
    {
        Person, Article
    }
     
    public class Controller
    {
        private IPersonConnecter<Person> _pc;
        private IPersonConnecter<Article> _ac;
        private bool _isRefreshNeed = true;
        public bool IsRefreshNeed
        {
            get
            {
                if (_isRefreshNeed)
                {
                    _isRefreshNeed = false;
                    return true;
                }
                return false;
            } 
            set { _isRefreshNeed = value; }
        }

        public IPersonConnecter<Person> PC
        {
            get {
                if (_pc == null)
                {
                    _pc = MakeConnecter<Person>();         
                }
                return _pc;
            }
        }
        public IPersonConnecter<Article> AC
        {
            get
            {
                if (_ac == null)
                {
                    _ac = MakeConnecter<Article>();
                }
                return _ac;
            }
        }


        private ConnecterType _chosenConnecter;
        public ConnecterType ChosenConnecter {
            get { return _chosenConnecter; }
            set {
                _chosenConnecter = value;
                switch (ChosenEntity)
                {
                    case EntityType.Article:
                        _ac = null;
                        break;
                    case EntityType.Person:
                        _pc = null;
                        break;
                }
            }
        }
        public EntityType ChosenEntity { get; set; }



        private IPersonConnecter<T> MakeConnecter<T>() where T :class, IAdoSaveable,new()
        {
            switch (ChosenConnecter)
            {
                case ConnecterType.ADO:
                    return new AdoConnecter<T>();
                case ConnecterType.MyORM:
                    MyOrmBase myOrmBase = new MyOrmBase();
                    return myOrmBase.GetConnecter<T>();
                default:
                    return null;
            }
            
        } 

        public List<object> GetAll()
        {
            List<object> result = null;
            switch (ChosenEntity)
            {
                    
                case  EntityType.Article:
                    result =  AC.GetAll().ToList<object>();
                    break;
                case EntityType.Person:
                    result =  PC.GetAll().ToList<object>();
                    break;
            }
            return result;
        }
        public object GetByID(object ID)
        {
            object result = null;
            switch (ChosenEntity)
            {

                case EntityType.Article:
                    result = AC.GetbyID(ID);
                    break;
                case EntityType.Person:
                    result = PC.GetbyID(ID);
                    break;
            }
            return result;
        }
        public void DeleteByID(object ID)
        {
            switch (ChosenEntity)
            {
                case EntityType.Article:
                    AC.DeletebyID(ID);
                    break;
                case EntityType.Person:
                    PC.DeletebyID(ID);
                    break;
            }
        }
        public void Insert(object obj)
        {
            switch (ChosenEntity)
            {
                case EntityType.Article:
                    Article a = obj as Article;
                    if (a != null)
                    {
                        AC.Insert(a);
                    }
                    else
                    {
                        throw new ArgumentException("Объект для вставки не является типом Article", "obj");
                    }
                    break;
                case EntityType.Person:
                    Person p = obj as Person;
                    if (p != null)
                    {
                        PC.Insert(p);
                    }
                    else
                    {
                        throw new ArgumentException("Объект для вставки не является типом Person","obj");
                    }
                    break;
            }
        }
    }
}