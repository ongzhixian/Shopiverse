using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

using NSubstitute;

using Xunit;

namespace Shopiverse.WebApi.Controllers.Tests;

public class WeatherForecastControllerTests
{
    [Fact()]
    public void WeatherForecastControllerTest()
    {
        var logger = NSubstitute.Substitute.For<ILogger<WeatherForecastController>>();

        var controller = new WeatherForecastController(logger);

        Xunit.Assert.NotNull(controller);
    }

    [Fact()]
    public void GetTest()
    {
        var logger = NSubstitute.Substitute.For<ILogger<WeatherForecastController>>();

        var controller = new WeatherForecastController(logger);

        var forecasts = controller.Get();

        logger.Received().Log(
            LogLevel.Information,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString() == "Get called"),
            null,
            Arg.Any<Func<object, Exception?, string>>());
    }
}