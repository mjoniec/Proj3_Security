using System;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IGoldService
    {
        IDictionary<DateTime, double> GetAllPriceData();
        string GetNewestPrice();
        DateTime GetOldestDay();
    }
}
