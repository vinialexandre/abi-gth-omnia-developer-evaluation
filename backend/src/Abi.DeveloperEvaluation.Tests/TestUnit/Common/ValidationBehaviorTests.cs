using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abi.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;
using Moq;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Common
{
    public class ValidationBehaviorTests
    {
        public class DummyRequest : IRequest<string>
        {
            public string Nome { get; set; } = "";
        }

        public class DummyValidator : AbstractValidator<DummyRequest>
        {
            public DummyValidator()
            {
                RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome obrigatório");
            }
        }

        [Fact]
        public async Task Should_Invoke_Next_When_Valid()
        {
            var request = new DummyRequest { Nome = "Sr Moqueca" };
            var validators = new List<IValidator<DummyRequest>> { new DummyValidator() };
            var behavior = new ValidationBehavior<DummyRequest, string>(validators);
            var next = new Mock<RequestHandlerDelegate<string>>();
            next.Setup(n => n()).ReturnsAsync("ok");

            var result = await behavior.Handle(request, next.Object, default);

            Assert.Equal("ok", result);
            next.Verify(n => n(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Invalid()
        {
            var request = new DummyRequest { Nome = "" };
            var validators = new List<IValidator<DummyRequest>> { new DummyValidator() };
            var behavior = new ValidationBehavior<DummyRequest, string>(validators);
            var next = new Mock<RequestHandlerDelegate<string>>();

            await Assert.ThrowsAsync<ValidationException>(() => behavior.Handle(request, next.Object, default));
            next.Verify(n => n(), Times.Never);
        }
    }
}
