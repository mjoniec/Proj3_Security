using Data.Model.Common;

namespace Data.Services
{
    public interface IGoldService
    {
        ushort StartPreparingData();
        GoldPrices GetDailyGoldPrices(ushort requestId);
    }
}
