using Xunit;
using library.worldcomputer.info;

namespace test.worldcomputer.info
{
    public class Test_Pairs
    {
        [Fact]
        public void Equals_Uses_Correct_Comparison()
        {
            var p1 = new Pair(1,1,1,1,1,1,1);
            var p2 = new Pair(2,2,2,2,2,2,2);
            var p4 = new Pair(1,1,1,1,1,1,1);

            Assert.True(p1.Equal(p4));
            Assert.False(p1.Equal(p2));
            Assert.False(p2.Equal(p4));


        }

    }
}
