public class Test_AnsiHelper
{
    [Fact]
    public void PairAnsi_WhenGivenEqualLengthInputs_ShouldReturnPairs()
    {
        // Arrange
        string left = "1\n2\n3";
        string right = "A\nB\nC";

        // Act
        IEnumerable<string> pairs = Ansi.PairAnsi(left, right);

        // Assert
        Assert.Equal(3, pairs.Count());

        Assert.Equal("1 A", pairs.ElementAt(0));
        Assert.Equal("2 B", pairs.ElementAt(1));
        Assert.Equal("3 C", pairs.ElementAt(2));
    }

    [Fact]
    public void PairAnsi_WhenGivenDifferentLengthInputs_ShouldReturnPairsUpToShortestLength()
    {
        // Arrange
        string left = "1\n2\n3";
        string right = "A\nB";

        // Act
        IEnumerable<string> pairs = Ansi.PairAnsi(left, right);

        // Assert
        Assert.Equal(2, pairs.Count());

        Assert.Equal("1 A", pairs.ElementAt(0));
        Assert.Equal("2 B", pairs.ElementAt(1));
    }

    [Fact]
    public void PairAnsi_WhenGivenEmptyInputs_ShouldReturnEmptyCollection()
    {
        // Arrange
        string left = "";
        string right = "";

        // Act
        IEnumerable<string> pairs = Ansi.PairAnsi(left, right);

        // Assert
        Assert.Empty(pairs);
    }

    [Fact]
    public void QRCode_Created()
    {
        var result = Ansi.GenerateQRCode("die scum");


        Console.WriteLine(result);

        Assert.True(result != null && result != String.Empty);


    }
}