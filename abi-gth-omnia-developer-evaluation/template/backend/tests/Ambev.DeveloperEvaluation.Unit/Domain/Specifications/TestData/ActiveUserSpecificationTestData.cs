using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation for ActiveUserSpecification tests
/// to ensure consistency across test cases.
/// </summary>
public static class ActiveUserSpecificationTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Email (valid format)
    /// - Password (meeting complexity requirements)
    /// - FirstName
    /// - LastName
    /// - Phone (Brazilian format)
    /// - Role (User)
    /// Status is not set here as it's the main test parameter
    /// </summary>
    private static readonly Faker<User> userFaker = new Faker<User>()
        .CustomInstantiator(f => new User {
            Email = f.Internet.Email(),
            Password = $"Test@{f.Random.Number(100, 999)}",
            Username = f.Name.FirstName(),
            Status = f.PickRandom<UserStatus>(),
            Phone = $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}",
            Role = f.PickRandom<UserRole> ()
        });

    /// <summary>
    /// Generates a valid User entity with the specified status.
    /// </summary>
    /// <param name="status">The UserStatus to set for the generated user.</param>
    /// <returns>A valid User entity with randomly generated data and specified status.</returns>
    public static User GenerateUser(UserStatus status)
    {
        var user = userFaker.Generate();
        user.Status = status;
        return user;
    }
}
