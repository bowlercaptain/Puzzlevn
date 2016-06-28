using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class DialogueView : MonoBehaviour {
    const string DIASCENENAME = "dialogueScene";

	// Use this for initialization
	void Start () {
        Debug.Log("starting");
        ShowDialogue("dialogueone", () => { Debug.Log("finish!"); });
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowDialogue(string resourcePath, Action callback, string nodeName = Yarn.Dialogue.DEFAULT_START)//NO NO NO this is static; make an instance object if you really have to. Find some other way to wait one frame for the scene to load goddamnit.
    {
        StartCoroutine(ShowDialogueR(resourcePath, callback, nodeName));
    }

    public static IEnumerator ShowDialogueR(string resourcePath, Action callback, string nodeName = Yarn.Dialogue.DEFAULT_START)
    {
        SceneManager.LoadScene(DIASCENENAME, LoadSceneMode.Additive);
        yield return null;
        ThisIsUI myUI = FindObjectOfType<ThisIsUI>();
        myUI.callBack = ()=> { callback(); SceneManager.UnloadScene(DIASCENENAME); };
        var myRunner = myUI.gameObject.GetComponent<Yarn.Unity.DialogueRunner>();
        myRunner.AddScript(Resources.Load<TextAsset>(System.IO.Path.Combine("chats", resourcePath)));
        myRunner.StartDialogue(nodeName);
    }
}
