using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers
{
    interface IInterface
    {
        void doSmth();
    }
    class IRealisation : IInterface
    {
        public void doSmth()
        {
            Console.WriteLine("Realisation");
        }
    }

    public class Worker
    {
        private IInterface tmp;

        public void Work()
        {
            tmp.doSmth();
        }
    }

    class Programm
    {
        static void Main(string[] args)
        {
            IContainer ioc = new MyIoc();
            ioc.Register<IRealisation,IInterface>();
            Worker work = ioc.Resolve<Worker>();
            work.Work();
            Console.ReadKey();
        }
    }
}
