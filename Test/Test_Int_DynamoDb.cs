
using Xunit;
using library.worldcomputer.info;
using Microsoft.OpenApi.Expressions;

namespace test.worldcomputer.info
{
    public class Test_Int_DynamoDb
    {
        //[Fact]
        public async void Can_Get_Body()
        {
            IDal<Body> dal = new DynamoDb<Body>();

            var body = await dal.Get("Mouaz_Coretchii");

            Assert.True(body.FirstName == "Mouaz");
            Assert.True(body.LastName == "Coretchii");

        }

        //[Fact]
        public async void Can_Put_Body()
        {
            IDal<Body> dal = new DynamoDb<Body>();

            var body = await dal.Get("Mouaz_Coretchii");

            Assert.True(body.FirstName == "Mouaz");
            Assert.True(body.LastName == "Coretchii");

            body.FamilyName = "Testino";

            await dal.Put(body);

            body = null;

            body = await dal.Get("Mouaz_Coretchii");

            Assert.True(body.FirstName == "Mouaz");
            Assert.True(body.LastName == "Coretchii");
            Assert.True(body.FamilyName == "Testino");

            body.FamilyName = "";

            await dal.Put(body);

            body = await dal.Get("Mouaz_Coretchii");

            Assert.True(body.FirstName == "Mouaz");
            Assert.True(body.LastName == "Coretchii");
            Assert.True(body.FamilyName == null);
        }

        //[Fact]
        public async Task Can_Choose_5_Unbound()
        {
            IDal<Body> dal = new DynamoDb<Body>();

            var names = await dal.GetRandomUnbound(5);

            Assert.True(names.Count() == 5);
            Assert.True(!names.Any(x=>x.LastName == "Coretchii"));
        }

    }
}