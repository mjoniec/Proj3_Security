using System;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IGoldPriceService
    {
        IEnumerable<string> GetAll();
        string GetForToday();
        DateTime GetFirstDate();
    }
}
