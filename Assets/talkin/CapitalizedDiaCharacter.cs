using UnityEngine;
using System.Collections;

public class CapitalizedDiaCharacter : DialogueCharacter {

	public override string getName() {
        var name = base.getName();
        Debug.Assert(name.Length > 0);

        return name.Substring(0, 1).ToUpper() + name.Substring(1);
	}
}