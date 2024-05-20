using Xunit;
using library.worldcomputer.info;
using Moq;

namespace test.worldcomputer.info
{
    public class Test_Menu
    {
        [Fact]
        public async void Displays_Menu()
        {
            var mockSocket = new ConsoleSocket();
            
            await "TestMenu".Menu(mockSocket,
                    "Option1",
                    "Option2");



        }

    }
}
