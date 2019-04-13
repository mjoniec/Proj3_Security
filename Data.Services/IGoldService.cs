using System;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IGoldService
    {
        ushort StartPreparingData();
        Dictionary<DateTime, double> GetDailyGoldPrices(string dataId);
        Dictionary<DateTime, double> GetDailyGoldPricesFromDatabase();
    }
}
