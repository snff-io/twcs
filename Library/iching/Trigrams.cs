using System.Drawing;

public record Trigram
{
    public Domain Domain { get; set; }
    public int Type => (int)Domain;
    public int Frequency => TrigramMaps.Frequency[Domain];
    public Color Color => TrigramMaps.ColorMap[Domain];
    public string Character => TrigramMaps.CharacterMap[Domain];
    public string Description => TrigramMaps.Descriptions[Domain];

    public Trigram(Domain domain)
    {
        this.Domain = domain;
    }
}

public enum Domain
{
    Heaven = 1,
    Mountain = 2,
    Thunder = 3,
    Fire = 4,
    Earth = 5,
    Wind = 6,
    Swamp = 7,
    Gorge = 8,
    Emptiness = 0

}


public static class Trigrams
{
    static Dictionary<Domain, Trigram> _trigrams;
    public static Dictionary<Domain, Trigram> Dictionary
    {
        get
        {
            if (_trigrams == null)
            {
                _trigrams = new Dictionary<Domain, Trigram>
                {
                    { Domain. Heaven, new Trigram(Domain.Heaven)},
                    { Domain.Mountain, new Trigram(Domain.Mountain)},
                    { Domain.Thunder, new Trigram(Domain.Thunder)},
                    { Domain.Fire, new Trigram(Domain.Fire)},
                    { Domain.Earth, new Trigram(Domain.Earth)},
                    { Domain.   Wind, new Trigram(Domain.Wind)},
                    { Domain.  Swamp, new Trigram(Domain.Swamp)},
                    { Domain.  Gorge, new Trigram(Domain.Gorge)},
                    { Domain.  Emptiness, new Trigram(Domain.Emptiness)},
                };
            }
            return _trigrams;
        }
    }
}


public static class TrigramMaps
{


    public static string ToKey(Domain upper, Domain lower)
    {
        return $"{upper}_{lower}".ToLower();
    }

    static Dictionary<Domain, Color> _altColorMap;
    public static Dictionary<Domain, Color> AltColorMap
    {
        get
        {
            if (_altColorMap == null)
            {
                _altColorMap = new Dictionary<Domain, Color>
                {
                    {Domain.Heaven   , Color.FromKnownColor(KnownColor.Violet)},
                    {Domain.Fire     , Color.FromKnownColor(KnownColor.Orange)},
                    {Domain.Earth    , Color.FromKnownColor(KnownColor.Brown)},
                    {Domain.Swamp    , Color.FromKnownColor(KnownColor.Green)},
                    {Domain.Wind     , Color.FromKnownColor(KnownColor.Cyan)},
                    {Domain.Gorge    , Color.FromKnownColor(KnownColor.Blue)},
                    {Domain.Mountain , Color.FromKnownColor(KnownColor.Purple)},
                    {Domain.Thunder  , Color.FromKnownColor(KnownColor.Red)},
                    {Domain.Emptiness  , Color.FromKnownColor(KnownColor.Transparent)},
                };
            }
            return _altColorMap;
        }
    }


    static Dictionary<Domain, Color> _colorMap;
    public static Dictionary<Domain, Color> ColorMap
    {
        get
        {
            if (_colorMap == null)
            {
                _colorMap = new Dictionary<Domain, Color>
                {
                    {Domain.Heaven   , Color.FromArgb(1, 255, 255, 255)},
                    {Domain.Mountain , Color.FromArgb(1, 255, 0,   0)},
                    {Domain.Thunder  , Color.FromArgb(1, 255, 128, 0)},
                    {Domain.Fire     , Color.FromArgb(1, 255, 255, 0)},
                    {Domain.Earth    , Color.FromArgb(1, 128, 255, 0)},
                    {Domain.Wind     , Color.FromArgb(1, 0,   127, 255)},
                    {Domain.Swamp    , Color.FromArgb(1, 127, 0,   255)},
                    {Domain.Gorge    , Color.FromArgb(1, 255, 0,   255)},
                    {Domain.Emptiness    , Color.FromArgb(0, 0, 0,   0)},
                };
            }
            return _colorMap;
        }
    }

    static Dictionary<Domain, int> _frequency;
    public static Dictionary<Domain, int> Frequency
    {
        get
        {
            if (_frequency == null)
            {
                _frequency = new Dictionary<Domain, int>
                {
                    {Domain.Heaven   , 46},
                    {Domain.Mountain , 43},
                    {Domain.Thunder  , 40},
                    {Domain.Fire     , 27},
                    {Domain.Earth    , 24},
                    {Domain.Wind     , 21},
                    {Domain.Swamp    , 18},
                    {Domain.Gorge    , 15},
                    {Domain.Emptiness , 0},
                };
            }

            return _frequency;
        }
    }

    static Dictionary<Domain, string> _characterMap;
    public static Dictionary<Domain, string> CharacterMap
    {
        get
        {
            if (_characterMap == null)
            {
                _characterMap = new Dictionary<Domain, string>
                {
                    {Domain.Heaven,     "☰"},
                    {Domain.Mountain,   "☶"},
                    {Domain.Thunder,    "☲"},
                    {Domain.Fire,       "☱"},
                    {Domain.Earth,      "☷"},
                    {Domain.Wind,       "☵"},
                    {Domain.Swamp,      "☴"},
                    {Domain.Gorge,      "☳"},
                    {Domain.Emptiness,  ""},
                };
            }
            return _characterMap;
        }
    }


    static Dictionary<Domain, string> _descriptions;
    public static Dictionary<Domain, string> Descriptions
    {
        get
        {
            if (_descriptions == null)
            {
                _descriptions = new Dictionary<Domain, string>()
                {
                    {
                        Domain.Heaven,
                        @"a celestial scene, where the boundless sky meets the earthly realm
                        , enveloping the viewer in a sense of tranquility and wonder, offering a glimpse of infinite possibilities and celestial beauty."
                    },
                    {
                        Domain.Mountain,
                        @"a breathtaking panorama of rugged peaks
                        , inviting contemplation and awe as one takes in the majestic beauty of nature's grandeur."
                    },
                    {
                        Domain.Thunder,
                        @"the resounding echo of thunder reverberating through a vast and open landscape
                        , igniting the senses with its power and intensity, while also offering a dramatic and awe-inspiring spectacle against the backdrop of the horizon."
                    },
                    {
                        Domain.Fire,
                        @"fire illuminates the landscape with its fierce and primal energy
                        , casting dancing shadows and painting the horizon with hues of orange and red, evoking both awe and respect for the raw power of nature's flames."
                    },
                    {
                        Domain.Earth,
                        @"ancient tapestry of the land, where rolling hills
                        , rugged mountains, and verdant valleys converge, offering a timeless view that speaks of strength, stability, and the enduring beauty of the natural world."
                    },
                    {
                        Domain.Wind,
                        @"sweeping gusts that traverse expansive landscapes
                        , carrying whispers of distant places and stirring the senses with the promise of adventure and the gentle caress of nature's breath."
                    },
                    {
                        Domain.Swamp,
                        @"a mysterious landscape veiled in mist
                        , where tangled vegetation and murky waters converge, creating an eerie yet captivating scene that teems with life and whispers ancient secrets."
                    },
                    {
                        Domain.Gorge,
                        @"a breathtaking spectacle of sheer cliffs and winding waterways
                        , where the rugged terrain plunges into depths below, creating a dramatic and awe-inspiring scene that evokes both wonder and reverence for the forces of nature."
                    },
                    {
                        Domain.Emptiness,
                        @"Silence echoes through the void, defining an infinite vista of emptiness."
                    }
                };
            }

            return _descriptions;
        }
    }
}