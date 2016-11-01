using UnityEngine;
using System.Collections;

public class ConstAltDiaCharacter : DialogueCharacter
{
    public string myName = "Protag";
    public override string getName()
    {
        return myName;
    }
}