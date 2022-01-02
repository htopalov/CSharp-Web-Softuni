using System.Collections.Generic;
using CustomHttpWebServer.App.Data.Models;

namespace CustomHttpWebServer.App.Data
{
    public class MyDbContex : IData
    {
        public MyDbContex()
        {
            this.Cats = new List<Cat>
            {
                new Cat{Id = 1, Name = "Cat1", Age = 1},
                new Cat{Id = 2, Name = "Cat2", Age = 5}
            };
        }

        public IEnumerable<Cat> Cats { get; }
    }
}
