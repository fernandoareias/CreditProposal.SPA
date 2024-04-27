using System;
using Atividade02.Proposals.Application.Proposals.Commands;

namespace Atividade02.Proposals.Application.Tests.Proposals.Commands
{
    public class CreateProposalCommandTests
    {
        [Fact(DisplayName = "Should create a CreateProposalCommand")]
        public void ShouldCreateACreateProposalCommand()
            => Assert.NotNull(new CreateProposalCommand(
                Guid.NewGuid(),
                "Fernando Areias",
                "70945816022",
                "18524535000127",
                "21",
                "975418356"));


        [Fact(DisplayName = "Should create a valid CreateProposalCommand")]
        public void ShouldCreateAValidCreateProposalCommand()
           => Assert.True(new CreateProposalCommand(
               Guid.NewGuid(),
               "Fernando Areias",
               "70945816022",
               "18524535000127",
               "21",
               "975418356").IsValid());
         
        [Fact(DisplayName = "Shouln't create a valid CreateProposalCommand when aggregateId is invalid")]
        public void ShouldntCreateAValidCreateProposalCommandWhenAggregateIdIsInvalid()
         => Assert.False(new CreateProposalCommand(
             Guid.Empty,
             "Fernando Areias",
             "70945816022",
             "18524535000127",
             "21",
             "975418356").IsValid());


        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("Fernando")]
        [Theory(DisplayName = "Shouln't create a valid CreateProposalCommand when name invalid")]
        public void ShouldntCreateAValidCreateProposalCommandWhenNameInvalid(string name)
           => Assert.False(new CreateProposalCommand(
               Guid.NewGuid(),
               name,
               "70945816022",
               "18524535000127",
               "21",
               "975418356").IsValid());

        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("709.458.160-22")]
        [InlineData("709458160222")]
        [InlineData("123")]
        [InlineData("12334567890")]
        [InlineData("11111111111")]
        [Theory(DisplayName = "Shouln't create a valid CreateProposalCommand when cpf is invalid")]
        public void ShouldntCreateAValidCreateProposalCommandWhenCPFIsInvalid(string cpf)
         => Assert.False(new CreateProposalCommand(
             Guid.Empty,
             "Fernando Areias",
             cpf,
             "18524535000127",
             "21",
             "975418356").IsValid());


        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("1852453500012722222")]
        [InlineData("1112")]
        [InlineData("11111111111111")]
        [Theory(DisplayName = "Shouln't create a valid CreateProposalCommand when cnpj is invalid")]
        public void ShouldntCreateAValidCreateProposalCommandWhenCNPJIsInvalid(string cnpj)
         => Assert.False(new CreateProposalCommand(
             Guid.Empty,
             "Fernando Areias",
             "70945816022",
             cnpj,
             "21",
             "975418356").IsValid());
    }
}

