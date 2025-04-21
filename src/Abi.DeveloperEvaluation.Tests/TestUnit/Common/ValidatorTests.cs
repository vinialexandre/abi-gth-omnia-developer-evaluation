using Xunit;
using FluentValidation;
using Abi.DeveloperEvaluation.Common.Validation;
using System.Threading.Tasks;
using System.Linq;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Common;
public class DummyClass
{
    public string Name { get; set; } = string.Empty;
}

public class DummyClassValidator : AbstractValidator<DummyClass>
{
    public DummyClassValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("'Name' deve ser informado.");
    }
}

public class ValidatorTests
{
    [Fact]
    public async Task ValidateAsync_Should_Return_Errors_When_Invalid()
    {
        var obj = new DummyClass();
        var validator = new DummyClassValidator();

        var result = await Validator.ValidateAsync(obj, validator);

        Assert.Single(result);

        var error = result.First();
        Assert.Equal("Name", error.Error);
        Assert.Equal("'Name' deve ser informado.", error.Detail);
    }

    [Fact]
    public async Task ValidateAsync_Should_Return_Empty_When_Valid()
    {
        var obj = new DummyClass { Name = "Sr Moqueca" };
        var validator = new DummyClassValidator();

        var result = await Validator.ValidateAsync(obj, validator);

        Assert.Empty(result);
    }
}
