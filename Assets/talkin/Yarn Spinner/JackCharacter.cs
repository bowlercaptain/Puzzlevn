using UnityEngine;
using System.Collections;

public class JackScript : DialogueCharacter {

    public override string getName()
    {
        return "Jack " + Random.value.ToString();
    }
}
