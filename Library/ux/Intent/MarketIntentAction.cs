using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using library.worldcomputer.info;
using Microsoft.AspNetCore.Razor.TagHelpers;

public class MarketIntentAction : IIntentAction
{
    private IWordResolver _wordResolver;

    public string Intent => "market";

    List<string> _operations = new List<string>
    {
        "buy",
        "sell",
        "check",
        "withdraw",
    };

    public MarketIntentAction(IWordResolver wordResolver)
    {
        _wordResolver = wordResolver;
    }

    public async Task<IntentActionResult> Exec(string intentPath, IUnit unit, Socket socket)
    {
        var args = intentPath.Split('.');

        if (args[0] != "market")
        {
            throw new InvalidOperationException("incorrect intent.");
        }

        await "Market Terminal".Emph().Send(socket);
        int mainMenuChoice = 0;


        if (args.Length > 1)
        {
            mainMenuChoice = _operations.IndexOf(args[1]);
        }
        else
        {
            mainMenuChoice = await MainMenu(args, socket);
        }

        do
        {
            switch (mainMenuChoice)
            {
                case 0:
                    await BuyMenu(args, socket);
                    break;
                case 1:
                    await SellMenu(args, socket);
                    break;
                case 2:
                    await CheckMenu(args, socket);
                    break;
                case 3:
                    await WithdrawMenu(args, socket);
                    break;
                case -1:
                    return new IntentActionResult { Success = true };
            }

            mainMenuChoice = await MainMenu(args, socket);
        }
        while (mainMenuChoice != -1);

        return new IntentActionResult() { Success = true };

    }

    async Task<int> MainMenu(string[] args, Socket socket)
    {
        await "Market Menu".Emph().Send(socket);
        await "-----------".Text().Send(socket);
        for (var i = 0; i < _operations.Count(); i++)
        {
            await $"{i + 1}. {_operations[i]}".Option().Send(socket);
        }

        var result = await socket.PromptForNumber($"Your Choice? [1-{_operations.Count()}]", _operations.Count, 1);

        return result;

    }

    async Task<int> BuyMenu(string[] args, Socket socket)
    {
        await "Buy Menu".Send(socket);
        return 0;
    }

    async Task<int> SellMenu(string[] args, Socket socket)
    {
        await "Sell Menu".Send(socket);
        return 0;
    }

    async Task<int> CheckMenu(string[] args, Socket socket)
    {
        await "Check Menu".Send(socket);
        return 0;
    }

    async Task<int> WithdrawMenu(string[] args, Socket socket)
    {
        await "Buy Menu".Send(socket);
        return 0;
    }


}