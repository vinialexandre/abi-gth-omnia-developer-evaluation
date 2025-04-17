using Xunit;
using FluentValidation.Results;
using Abi.DeveloperEvaluation.Common.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Common;
public class ValidationResultDetailTests
{
    [Fact]
    public void Constructor_Should_Convert_ValidationResult()
    {
        var errors = new List<ValidationFailure>
        {
            new("Field", "Error message")
        };
        var result = new ValidationResult(errors);

        var detail = new ValidationResultDetail(result);

        Assert.False(detail.IsValid);
        Assert.Single(detail.Errors);
        Assert.Equal("Field", detail.Errors.First().Error);        
        Assert.Equal("Error message", detail.Errors.First().Detail);
    }

    [Fact]
    public void Default_Constructor_Should_Set_Defaults()
    {
        var detail = new ValidationResultDetail();

        Assert.False(detail.IsValid);    
        Assert.Empty(detail.Errors);     
    }
}
