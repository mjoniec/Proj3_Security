using System.Collections.Generic;

namespace Data.Services
{
    public interface IGoldService
    {
        ushort StartPreparingData();
        string GetNewestPrice(string dataId);
        IEnumerable<string> GetAll(string dataId);
        IEnumerable<string> GetAllPrices();
    }
}
