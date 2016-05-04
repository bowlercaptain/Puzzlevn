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
    public CharacterRend[] rends;
    string[] slotChars;//name cache left in place so we don't have to do a million null checks. slot-changing animations should make sure this doesn't desync

    public void Awake()
    {
        slotChars = new string[rends.Length];
    }

    public void SetCharacter(RendName name, string charName)
    {
        Debug.Log("setting character " + name.ToString() + " " + charName);
        SetCharacter((int)name, charName);
    }

    public void SetCharacter(int slot, string charName)
    {
        if (slotChars[slot] != charName)//yes this is actually necessary
        {
            slotChars[slot] = charName;
            rends[slot].name =charName;
            SetEmotion(slot, "default");//so this doesn't run when you're just going back to a character and want the right font.
        }
    }

    public string GetCharacter(int slot)
    {
        return slotChars[slot];
    }

    public void SetEmotion(string character, string emotion)
    {
        for (int i = 0; i < slotChars.Length; i++)
        {
            if (slotChars[i] == character)
            {
                SetEmotion(i, emotion);
            }
        }
    }

    public void SetEmotion(int slot, string emotion)
    {
        SetEmotion(slot, slotChars[slot], emotion);
    }


    public void SetEmotion(int slot, string charname, string emotion)
    {
        Texture toShow = Resources.Load<Texture>("Characters/" + charname + "/" + emotion);
        if (toShow == null)//skip looking for character default to show that emotion is missing
        {
            toShow = Resources.Load<Texture>("Characters/fallback");
        }
        rends[slot].texture = toShow;
    }

    public void HighlightCharacter(string character)
    {
        HighlightCharacter(GetSlot(character));
    }

    public void HighlightCharacter(int slot)
    {
        for (int i = 0; i < rends.Length; i++)
        {
            if (i == slot)
            {
                FadeIn(i);
            }
            else
            {
                FadeOut(i);
            }
        }
    }

    public void FadeOut(int slot)
    {
        if (rends[slot] != null)
            rends[slot].color = Color.grey;
    }

    public void FadeIn(int slot)
    {
        if (rends[slot] != null)
            rends[slot].color = Color.white;//send a fade-to-color animation to the appropriate manager; override previous fades. (animation slots? animation tags? non-uniqueness somehow.
    }

    public void FadeOut(string character)
    {
        FadeOut(GetSlot(character));
    }

    public void FadeIn(string character)
    {
        FadeIn(GetSlot(character));
    }

    public int GetSlot(string character)
    {
        for (int i = 0; i < slotChars.Length; i++)
        {
            if (slotChars[i] == character)
            {
                return i;
            }
        }
        return -1;
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
