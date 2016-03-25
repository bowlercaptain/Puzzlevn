using UnityEngine;
using System.Collections;
using Yarn;
using Yarn.Unity;
using System;

public class ThisIsUI : DialogueUIBehaviour {
    // A conversation has started.
    public override IEnumerator DialogueStarted()
    {
        // Default implementation does nothing.
        yield break;
    }

    // Display a line.
    public override IEnumerator RunLine(Yarn.Line line) { yield break; }

    // Display the options, and call the optionChooser when done.
    public override IEnumerator RunOptions(Yarn.Options optionsCollection,
                                            Yarn.OptionChooser optionChooser)
    { yield break; }

    // Perform some game-specific command.
    public override IEnumerator RunCommand(Yarn.Command command) { yield break; }

    // The node has ended.
    public override IEnumerator NodeComplete(string nextNode)
    {
        // Default implementation does nothing.
        yield break;
    }

    // The conversation has ended.
    public override IEnumerator DialogueComplete()
    {
        // Default implementation does nothing.
        yield break;
    }


}
