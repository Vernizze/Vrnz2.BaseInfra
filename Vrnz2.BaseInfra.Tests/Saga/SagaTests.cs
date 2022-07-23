using Serilog;
using Xunit;
using Vrnz2.BaseInfra.Saga;
using FluentAssertions;
using Vrnz2.BaseInfra.Logs;
using Microsoft.Extensions.DependencyInjection;

namespace Vrnz2.BaseInfra.Tests.Saga
{
    public class SagaTests
    {
        [Fact]
        public void GetSaga_When_GetSagaHandlerFromASpecificScope_Should_BeDifferentOfAnAnotherTakedFromOtherScope()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection().AddLogs(out ILogger _);

            // Act
            var sagaHandler = services.BuildServiceProvider().GetService<ISagaHandler>();

            var saga01 = sagaHandler.GetSaga;
            var saga02 = sagaHandler.GetSaga;

            var sagaHandler2 = services.BuildServiceProvider().GetService<ISagaHandler>();

            var saga03 = sagaHandler2.GetSaga;
            var saga04 = sagaHandler2.GetSaga;

            var saga05 = sagaHandler.GetSaga;
            var saga06 = sagaHandler2.GetSaga;

            // Assert
            saga01.Should().Be(saga02);
            saga03.Should().Be(saga04);
            saga05.Should().Be(saga01);
            saga06.Should().Be(saga03);

            saga03.Should().NotBe(saga01);
            saga05.Should().NotBe(saga06);
        }
    }
}
