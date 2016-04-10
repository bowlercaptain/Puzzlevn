using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PortraitDisplay : MonoBehaviour
{



    void Start()
    {

    }

    Dictionary<string, Character> characterLookup;//CONVERT THIS TO FOLDER/RESOURCES/SCRIPTABLEOBJECT FORMAT

    public struct Emotion
    {
        string portraitName;//filename or path to inside resources

        public Emotion(string resourcePath)
        {
            portraitName = resourcePath;
        }
    }
    Emotion defaultEmotion = new Emotion("default");

    public struct Character//scriptable object? Probably needs more specific name
    {
        public Dictionary<string, Emotion> emotions;
        public Color textColor;//used in similar ways: font, color, border style, whatever
    }

    public void ShowPortrait(string emotion, string character = null, int slot = -1) //this will be from commands
    {
        DialogueCharacter format;
        if (character != null)
        {
            format = Resources.Load<DialogueCharacter>(character + "/" + emotion);
        }
        else
        {

        }


        //load from Resources Characters/item0/item0. If that's null, show missing resource. load default for character if no item [1]. use last loaded on that side if nothing

        var splitString = emotion.Split('.');
        Emotion toShow = defaultEmotion;
        if (characterLookup.ContainsKey(splitString[0]))
        {
            Character chara = characterLookup[splitString[0]];
            try
            {
                if (splitString.Length > 0 && chara.emotions != null && chara.emotions.ContainsKey(splitString[1]))
                {
                    toShow = chara.emotions[splitString[1]];
                }
                else
                {
                    toShow = chara.emotions["default"];
                }
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogError("no portrait " + e.Message);
                //use mystery/broken portrait
            }
        }
        else
        {
            toShow = defaultEmotion;
        }
        showEmotion(defaultEmotion);
        //use font/color/name/whatever
        //check resources for full string
        //look for parenthesized filename?
        //set character name / text type (font, color, whatever) based on character?
        //could have a series of lookups that link to character type and sub-emotion
    }

    public void showEmotion(Emotion toShow)
    {
        //resources.load toshow
    }

}
