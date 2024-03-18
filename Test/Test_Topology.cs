using Xunit;
using library.worldcomputer.info;

namespace test.worldcomputer.info
{
    public class Test_Topology
    {
        [Fact]
        public void InitializeGrid_ReturnsCorrectResult()
        {
            var grid = new Grid();
            grid.InitializeGrid();

            Assert.True(grid.Layers.Length > 0);
        }

        [Fact]
        public void Grid_Pairs_Are_Deterministic()
        {
            var grid = new Grid();
            grid.InitializeGrid();

            var coresample1 = grid.Layers[0][190][190];

            var grid2 = new Grid();
            grid2.InitializeGrid();

            var coresample2 = grid.Layers[0][190][190];

            Assert.True(coresample1.Equal(coresample2));
        }

        [Fact]
        public void Grid_Returns_PairGroup_Given_LXY()
        {
            var grid = new Grid();
            grid.InitializeGrid();
            var pair = grid.Layers[0][190][190];

            var pairGroup = new PairGroup(grid, grid.Layers[0][190][190]);

            Assert.True(pairGroup.Current.Equal(pair));
            Assert.True(pairGroup.Up.Equals(Pair.None));
            Assert.True(pairGroup.Down.Equals(Pair.None));
            Assert.False(pairGroup.North.Equals(Pair.None));
            Assert.False(pairGroup.South.Equals(Pair.None));
            Assert.False(pairGroup.East.Equals(Pair.None));
            Assert.False(pairGroup.West.Equals(Pair.None));

        }

        [Fact]
        public void Grid_Returns_All_Pairs()
        {
            var grid = new Grid();
            grid.InitializeGrid();

            var res = grid.GetAllPairs();

            Assert.Equal(Math.Pow(379,3), res.Count());
        }
    }
}
