using System;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IGoldService
    {
        ushort GetDataPrepared();
        string GetNewestPrice(string dataId);
        //IDictionary<DateTime, double> GetAllGoldPriceData(ushort dataId);
    }
}
