using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueCharacter : ScriptableObject {

    
    public Color textColor = new Color(1,0,1);//used in similar ways: font, color, border style, whatever
    public Font textFont;
    //public new string name;

    public virtual string getName()
    {
        return name;
    }
}
