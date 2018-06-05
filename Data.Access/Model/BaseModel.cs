namespace Data.Access.Model
{
    public abstract class BaseModel
    {
        public string Id { get; set; }
        public string Partition => "partition";
    }
}
