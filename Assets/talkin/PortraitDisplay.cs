//#define adorb

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PortraitDisplay : MonoBehaviour
{

    public GameObject rendPrefab;

    public struct Slot
    {
        public Slot(int slotNum, Vector3 position, Vector3 scale)
        {
            this.slotNum = slotNum;
            this.position = position;
            this.scale = scale;
        }
        public int slotNum;
        public Vector3 position;
        public Vector3 scale;
    }

    private Dictionary<string, Slot> slotList = new Dictionary<string, Slot>()  {
    { "left", new Slot(0,new Vector3(-4.14f,3f,0),new Vector3(7.05f,14.1f,1f))},
    { "right", new Slot(1,new Vector3(3.54f,3f,0),new Vector3(7.05f,14.1f,1f))},
    { "center", new Slot(2,new Vector3(0f,2.6f,0),new Vector3(8f,16f,1f))},
    { "background", new Slot(3,new Vector3(0,0,1),new Vector3(19.2f*1.2f,10.8f*1.2f,1f))}
    };

    public CharacterRend[] rends;
    string[] slotChars;//name cache left in place so we don't have to do a million null checks. slot-changing animations should make sure this doesn't desync
    public Vector3[] slotLocations;

    private Dictionary<string, string> aliases = new Dictionary<string, string>();

    public void Awake()
    {
        rends = new CharacterRend[slotList.Count];
        slotChars = new string[rends.Length];
    }

    public void PlaceCharacter(string slotName, CharacterRend rend)
    {
        PlaceCharacter(slotList[slotName], rend);
    }

    private void PlaceCharacter(Slot slot, CharacterRend rend)
    {
        rends[slot.slotNum] = rend;
        slotChars[slot.slotNum] = rend.name;
    }

    public void SetCharacter(string slotName, string charName, string showHide = "show")
    {
        SetCharacter(slotList[slotName], charName, showHide);
    }

    public void SetCharacter(int slotNum, string charName, string showHide = "show")
    {
        SetCharacter(GetSlot(slotNum), charName, showHide);
    }

    private void SetCharacter(Slot slot, string charName, string showHide = "show")
    {
        if (rends[slot.slotNum] == null)
        {
            //TODO: probably objectFactory this but fuck it
            rends[slot.slotNum] = Instantiate(rendPrefab).GetComponent<CharacterRend>();
            PositionRend(rends[slot.slotNum], slot);
            if (showHide == "hide")
            {
#if adorb
			rends[slot.slotNum].transform.localRotation = Quaternion.Euler(0,0,90) ;
#endif
                rends[slot.slotNum].transform.position = Vector3.up * 1000;
            }
        }
        if (slotChars[slot.slotNum] != charName)//yes this is actually necessary
        {
            slotChars[slot.slotNum] = charName;
            rends[slot.slotNum].name = charName;
            SetEmotion(slot.slotNum, "default");//so this doesn't run when you're just going back to a character and want the right font.
        }
    }

    private void PositionRend(CharacterRend rend, Slot slot)
    {
        Transform rendTransform = rend.transform;
        rendTransform.position = slot.position;
        rendTransform.localScale = slot.scale;
        rend.targetSlot = slot;
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
                return;
            }
        }
        Debug.LogWarning("could not find " + character + " to emote with");
    }

    public void SetEmotion(int slot, string emotion)
    {
        SetEmotion(slot, slotChars[slot], emotion);
    }

    public void SetAlias(string alias, string actualName)
    {
        aliases[alias] = actualName;
    }

    public void SetEmotion(int slot, string charname, string emotion)
    {
        if (aliases.ContainsKey(charname))
        {
            charname = aliases[charname];
        }
        Texture toShow = Resources.Load<Texture>("characters/" + charname + "/" + emotion);
        if (toShow == null)//skip looking for character default to show that emotion is missing
        {
            Debug.LogWarning("Couldn't find emotion " + emotion + " for character " + charname);
            toShow = Resources.Load<Texture>("characters/fallback");
        }
        rends[slot].texture = toShow;
    }

    public void HighlightCharacter(string character)
    {
        HighlightCharacter(GetSlotNum(character));
    }

    public void HighlightCharacter(int slot)
    {
        Debug.Log("highlighting " + slot.ToString());
        for (int i = 0; i < rends.Length; i++)
        {


            if (i == slot)
            {
                Debug.AssertFormat(rends[i] != null, "Tried to highlight a nulled slot! What?", slot, GetCharacter(slot));
                rends[i].FadeUpA();
            }
            else
            {
                if (rends[i] != null)
                    rends[i].FadeDownA();
            }

        }
    }


    public int GetSlotNum(string character)
    {
        for (int i = 0; i < slotChars.Length; i++)
        {
            if (slotChars[i] == character)
            {
                return i;
            }
        }
        Debug.Log("Can't find " + character);
        Debug.Log(string.Join(", ", slotChars));
        return -1;
    }

    private Slot GetSlot(int slotNum)
    {
        foreach (Slot slot in slotList.Values)
        {
            if (slot.slotNum == slotNum)
            {
                return slot;
            }
        }
        throw new ArgumentOutOfRangeException("slotNum", "slot does not exist");
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
