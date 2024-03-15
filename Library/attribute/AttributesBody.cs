using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace library.worldcomputer.info;
class AttributesBody : IAttributes
{

    public int Connection { get; set; }
    public int Communication { get; set; }
    public int Environment { get; set; }
    public int Perception { get; set; }
    public int Energy { get; set; }
    public int Vibration { get; set; }
    public int Navigation { get; set; }
    public int Automation { get; set; }

    public int Get(string key)
    {
        throw new System.NotImplementedException();
    }

    public int[] Roll(string[] against)
    {
        throw new System.NotImplementedException();
    }
}