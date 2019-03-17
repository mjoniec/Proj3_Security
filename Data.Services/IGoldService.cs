namespace Data.Services
{
    public interface IGoldService
    {
        ushort GetDataPrepared();
        string GetNewestPrice(string dataId);
    }
}
