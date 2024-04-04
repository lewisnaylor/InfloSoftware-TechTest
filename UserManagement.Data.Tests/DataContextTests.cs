using System.Linq;
using UserManagement.Models;
using System.Threading.Tasks;

namespace UserManagement.Data.Tests;

public class DataContextTests
{
    private DataContext CreateContext() => new();

    [Fact]
    public async Task GetAll_WhenNewEntityAdded_MustIncludeNewEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var context = CreateContext();

        var entity = new User
        {
            Forename = "Brand New",
            Surname = "User",
            Email = "brandnewuser@example.com"
        };
        await context.CreateAsync(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = await context.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result
            .Should().Contain(s => s.Email == entity.Email)
            .Which.Should().BeEquivalentTo(entity);
    }

    [Fact]
    public async Task GetAll_WhenDeleted_MustNotIncludeDeletedEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var context = CreateContext();
        var entities = await context.GetAll<User>();
        var entity = entities.First();
        await context.DeleteAsync<User>(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = await context.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().NotContain(s => s.Email == entity.Email);
    }

    [Fact]
    public async Task GetAll_WhenNewEntityUpdate_MustIncludeUpdatedProperty()
    {
        var context = CreateContext();

        var entity = new User
        {
            Forename = "Brand New",
            Surname = "User",
            Email = "brandnewuser@example.com"
        };
        await context.CreateAsync(entity);

        var result = await context.GetAll<User>();
        var addedUser = result.First(x => x.Id == entity.Id);
        addedUser.Forename = "Update User";

        await context.UpdateAsync(addedUser);

        var updatedResult = await context.GetAll<User>();
        var updatedUser = result.First(x => x.Id == entity.Id);
        updatedUser.Forename.Should().Be("Update User");
    }


}
