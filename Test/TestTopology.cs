using Xunit;
using library.worldcomputer.info;

namespace test.worldcomputer.info
{
    public class TestTopology
    {
        [Fact]
        public void InitializeGrid_ReturnsCorrectResult()
        {
            var grid = new Grid();
            grid.InitializeGrid();

            Assert.True(grid.Layers.Length > 0);
        }
    }
}
