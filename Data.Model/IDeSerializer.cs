namespace Data.Model
{
    public interface IDeSerializer<T>
    {
        string Serialize(T t);
        T Deserialize(string json);
    }
}
