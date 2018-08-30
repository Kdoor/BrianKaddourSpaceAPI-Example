namespace SpaceSmileBrianKaddour.ApplicationCore.Entities
{
    //  TODO: Consider removal if we have something with no Id, also check into why entity would not have an Id
    //  This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
    //  Using non-generic integer types for simplicity and to ease caching logic
    public class BaseEntity
    {
        public int Id { get; set; }
    }
}
