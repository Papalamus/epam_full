using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.Attributes;
using System.Reflection;

namespace DataObjects
{
    class AssemblyInspector
    {
        Assembly _asm;

        public AssemblyInspector(Assembly asm)
        {
            this._asm = asm;
        }

        public void InspectClases(ClassInspectHandler handler)
        {
            var classTypes = from t in _asm.GetTypes()
                             where
                                 t.IsClass
                             select t;
            foreach (Type t in classTypes)
            {
                handler(t);
            }
        }

        public void OverridedInspect(Type t)
        {            
            var classMethods = from m in t.GetMethods()
                                where CheckAttribute(m, typeof(OverridedAttribute))
                                select m;
            foreach (var m in classMethods)
            {
                Console.WriteLine(@" В классе {0}, проверяем метод  {1} ", t, m.Name);
                Type baseType = t.BaseType;
                MethodInfo baseMethod = baseType.GetMethod(m.Name);
                if ((baseMethod == null)|| !CheckOverriding(baseMethod, m))
                {
                    Console.WriteLine("Неправильно переопределен метод {1} в классе {0},  ", t, m.Name);
                }
                
            }
            
        }


        public string StoreObject(object obj)
        {
            Dictionary<string, string> insertParams = new Dictionary<string, string>();
            MyOrmInspect(obj, insertParams );
            return MakeInsertString(insertParams,GetTableName(obj));

        }

       

     

        public void MyOrmInspect(object obj, Dictionary<string, string> insertParams)
        {
            Type t = obj.GetType();
           
            if (!CheckAttribute(t, typeof(MyAttribute)))
                return ;
            var fields = from f in t.GetFields()
                                where CheckAttribute(f, typeof(MyAttribute))
                                select f;

            foreach (var f in fields)
            {
                ProcessField(f, insertParams, obj);     
            }
            var properties = from f in t.GetProperties()
                         where CheckAttribute(f, typeof(MyAttribute))
                         select f;
            
            foreach (var p in properties)
            {               
                ProcessField(p, insertParams, obj);
            }
            
        }

        private void ProcessField(FieldInfo f, Dictionary<string, string> selectParams, object obj)
        {
            if (f.ReflectedType.IsPrimitive)
            {
                selectParams.Add(f.Name, f.GetValue(obj).ToString());
            }
            else
            {
                StoreObject(f.GetValue(obj));
            }
        }
        private void ProcessField(PropertyInfo f, Dictionary<string, string> selectParams, object obj)
        {
            if (f.PropertyType.IsPrimitive)
            {
                selectParams.Add(f.Name, f.GetValue(obj).ToString());
            }
            else
            {
                StoreObject(f.GetValue(obj));
            }
        }
        

        private string MakeInsertString(Dictionary<string, string> insertParams,string tableName)
        {
            StringBuilder into = new StringBuilder();
            into.Append("Insert into ");
            into.Append(tableName+"(");
            
            StringBuilder values = new StringBuilder();
            values.Append(" Values(");            
            foreach (KeyValuePair<string,string> pair in insertParams)
            {
                into.Append(pair.Key + ",");
                values.Append(pair.Value+ ",");
            }
            into.Remove(into.Length - 1, 1);
            values.Remove(values.Length - 1, 1);
            into.Append(")");
            values.Append(");");

            into.Append(values);
            return into.ToString();
        }

        private string GetTableName(object obj)
        {
            return obj.GetType().Name;
        }

        private bool CheckAttribute(MemberInfo type, Type attribute)
        {
            Attribute a = Attribute.GetCustomAttributes(type).FirstOrDefault(
                atr => attribute == atr.GetType()) ;
            return a != default(Attribute);
        }

        private static bool CheckOverriding(MethodInfo baseMethod, MethodInfo inhMethod)
        {
            return baseMethod.MethodSignature() == inhMethod.MethodSignature();
        }

    }
}
