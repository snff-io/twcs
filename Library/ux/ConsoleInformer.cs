public class ConsoleInformer: IInformer
{
    public void Inform(string information)
    {
        System.Console.WriteLine(information);
    }
}