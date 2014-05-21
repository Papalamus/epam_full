using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal delegate void OptionHandler();
    class ConsoleDialog
    {
        private Dictionary<int, Option> _options;
        private int _counter;
        private string _message;

        public ConsoleDialog(string message)
        {
            _message = message;
            _options = new Dictionary<int, Option>();
        }

        
        public void Add(string optionDescr,OptionHandler processor)
        {
            _options.Add(_counter++,new Option(optionDescr,processor));
        }

        public void Run()
        {
            Choise(_message);
        }


        public static string Request(string message)
        {
            TryParseHandler<string> foo = (string s, out string result) =>
            {
                result = s;
                return true;
            };
            return Request(message, foo);
        }

        public static T Request<T>(string message, TryParseHandler<T> handler)
        {
            bool isCorrect;
            T result;
            do
            {
                Console.WriteLine(message);
                string responce = Console.ReadLine();
                isCorrect = handler(responce, out result);
                if (!isCorrect)
                {
                    Console.WriteLine(GetErrorMessage());
                }
            }
            while (!isCorrect);
            return result;
        }

        private static string GetErrorMessage()
        {
            return "Повторите ввод";
        }

        private void Choise(string headerMsg)
        {
            StringBuilder sb = new StringBuilder(headerMsg);
            foreach (KeyValuePair<int, Option> keyValuePair in _options)
            {
                sb.Append(keyValuePair.Key).Append(" - ");
                sb.AppendLine(keyValuePair.Value.Message);
            }
            bool isExist;
            Option chosenOption;
            do
            {
                int chosenNumber = Request<int>(sb.ToString(), Int32.TryParse);
                isExist = _options.TryGetValue(chosenNumber, out chosenOption);

            } while (!isExist);
            chosenOption.Processor();
        }
        class Option
        {
            public string Message { get; set; }
            public OptionHandler Processor { get; set; }

            public Option(string message, OptionHandler processor)
            {
                Message = message;
                this.Processor = processor;
            }
        }

    }
}
