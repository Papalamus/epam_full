﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.DataBase.Interface;
using DataObjects.Entities;

namespace DataObjects.DataBase
{
    public class MemoryConnector :IPersonConnecter<Person>
    {
        List<Person> DB ;
        public MemoryConnector(List<Person> DB)
        {
              this.DB = new List<Person>(DB);
        }


        public List<Person> GetAll()
        {
            return DB;
        }

        

        
        public bool Insert(Person p)
        {
            DB.Add(p);
            return true;
        }


        public Person GetbyID(object ID)
        {
            string Name = ID.ToString();
            return DB.Find((i) => i.Name == Name);
        }

        public void DeletebyID(object ID)
        {
            string Name = ID.ToString();
            DB.Remove(DB.Find((i) => i.Name == Name));
        }
    }
}
