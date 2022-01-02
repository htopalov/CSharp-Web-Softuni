using System.Collections.Generic;
using CustomHttpWebServer.App.Data.Models;

namespace CustomHttpWebServer.App.Data
{
    public interface IData
    {
        IEnumerable<Cat> Cats { get; }
    }
}
