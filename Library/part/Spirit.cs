namespace library.worldcomputer.info;
class Spirit : IUnit, IPart<Spirit>
{
    public string _id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FamilyName { get; set; }
    public IAttributes Attributes { get; set; }
    public IAbility[] Abilities { get; set; }
    public Pair[] Key { get; set; }
    public Pair[] Aura { get; set; }
    public string Type { get; set; }
    public string BelongsTo { get; set; }
    public DateTime Found { get; set; }
    public bool OnLoan { get; set; }
    public string OnLoanTo { get; set; }
    public string GameName { get; set; }
    public string GivenName { get; set; }
    public string[] Intentions { get; set; }
    public string[] Memories { get; set; }
    public Pair[] PrivateKey { get; set; }
    public Pair[] PublicKey { get; set; }

    public string DisplayType()
    {
        throw new NotImplementedException();
    }

    public string GetHash(int length)
    {
        throw new NotImplementedException();
    }
}