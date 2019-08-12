using System;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IGoldService
    {
        bool IsMqttConnected { get; }
        ushort StartPreparingData();
        Dictionary<DateTime, double> GetDailyGoldPrices(string dataId);
        string GetDailyGoldPricesSerialized(string dataId);
    }
}
