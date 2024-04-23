
using System.Security.Cryptography.X509Certificates;
using Microsoft.Net.Http.Headers;

namespace library.worldcomputer.info;

public class Reflecting_Benie : Item
{


    public override string Name => "Reflecting_TruckerHat";

    public Reflecting_TruckerHat()
    {
        
    }

    public Reflecting_TruckerHat(IUnit unit, int quality)
    {
        this.Quality = quality;

        EnergySlotsMax.Values.ForAll(i=>i = quality * 10);

    }

    public bool Use(Domain type, int magnitude)
    {
        if (EnergySlots[type] < magnitude)
            return false;
        
        EnergySlots[type] -= magnitude;
        return true;
    }

    public string[] Examine()
    {

        var d = new List<string> {
        "Crafted from durable materials and adorned with a reflective surface, this hat-like accessory is designed to repel and counter energy-based attacks with precision.",
        "Its unique construction harnesses advanced nanotechnology to return-to-sender incoming energy projectiles while also dispersing and mitigating the absorption of local energy fields.",
        "The Reflective Trucker Hat offers players a versatile defense against a wide array of energy threats, granting them the confidence to tackle even the most formidable adversaries head-on."
            "\n"
        };

        foreach (var k in EnergySlots.Keys)
        {
            d.Add($"{k}: {EnergySlots[k]}" );
        }

        return d.ToArray();
    }
}