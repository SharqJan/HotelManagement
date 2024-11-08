using System.Collections.Generic;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

// Sample data for demonstration
public static class UserData
{
    public static List<User> GetSampleUsers()
    {
        return new List<User>
{
    new User { Id = 1, Name = "John Doe", Age = 30 },
    new User { Id = 2, Name = "Jane Smith", Age = 25 },
    new User { Id = 3, Name = "Sam Johnson", Age = 35 },
    new User { Id = 4, Name = "Emily Davis", Age = 28 },
    new User { Id = 5, Name = "Michael Brown", Age = 40 },
    new User { Id = 6, Name = "Sarah Wilson", Age = 32 },
    new User { Id = 7, Name = "David Lee", Age = 27 },
    new User { Id = 8, Name = "Laura White", Age = 22 },
    new User { Id = 9, Name = "Daniel Harris", Age = 29 },
    new User { Id = 10, Name = "Sophia Clark", Age = 31 }
};

    }
}