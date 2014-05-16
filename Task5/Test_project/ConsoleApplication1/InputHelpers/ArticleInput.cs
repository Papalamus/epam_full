using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.Entities;

namespace ConsoleApplication1.InputHelpers
{
    class ArticleInput:IInputHelper<Article>
    {
        public Article MakeObject()
        {
            return new Article()
            {
                Title = ConsoleDialog.Request("Введите название статьи"),
                Value = ConsoleDialog.Request<int>("Введите стоимость", Int32.TryParse),
                ArticleCode = ConsoleDialog.Request<int>("Введите код статьи", Int32.TryParse)
            };
        }

        public object RequestId()
        {
            return ConsoleDialog.Request<int>("Введите код статьи", Int32.TryParse);
        }
    }
}
