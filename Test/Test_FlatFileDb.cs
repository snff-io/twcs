using System.Diagnostics;
using Amazon.Runtime.Internal.Util;
using library.worldcomputer.info;

public class Test_FlatFileDb
{
    [Fact]
    public async void FlatFileDb_Finds_And_Deserializes()
    {
        var dal = new FlatFileDb<Heart>();

        var sw = new Stopwatch();
        sw.Start();
        var item = await dal.Get("Minghei_Sameli");
        sw.Stop();

        Assert.Equal("Minghei", item.FirstName);
        Assert.Equal("Sameli", item.LastName);
        Assert.True(sw.ElapsedMilliseconds < 150);

    }

    [Fact]
    public async void FlatFileDb_Scans_And_Deserializes()
    {
        var dal = new FlatFileDb<Heart>();

        var sw = new Stopwatch();
        sw.Start();
        var item = await dal.GetByAttr("FirstName", "Minghei");
        sw.Stop();

        Assert.Equal("Minghei", item.First().FirstName);
        Assert.Equal("Sameli", item.First().LastName);
        Assert.True(sw.ElapsedMilliseconds < 5000);
    }

    [Fact]
    public async void FlatFileDb_Gets_Random_And_Deserializes()
    {
        var dal = new FlatFileDb<Heart>();

        var sw = new Stopwatch();
        sw.Start();

        var items = await dal.GetRandomUnbound(5);

        Assert.Equal(5, items.Count());
        Assert.True(items.All(x => x.Bound == false));

        Assert.True(sw.ElapsedMilliseconds < 1000);
    }

    [Fact]
    public async void Can_Put_Body()
    {
        IDal<Body> dal = new FlatFileDb<Body>();

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
        Assert.True(body.FamilyName == "");
    }


}