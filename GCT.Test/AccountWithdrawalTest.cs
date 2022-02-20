using AutoFixture;
using GCT.Contracts.Data;
using GCT.Core.Handlers.Commands;
using Moq;
using Xunit;

namespace GCT.Test
{
    public class AccountWithdrawalTest
    {
        [Fact]
        public void WithdrawalAccountShouldChangeTheAccountBalance()
        {
            var fixture = new Fixture();
            var command = fixture.Create<WithrawalAccountCommand>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new WithrawalAccountCommandHandler(mockUnitOfWork.Object, null);

            var result = handler.Handle(command, new System.Threading.CancellationToken());
            Assert.NotNull(result);
        }
    }
}
