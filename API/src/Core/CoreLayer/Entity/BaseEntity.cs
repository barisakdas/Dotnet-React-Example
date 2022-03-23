namespace CoreLayer.Entity;
public class BaseEntity
{
    public int ID { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

