using Data.Model.External;

namespace Data.Repositories
{
    public interface IGoldRepository
    {
        ExternalGoldDataModel Get();
    }
}
