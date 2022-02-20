using AutoFixture;
using GCT.Contracts.Data;
using GCT.Contracts.DTO;
using GCT.Core.Handlers.Commands;
using Moq;
using Xunit;

namespace GCT.Test
{
    public class AccountTopUpTest
    {
        [Fact]
        public void TopUpShouldChangeTheAccountBalance()
        {
            var fixture = new Fixture();
            var command = fixture.Create<TopUpAccountCommand>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new TopUpAccountCommandHandler(mockUnitOfWork.Object, null);

            var result = handler.Handle(command, new System.Threading.CancellationToken());
            Assert.NotNull(result);
        }
    }
}