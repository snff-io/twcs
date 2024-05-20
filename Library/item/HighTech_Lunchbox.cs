
using System.Security.Cryptography.X509Certificates;
using Microsoft.Net.Http.Headers;

namespace library.worldcomputer.info;

public class HighTech_Lunchbox : Item
{


    public override string Name => "Hightech_Lunchbox";

    public HighTech_Lunchbox(IImageHandler imageHandler):base(imageHandler)
    {
        
    }

    public HighTech_Lunchbox(IUnit unit, int quality, IImageHandler imageHandler): base(imageHandler)
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
            "A sleek, high-tech lunchbox that captures ambient energy and stores it in specialized slots, ensuring a constant power supply.".Emph(),
            "With its modern design and intuitive interface featuring LED lights, tactile buttons, and adjustable knobs, users can easily customize settings and monitor energy levels.".Emph(),
            "Modular compartments and smart connectivity offer versatility and convenience, making the EnerGastronome 3000 the ultimate solution for on-the-go dining in the digital age.".Emph(),
            "\n"
        };

        foreach (var k in EnergySlots.Keys)
        {
            d.Add($"{k}: {EnergySlots[k]}" );
        }

        return d.ToArray();
    }
}