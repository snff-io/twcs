using library.worldcomputer.info;
using Microsoft.AspNetCore.Identity;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.Sequence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public class CmdParser:ICmdParser
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


    public List<string> ParseCommand(string input)
    {
        var result = new List<string>();
        foreach(var intent in _intents) {
            string intentPath = "";
            if ( intent.TryParse(input, out intentPath))
            {
                result.Add(intentPath);
            }
        }

        if (!result.Any()) {
            result.Add(_status[2]);
        }

        return result;

    }



}

