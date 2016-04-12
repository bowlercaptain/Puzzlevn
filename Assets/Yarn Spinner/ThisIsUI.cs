using UnityEngine;
using System.Collections;
using Yarn;
using Yarn.Unity;
using System;
using UnityEngine.UI;

public class ThisIsUI : DialogueUIBehaviour {

    public Text output;
    public Text charName;

    PortraitDisplay portrait;

    // Display a line.
    public override IEnumerator RunLine(Yarn.Line line) {
        if (line.text.Contains(":"))
        {
            string title = line.text.Substring(0, line.text.IndexOf(':'));
            string text = line.text.Substring(line.text.IndexOf(':') + 1);
            if (title.Split('.').Length > 1)
            {
                ShowPortrait(title.Split('.')[1], title.Split('.')[0]);
            }
            else
            {
                ShowPortrait(character: title);
            }
            output.text = text;
        } else
        {
            output.text = line.text;
        }
        //break by :
        //build command and send to RunCommand to change emotions if necessary. Then Apply Shading?
        Debug.Log(line.text);
        
        yield return null;
        //yield break;

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }

    // Display the options, and call the optionChooser when done.
    public override IEnumerator RunOptions(Yarn.Options optionsCollection,
                                            Yarn.OptionChooser optionChooser)
    {
        foreach (string option in optionsCollection.options)
        {
            Debug.Log(option);
        }
        optionChooser(0);
        yield return null;
        
    }

    // Perform some game-specific command.
    public override IEnumerator RunCommand(Yarn.Command command) {
        Debug.Log("Run command: "+command.text);
        //"show <character> <emotion>"
        //"move <character> <slot>"
        //move <slot> <slot>
        //animate <character> <animation>
        //animate <slot> <animation>
        //shade <character/slot>
        //light <character/slot>

        
        
        yield return null;
    }

    // The node has ended.
    public override IEnumerator NodeComplete(string nextNode)
    {
        // Default implementation does nothing.
        yield break;
    }

    // The conversation has ended.
    public override IEnumerator DialogueComplete()
    {
        Debug.Log("complete");
        // Default implementation does nothing.
        yield break;
    }

    // A conversation has started.
    public override IEnumerator DialogueStarted()
    {
        Debug.Log("started.");
        // Default implementation does nothing.
        yield break;
    }


    public DialogueCharacter defaultFormat;
    public void ShowPortrait(string emotion = "default", string character = null, int slot = -1) //this will be from commands
    {
        DialogueCharacter format = null;
        if (character != null)
        {
            format = Resources.Load<DialogueCharacter>(character + "/" + emotion);
        }
        if(format == null)
        {
            format = defaultFormat;
        }
        if (format != null)
        {
            charName.text = format.getName();
            output.color = format.textColor;
        } else
        {
            Debug.LogError("NO FORMAT PUT DEBUG INFO HERE");
        }
    }

}
