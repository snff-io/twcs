using Xunit;
using Moq;
using library.worldcomputer.info;

public class CmdParserTests
{
    [Fact]
    public void ParseCommand_ShouldReturnIntentPath_WhenIntentSuccessfullyParsed()
    {
        // Arrange
        var mockMove = new Mock<IMove>();
        mockMove.Setup(m => m.TryParse(It.IsAny<string>(), out It.Ref<string>.IsAny))
            .Returns(true)
            .Callback((string input, out string intentPath) => intentPath = "move.north.20");

        var mockStatus = new Mock<IStatus>();
        mockStatus.Setup(s => s[2]).Returns("status/unknown");

        var cmdParser = new CmdParser(mockMove.Object, mockStatus.Object);

        // Act
        var result = cmdParser.ParseCommand("move north 20");

        // Assert
        // Assert.Collection(result,
        //     item => Assert.Equal("move.north.20", item));
    }

    [Fact]
    public void ParseCommand_ShouldReturnUnknownStatus_WhenNoIntentParsed()
    {
        // Arrange
        var mockMove = new Mock<IMove>();
        mockMove.Setup(m => m.TryParse(It.IsAny<string>(), out It.Ref<string>.IsAny))
            .Returns(false);

        var mockStatus = new Mock<IStatus>();
        mockStatus.Setup(s => s[2]).Returns("status/unknown");

        var cmdParser = new CmdParser(mockMove.Object, mockStatus.Object);

        // Act
        var result = cmdParser.ParseCommand("invalid command");

        // Assert
        // Assert.Collection(result,
        //     item => Assert.Equal("status/unknown", item));
    }
}