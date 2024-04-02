using library.worldcomputer.info;

public class CmdParser : ICmdParser
{

    List<IIntent> _intents;
    private IStatus _status;

    public CmdParser(IMove mover, IStatus status)
    {
        _intents = new List<IIntent>(){
            mover
        };

        _status = status;
    }

    public async Task<string> ParseCommand(string input)
    {
        foreach (var intent in _intents)
        {
            var tpr = await intent.TryParse(input);
            if (tpr.Success)
            {
                return tpr.IntentPath;
            }
        }

        return _status[2];
    }
}

