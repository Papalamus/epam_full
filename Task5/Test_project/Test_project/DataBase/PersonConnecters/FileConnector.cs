using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DataObjects.DataBase.Interface;
using DataObjects.Entities;

namespace DataObjects.DataBase
{
    public class FileConnector :IPersonConnecter<Person>
    {
        XmlSerializer serializer;
        private List<Person> cache;
        string path;

        protected List<Person> Cache 
        { 
            get 
            {
                if (cache == null)
                {
                    using (Stream fstream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        cache = (List<Person>)serializer.Deserialize(fstream);
                    }
                }
                return cache;
            }        
                
        }

        public FileConnector(string path)
        {
            this.path = path;
            this.serializer = new XmlSerializer(typeof(List < Person >), new Type[]{typeof(Person)});
        }


        public List<Person> GetAll()
        {
            return Cache;
        }

        public Person GetbyID(object id)
        {
            string Name = id.ToString();
            return Cache.Find((i) => i.Name == Name);
        }

        public void DeletebyID(object id)
        {
            string Name = id.ToString();
            Cache.Remove(Cache.Find((i) => i.Name == Name));
            WriteChanges();
        }

        public bool Insert(Person p)
        {
            try
            {
                Cache.Add(p);
                WriteChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void WriteChanges()
        {
            using (Stream fstream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(fstream, cache);
            }
        }

        
    }
}
