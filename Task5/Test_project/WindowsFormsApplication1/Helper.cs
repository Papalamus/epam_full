using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DataObjects.DataBase.Interface;
using DataObjects.Entities;

namespace WindowsFormsApplication1
{
    class Helper
    {
        private object _inter;
        private Type type;

        public Helper(object inter, Type type)
        {
            this._inter = inter;
            this.type = type;
        }

        public IList GetAll()
        {
            Type elementType = type;
            Type connecterType = typeof(IPersonConnecter<>).MakeGenericType(elementType);
            var methods = connecterType.GetMethods();
            object result = connecterType.InvokeMember("GetAll", BindingFlags.Public | BindingFlags.Instance| BindingFlags.IgnoreReturn
                | BindingFlags.InvokeMethod, null, _inter, new object[0]);

            Type listType = typeof(List<>).MakeGenericType(elementType);
            List<object> list = (List<object>)result;
            
            return null;
        }

        object GetbyID<T>(IPersonConnecter<T> connecter, object ID)
        {
            return connecter.GetbyID(ID);
        }

        void DeletebyID<T>(IPersonConnecter<T> connecter, object ID)
        {
            connecter.DeletebyID(ID);
        }
        bool Insert<T>(IPersonConnecter<T> connecter, T p)
        {
            return connecter.Insert(p);
        }
    }
}
