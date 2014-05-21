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
        public Person MakeObject()
        {
            string name = ConsoleDialog.Request("Введите имя");
            string surname = ConsoleDialog.Request("Введите фамилию");
            int inn = ConsoleDialog.Request<int>("Введите ИНН", Int32.TryParse);
            return new Person()
            {
                Name = name,
                Surname = surname,
                INN = inn
            };
        }

        public object RequestId()
        {
            return ConsoleDialog.Request<int>("Введите ИНН элемента", Int32.TryParse);
        }
    }
}
