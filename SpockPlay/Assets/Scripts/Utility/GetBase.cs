public abstract class GetBase<T> where T : new()
{
    public static T Get()
    {
        return new T();
    }
}
