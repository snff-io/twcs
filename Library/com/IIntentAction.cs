namespace library.worldcomputer.info;

public interface IIntentAction
{
    public string Intent {get;}

    public Task<IntentActionResult> Exec(string intentPath, IUnit unit, Socket socket);

}
