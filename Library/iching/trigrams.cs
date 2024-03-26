using System.Drawing;

public record Trigram
{
    public string Domain { get; set; }
    public int Type { get; set; }
    public int Frequency { get; set; }
    public Color Color { get; set; }
    public string Description
    {
        get
        {
            return TrigramMaps.Descriptions[Domain];
        }
    }
}

public static class TrigramMaps
{
    public static List<Trigram> Trigrams
    {
        get
        {

        }
    }


}

public static class TrigramMaps
{

    static Dictionary<string, string> _descriptions;
    public static Dictionary<string, string> Descriptions
    {
        get
        {
            if (_descriptions == null)
            {
                _descriptions = new Dictionary<string, string>()
                {
                    {
                        "heaven",
                        @"a celestial scene, where the boundless sky meets 
                        the earthly realm, enveloping the viewer in a sense
                        of tranquility and wonder, offering a glimpse of 
                        infinite possibilities and celestial beauty."
                    },
                    {
                        "mountain",
                        @"a breathtaking panorama of rugged peaks, 
                        inviting contemplation and awe as 
                        one takes in the majestic beauty of nature's grandeur."
                    },
                    {
                        "thunder",
                        @"the resounding echo of thunder reverberating through 
                        a vast and open landscape, igniting the senses with its 
                        power and intensity, while also offering a dramatic 
                        and awe-inspiring spectacle against the backdrop of the horizon."

                    },
                    {
                        "fire",
                        @"fire illuminates the landscape with its fierce and primal energy,
                        casting dancing shadows and painting the horizon with hues 
                        of orange and red, evoking both awe and respect for the raw power
                        of nature's flames."
                    },
                    {
                        "earth",
                        @"ancient tapestry of the land, where rolling hills, 
                        rugged mountains, and verdant valleys converge, 
                        offering a timeless view that speaks of strength, 
                        stability, and the enduring beauty of the natural world."
                    },
                    {
                        "wind",
                        @"sweeping gusts that traverse expansive landscapes, 
                        carrying whispers of distant places and stirring 
                        the senses with the promise of adventure and 
                        the gentle caress of nature's breath."
                    },
                    {
                        "swamp",
                        @"a mysterious landscape veiled in mist, 
                        where tangled vegetation and murky waters converge, 
                        creating an eerie yet captivating scene that 
                        teems with life and whispers ancient secrets."
                    },
                    {
                        "gorge",
                        @"a breathtaking spectacle of sheer cliffs and
                        winding waterways, where the rugged terrain plunges
                        into depths below, creating a dramatic and 
                        awe-inspiring scene that evokes both wonder and 
                        reverence for the forces of nature."
                    }
                };
            }

            return _descriptions;
        }
    }



}



