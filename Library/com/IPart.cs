namespace library.worldcomputer.info;
interface IPart<T> : IUnit
{
    string FamilyName { get; set; }
    IAttributes Attributes { get; set; }
    IAbility[] Abilities { get; set; }
    public string Type { get; set; }
    public string BelongsTo { get; set; }
    public DateTime Found { get; set; }
    public bool OnLoan { get; set; }
    public string OnLoanTo { get; set; }
    public string GameName { get; set; }
    public string GivenName { get; set; }
    public string[] Intentions { get; set; }
    public string[] Memories { get; set; }  
    Pair[] PrivateKey { get; set; }
    Pair[] PublicKey {get;set;}
    Pair[] Aura { get; set; }
}


