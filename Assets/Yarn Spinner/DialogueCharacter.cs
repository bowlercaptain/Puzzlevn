using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueCharacter : ScriptableObject {

    public Dictionary<string, string> aliases = new Dictionary<string, string>() { {"lol", "laughing" }, { "sads", "unhappy"} };


    public Color textColor;//used in similar ways: font, color, border style, whatever
    public Font textFont;
}
