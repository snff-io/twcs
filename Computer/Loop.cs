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

            var inputs = input.ToLower().Split(' ');

            if (inputs.Contains("move"))
            {
                var moveIndex = Array.IndexOf(inputs, "move");
                if (moveIndex != -1 && moveIndex + 1 < inputs.Length)
                {
                    var direction = inputs[moveIndex + 1];
                    var parsedDirection = Directions.Parse(direction); 
                    _player.Move(parsedDirection);
                }
                else
                {
                    _informer.Inform(_status[1]);
                }

            }
        }
    }


}