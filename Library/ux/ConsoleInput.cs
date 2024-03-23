using System;

public class ConsoleInput : IInput

{

    public string GetRawInput()
    {
        return Console.ReadLine()??"";
    }

}