using System.Diagnostics;
using Amazon.Runtime.Internal.Util;
using library.worldcomputer.info;

public class Test_ZipFileDb
{
    [Fact]
    public async void ZipFileDb_Finds_And_Deserializes()
    {
        var dal = new ZipFileDb<Heart>("/srv/filedb/filedb_heart.zip");
        
        var sw = new Stopwatch();
        sw.Start();
        var item = await dal.Get("Minghei_Sameli");
        sw.Stop();


        Assert.Equal("Minghei", item.FirstName);
        Assert.Equal("Sameli", item.LastName);
        Assert.True(sw.ElapsedMilliseconds < 1000);

    }


}