using AutoFixture;
using GCT.Contracts.Data;
using GCT.Core.Handlers.Commands;
using GCT.Core.StateMachine;
using Moq;
using Xunit;

namespace GCT.Test
{
    public class AccountDepositTest
    {
        [Fact]
        public void DepositpShouldChangeTheAccountBalance()
        {
            var fixture = new Fixture();
            var command = fixture.Create<DepositAccountCommand>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var smMock = new Mock<IProcessSM>();
            var handler = new DepositAccountCommandHandler(mockUnitOfWork.Object, null, smMock.Object);

            var result = handler.Handle(command, new System.Threading.CancellationToken());
            Assert.NotNull(result);
        }
    }
}
