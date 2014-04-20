using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.Entities;

namespace ConsoleApplication1.InputHelpers
{
    class PersonInput : IInputHelper<Person>
    {
        public Person makeObject()
        {
            return new Person()
            {
                Name = ConsoleDialog.Request("Введите имя"),
                Surname = ConsoleDialog.Request("Введите фамилию"),
                INN = ConsoleDialog.Request<int>("Введите ИНН", Int32.TryParse)
            };
        }

        public object requestID()
        {
            return ConsoleDialog.Request<int>("Введите ИНН элемента", Int32.TryParse);
        }
    }
}
