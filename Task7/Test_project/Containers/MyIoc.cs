using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Containers
{
    class MyIoc:IContainer
    {
        private Dictionary<Type, Type> _container;

        public MyIoc()
        {
            _container = new Dictionary<Type, Type>();
        }

        public T Resolve<T>(params object[] args) where T : class
        {
            Type to = typeof(T);
            return (T) Create(to,args);
        }
       
        public T Resolve<T>() where T : class
        {
            Type to = typeof(T);
            return (T)Create(to);
        }


        private void InjectFieldsPropeties(object target)
        {
            Type targetType = target.GetType();
            var fields = targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var properties = targetType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            
            foreach (var fieldInfo in fields)
            {
                var fieldType = fieldInfo.FieldType;
                Type injectedetType;
                bool needInject = _container.TryGetValue(fieldType, out injectedetType);
                if (needInject)
                {
                    fieldInfo.SetValue(target, Create(injectedetType));
                }
            }
            
            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                Type injectedetType;
                bool needInject = _container.TryGetValue(propertyType, out injectedetType);
                if (needInject)
                {
                    propertyInfo.SetValue(target, Create(injectedetType));
                }
            }
        }

        public void Register<TFrom, TTo>()
            where TTo : class
            where TFrom : TTo
        {
            _container.Add(typeof(TTo), typeof(TFrom));
        }

        private object Create(Type t,params object[] args)
        {
            object result = Activator.CreateInstance(t, args);
            InjectFieldsPropeties(result);
            return result;
        }
    }
}
