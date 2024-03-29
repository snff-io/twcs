using System.Linq;
using library.worldcomputer.info;
public class Loop
{

    private IPlayer _player;
    private IStatus _status;
    private IInformer _informer;
    private IInput _input;

    public Loop(IStatus status, IInformer informer, IInput input)
    {
        _status = status;
        _informer = informer;
        _input = input;

    }

    public void Run(IPlayer player)
    {
        _player = player;

        while (true)
        {
            _informer.Inform($"Player is on Layer {player.Location.Layer}");
            _informer.Inform($"Player X,Y is {player.Location.X},{player.Location.Y}\r\n");

            _informer.Inform(_status["player_prompt"]);

            var input = _input.GetRawInput();

        }
    }
}
