using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;

namespace MyMvcApp.Tests;

public class UserControllerTests
{
    public UserControllerTests()
    {
        UserController.ResetUserList();
    }

    [Fact]
    public void Index_ReturnsAllUsers_WhenNoSearchTermProvided()
    {
        var controller = new UserController();

        controller.Create(new User { Name = "Alice", Email = "alice@example.com" });
        controller.Create(new User { Name = "Bob", Email = "bob@example.com" });

        var result = controller.Index(null) as ViewResult;
        var model = Assert.IsAssignableFrom<IEnumerable<User>>(result?.Model);

        Assert.Equal(2, model.Count());
    }

    [Fact]
    public void Index_FiltersUsers_ByName()
    {
        var controller = new UserController();

        controller.Create(new User { Name = "Charlie", Email = "charlie@example.com" });
        controller.Create(new User { Name = "Dana", Email = "dana@example.com" });
        controller.Create(new User { Name = "Eve", Email = "eve@sample.com" });

        var result = controller.Index("Dana") as ViewResult;
        var model = Assert.IsAssignableFrom<IEnumerable<User>>(result?.Model);

        Assert.Single(model);
        Assert.Equal("Dana", model.First().Name);
    }

    [Fact]
    public void Index_FiltersUsers_ByEmail()
    {
        var controller = new UserController();

        controller.Create(new User { Name = "Frank", Email = "frank@example.com" });
        controller.Create(new User { Name = "Grace", Email = "grace@sample.com" });

        var result = controller.Index("example.com") as ViewResult;
        var model = Assert.IsAssignableFrom<IEnumerable<User>>(result?.Model);

        Assert.Single(model);
        Assert.Equal("Frank", model.First().Name);
    }

    [Fact]
    public void Index_ReturnsNoUsers_WhenSearchTermDoesNotMatch()
    {
        var controller = new UserController();

        controller.Create(new User { Name = "Hank", Email = "hank@example.com" });

        var result = controller.Index("xyz") as ViewResult;
        var model = Assert.IsAssignableFrom<IEnumerable<User>>(result?.Model);

        Assert.Empty(model);
    }
}