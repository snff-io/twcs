namespace library.worldcomputer.info;
interface IUnit : IHash
{
    string _id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string DisplayType();

    
}