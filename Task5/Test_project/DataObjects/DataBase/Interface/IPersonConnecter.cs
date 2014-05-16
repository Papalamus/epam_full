using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.DataBase.Interface
{
    public interface IPersonConnecter<T>
    {
        List<T> GetAll();
        T GetbyID(object ID);
        void DeletebyID(object ID);
        bool Insert(T p);
    }
    
}
