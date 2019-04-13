using System.Collections.Generic;

namespace Data.Services
{
    public interface IGoldService
    {
        ushort StartPreparingData();
        IEnumerable<string> GetAll(string dataId);
        IEnumerable<string> GetAllPrices();
    }
}
