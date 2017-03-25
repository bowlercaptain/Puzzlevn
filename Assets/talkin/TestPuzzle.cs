using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPuzzle : MonoBehaviour, UiTakeover {

	// Use this for initialization
	void Start () {
		
	}

	IEnumerator UiTakeover.Start(int[] doneBox)
	{
		//make a thing
		//yield break when you want to get another line/options
		yield break;
	}

	void UiTakeover.TakeLine()
	{
		throw new NotImplementedException();
	}

	void UiTakeover.TakeOptions()
	{
		throw new NotImplementedException();
	}

	// Update is called once per frame
	void Update () {
		
	}

	
}
