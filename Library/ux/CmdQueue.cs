public class CmdQueue 
{
    public Queue<string[]> Queue {get;private set;}

    public CmdQueue()
    {
        Queue = new Queue<string[]>();
    }
}