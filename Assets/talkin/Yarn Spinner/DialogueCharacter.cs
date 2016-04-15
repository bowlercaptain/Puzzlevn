using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueCharacter : ScriptableObject {

    
    public Color textColor;//used in similar ways: font, color, border style, whatever
    public Font textFont;


    public virtual string getName()
    {
        return name;
    }
}
