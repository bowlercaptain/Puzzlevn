using UnityEngine;
using System.Collections;
using Yarn;
using Yarn.Unity;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class ThisIsUI : DialogueUIBehaviour
{

    public System.Action callBack;

    public Text output;
    public Text charName;

    public GameObject textParent;
    public GameObject nameParent;//this is dumb, Unity should let me sort UI children behind their parents.

    public TextRenderer textRenderer;

    public PortraitDisplay portrait;

    public Transform buttonsPanel;
    public GameObject choiceButton;
    public GameObject spacer;
    public GameObject halfSpacer;

    // Display a line.
    public override IEnumerator RunLine(Yarn.Line line)
    {
        string goalText;
        List<string> tags =new List<string>();
        if (line.text[0] == '#')
        {
            Debug.LogWarning("Comment found: " + line.text);
            yield break;
        }
        if (line.text.Contains(":"))
        {
            string title = line.text.Substring(0, line.text.IndexOf(':')).ToLowerInvariant();
            string text = line.text.Substring(line.text.IndexOf(':') + 1);
            if (title.Length > 0 && title[title.Length - 1] == '*')
            {
                title = title.Substring(0, title.Length - 1);
                tags.Add("italic");
            }
            if (title.Split('.').Length > 1)
            {
                ShowPortrait(title.Split('.')[1], title.Split('.')[0]);
                portrait.HighlightCharacter(title.Split('.')[0]);
            }
            else
            {
                ShowPortrait(character: title);
                portrait.HighlightCharacter(title.Split('.')[0]);
            }
            goalText = text;
        }
        else
        {
            goalText = line.text;
        }
        

        yield return StartCoroutine(textRenderer.renderText(goalText, tags));
        
        yield return null;

        while (!CheckContinue())
        {
            yield return null;
        }
    }

    public static bool CheckContinue()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit");
    }

    int[] doneBox;
    // Display the options, and call the optionChooser when done.
    public override IEnumerator RunOptions(Yarn.Options optionsCollection,
                                            Yarn.OptionChooser optionChooser)
    {
        Coroutine timerRoutine = null;
        Debug.Log("Running Options");
        //output.text = "";
        int len = optionsCollection.options.Count;
        Instantiate(halfSpacer).transform.SetParent(buttonsPanel);
        doneBox = new int[] { -1 };
        for (int i = 0; i < len; i++)
        {
            string[] splitTitle = optionsCollection.options[i].Split('@');
            if (i != 0)
            {
                Instantiate(spacer).transform.SetParent(buttonsPanel);
            }
            GameObject buttonObject = Instantiate(choiceButton);
            buttonObject.GetComponentInChildren<Text>().text = TextRenderer.ChompLeadingSpace(splitTitle[0]);
            var cb = buttonObject.GetComponent<ChoiceButton>();
            cb.ui = this;
            cb.index = i;
            buttonObject.transform.SetParent(buttonsPanel);
            //output.text += (i+1).ToString() + ": " + optionsCollection.options[i];
            if (splitTitle.Length > 1)
            {
                switch (splitTitle[1])
                {
                    case "timer":
                        Debug.Assert(splitTitle.Length >= 3);
#if UNITY_EDITOR
                        int outt;
                        Debug.Assert(int.TryParse(splitTitle[2], out outt));
#endif
                        timerRoutine = StartCoroutine(TimeDelayChoice(i, int.Parse(splitTitle[2])));
                        break;
                }
            }
        }
        Instantiate(halfSpacer).transform.SetParent(buttonsPanel);

        while (doneBox[0] == -1)
        {
            for (int i = 0; i < len; i++)
            {
                if (Input.GetKey((i + 1).ToString()))
                {

                    doneBox[0] = i;
                }
            }



            yield return null;
        }
        optionChooser(doneBox[0]);
        foreach (Transform child in buttonsPanel)
        {
            Destroy(child.gameObject);
        }
        if (timerRoutine != null) { StopCoroutine(timerRoutine); }
        yield break;
    }

    IEnumerator TimeDelayChoice(int index, int millis)
    {
        yield return new WaitForSeconds(millis / 1000f);
        doneBox[0] = index;
    }

    public void AcceptChoice(int index)
    {//all of C# is stabbed. Straight stabbed.
        doneBox[0] = index;

    }

    // Perform some game-specific command.
    public override IEnumerator RunCommand(Yarn.Command command)
    {
        Debug.Log("Run command: " + command.text);
        string[] splitCommand = command.text.Split(' ');//command.text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        switch (splitCommand[0].ToLowerInvariant())
        {
            case "place":
                {
                    portrait.SetCharacter(splitCommand[1].ToLowerInvariant(), splitCommand[2].ToLowerInvariant(), (splitCommand.Length >= 4 ? splitCommand[3].ToLowerInvariant() : "show"));//figure this out later;
                    portrait.SetEmotion(splitCommand[2].ToLowerInvariant(), "default");
                }
                break;
            case "emote":
                {
                    portrait.SetEmotion(splitCommand[1].ToLowerInvariant(), splitCommand[2].ToLowerInvariant());
                }
                break;
            case "hidetext":
                {
                    textParent.SetActive(false);
                    nameParent.SetActive(false);
                    while (!CheckContinue())
                    {
                        yield return null;
                    }
                    textParent.SetActive(true);
                    nameParent.SetActive(true);
                }
                break;

            case "playsound":
                {
                    AudioManager.PlaySound(splitCommand[1], 1);
                }
                break;

            case "playuniquelooping":
                {
                    AudioManager.PlayUniqueLooping(splitCommand[1], splitCommand[2], 1);
                }
                break;
            case "alias":
                portrait.SetAlias(splitCommand[1], splitCommand[2]);
                break;
            default:
                Debug.LogError("Unknown command " + splitCommand[0]);
                break;
        }
        //"move <character> <slot>"
        //move <slot> <slot>

        yield break;
    }

    // The node has ended.
    public override IEnumerator NodeComplete(string nextNode)
    {

        yield break;
    }

    // The conversation has ended.
    public override IEnumerator DialogueComplete()
    {
        if (callBack != null) { callBack(); }

        yield break;
    }

    // A conversation has started.
    public override IEnumerator DialogueStarted()
    {


        yield break;
    }


    public DialogueCharacter defaultFormat;
    public void ShowPortrait(string emotion = null, string character = null, int slot = -1) //this will be from commands
    {
        if (slot != -1)
        {
            if (character != null)
            {
                portrait.SetCharacter(slot, character);
            }
            else
            {
                character = portrait.GetCharacter(slot);
            }
        }
        if (emotion != null)
        {
            portrait.SetEmotion(character, emotion);
        }
        DialogueCharacter format = null;
        //if (character != null)
        //{
        //    format = Resources.Load<DialogueCharacter>("Characters/"+character + "/" + emotion);
        //} if format == null && character != null)
        if (character != null)
        {
            format = Resources.Load<DialogueCharacter>("Characters/" + character + "/" + character);

        }
        if (format == null)
        {
            Debug.Log("Using default; could not find " + character);

            format = new DialogueCharacter();
            format.textColor = defaultFormat.textColor;
            format.name = character;
        }
        ShowFormat(format);

    }

    public void ShowFormat(DialogueCharacter format)
    {
        if (format != null)
        {
            charName.text = format.getName();
            output.color = format.textColor;
        }
        else
        {
            Debug.LogError("NO DEFAULT FORMAT PANIC ALSO HERE'S THE STACK TRACE");
        }
    }


}
