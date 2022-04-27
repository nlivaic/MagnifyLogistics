namespace Common.Base;
public abstract class BaseEntity<TId>
{
    public TId Id { get; private set; }

    public BaseEntity(TId id)
    {
        Id = id;
    }
}
