using UnityEngine;
using System.Collections;

public class ChoiceButton : MonoBehaviour {

	public ThisIsUI ui;
	public int index;

	public void sendClick() {
		ui.AcceptChoice(index);
	}

}
