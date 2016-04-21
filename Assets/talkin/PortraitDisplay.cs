using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PortraitDisplay : MonoBehaviour
{
    public enum RendName
    {
        left = 0,
        right = 1
    }
    public MeshRenderer[] rends;
    string[] slotChars;

    public void Awake()
    {
        slotChars = new string[rends.Length];
    }

    public void SetCharacter(RendName name, string charName)
    {
        SetCharacter((int)name, charName);
    }

    public void SetCharacter(int slot, string charName)
    {
        slotChars[slot] = charName;
        //queue show default emotion
        SetEmotion(slot, "default");
    }

    public void SetEmotion(string character, string emotion)
    {
        for (int i = 0; i < slotChars.Length; i++)
        {
            if(slotChars[i] == character) {
                SetEmotion(i, emotion);
            }
        }
    }

    public void SetEmotion(int slot, string emotion)
    {
        Texture toShow = Resources.Load<Texture>("Characters/" + slotChars[slot] + "/" + emotion);
        if(toShow == null)
        {
            toShow = Resources.Load<Texture>("Characters/fallback");
        }

        rends[slot].material.mainTexture = toShow; 
    }

    //play animation (lrc, animation);
    //fade managment
    //etc.
    //use animationManager type thing, make character switches/fades also animations in coroutines into the same system

    









    //Dictionary<string, Character> characterLookup;//CONVERT THIS TO FOLDER/RESOURCES/SCRIPTABLEOBJECT FORMAT

    //public struct Emotion
    //{
    //    string portraitName;//filename or path to inside resources

    //    public Emotion(string resourcePath)
    //    {
    //        portraitName = resourcePath;
    //    }
    //}
    //Emotion defaultEmotion = new Emotion("default");

    //public struct Character//scriptable object? Probably needs more specific name
    //{
    //    public Dictionary<string, Emotion> emotions;
    //    public Color textColor;//used in similar ways: font, color, border style, whatever
    //}

 

        /*
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

    */
    

}
