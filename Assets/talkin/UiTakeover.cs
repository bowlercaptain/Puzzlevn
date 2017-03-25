using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UiTakeover {

	IEnumerator Start(int[] doneBox);

	void TakeLine();//take a yarn line

	void TakeOptions(); //take some options
}
