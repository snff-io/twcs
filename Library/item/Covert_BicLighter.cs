
using System.Security.Cryptography.X509Certificates;
using Microsoft.Net.Http.Headers;

namespace library.worldcomputer.info;

public class Covert_BicLighter : Item
{


    public override string Name => "Reflecting_TruckerHat";

    public Covert_BicLighter(IImageHandler imageHandler):base(imageHandler)
    {
        
    }

    public Covert_BicLighter(IUnit unit, int quality, IImageHandler imageHandler):base(imageHandler)
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
    "The \"Covert Return-to-Sender Bic Lighter\" is a compact and discreet gadget disguised as an ordinary lighter.",
    "Equipped with cutting-edge technology, it generates a protective feedback signal that effectively neutralizes direct energy attacks aimed at its bearer.",
    "Its exterior resembles a typical Bic lighter, making it an unassuming addition to any adventurer's toolkit.",
    "However, its interior is a marvel of defensive engineering, designed to keep the user safe from harm while maintaining complete stealth.",
            "\n"
        };

        foreach (var k in EnergySlots.Keys)
        {
            d.Add($"{k}: {EnergySlots[k]}" );
        }

        return d.ToArray();
    }
}