using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PortraitDisplay : MonoBehaviour {

	public GameObject rendPrefab;

	private struct Slot {
		public Slot(int slotNum, Vector3 position, Vector3 scale) {
			this.slotNum = slotNum;
			this.position = position;
			this.scale = scale;
		}
		public int slotNum;
		public Vector3 position;
		public Vector3 scale;
	}

	private Dictionary<string, Slot> slotList = new Dictionary<string, Slot>()  {
	{ "left", new Slot(0,new Vector3(-3.1f,-0.06f,0),new Vector3(5f,5f,1f))},
	{ "right", new Slot(1,new Vector3(2.44f,-0.06f,0),new Vector3(5f,5f,1f))},
	{ "background", new Slot(2,new Vector3(0,0,0),new Vector3(10f,10f,1f))}
	};

	public CharacterRend[] rends;
	string[] slotChars;//name cache left in place so we don't have to do a million null checks. slot-changing animations should make sure this doesn't desync
	public Vector3[] slotLocations;

	public void Awake() {
		slotChars = new string[rends.Length];
	}

	public void PlaceCharacter(string slotName, CharacterRend rend) {
		PlaceCharacter(slotList[slotName], rend);
	}

	private void PlaceCharacter(Slot slot, CharacterRend rend) {
		rends[slot.slotNum] = rend;
		slotChars[slot.slotNum] = rend.name;
	}

	public void SetCharacter(string slotName, string charName) {
		SetCharacter(slotList[slotName], charName);
	}

	public void SetCharacter(int slotNum, string charName) {
		SetCharacter(GetSlot(slotNum), charName);
	}

	private void SetCharacter(Slot slot, string charName) {
		if (rends[slot.slotNum] == null) {
			//TODO: probably objectFactory this but fuck it
			rends[slot.slotNum] = Instantiate(rendPrefab).GetComponent<CharacterRend>();
			PositionRend(rends[slot.slotNum], slot);
        }
		if (slotChars[slot.slotNum] != charName)//yes this is actually necessary
		{
			slotChars[slot.slotNum] = charName;
			rends[slot.slotNum].name = charName;
			SetEmotion(slot.slotNum, "default");//so this doesn't run when you're just going back to a character and want the right font.
		}
	}

	private void PositionRend(CharacterRend rend, Slot slot) {
		Transform rendTransform = rend.transform;
		rendTransform.position = slot.position;
		rendTransform.localScale = slot.scale;
	}

	public string GetCharacter(int slot) {
		return slotChars[slot];
	}

	public void SetEmotion(string character, string emotion) {
		for (int i = 0; i < slotChars.Length; i++) {
			if (slotChars[i] == character) {
				SetEmotion(i, emotion);
			}
		}
	}

	public void SetEmotion(int slot, string emotion) {
		SetEmotion(slot, slotChars[slot], emotion);
	}


	public void SetEmotion(int slot, string charname, string emotion) {
		Texture toShow = Resources.Load<Texture>("Characters/" + charname + "/" + emotion);
		if (toShow == null)//skip looking for character default to show that emotion is missing
		{
			toShow = Resources.Load<Texture>("Characters/fallback");
		}
		rends[slot].texture = toShow;
	}

	public void HighlightCharacter(string character) {
		HighlightCharacter(GetSlotNum(character));
	}

	public void HighlightCharacter(int slot) {
		for (int i = 0; i < rends.Length; i++) {
			if (i == slot) {
				FadeIn(i);
			} else {
				FadeOut(i);
			}
		}
	}

	public void FadeOut(int slot) {
		if (rends[slot] != null)
			rends[slot].color = Color.grey;
	}

	public void FadeIn(int slot) {
		if (rends[slot] != null)
			rends[slot].color = Color.white;//send a fade-to-color animation to the appropriate manager; override previous fades. (animation slots? animation tags? non-uniqueness somehow.
	}

	public void FadeOut(string character) {
		FadeOut(GetSlotNum(character));
	}

	public void FadeIn(string character) {
		FadeIn(GetSlotNum(character));
	}

	public int GetSlotNum(string character) {
		for (int i = 0; i < slotChars.Length; i++) {
			if (slotChars[i] == character) {
				return i;
			}
		}
		return -1;
	}

	private Slot GetSlot(int slotNum) {
		foreach (Slot slot in slotList.Values) {
			if (slot.slotNum == slotNum) {
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
