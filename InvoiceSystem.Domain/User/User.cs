namespace InvoiceSystem.Domain.User;

public sealed class User
{
    private User() { }

    public string Id { get; private set; }
    public string Name { get; private set; }


    public static User Create(string name)
    {
        return new User()
        {
            Name = name
        };
    }
}