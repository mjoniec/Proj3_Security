namespace Data.Model
{
    public interface ISerializer<T>
    {
        string Serialize(T t);
        T Deserialize(string json);
    }
}
