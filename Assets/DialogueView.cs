using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class DialogueView : MonoBehaviour
{
    const string DIASCENENAME = "dialogueScene";

    // This is debug code
    //void Start()
    //{
    //    Debug.Log("starting");
    //    ShowDialogue("dialogueone", () => { Debug.Log("finish!"); });
    //}

        public static void Show(string resourcePath)
    {
        Show(resourcePath, AFunctionThatDoesntDoAnythingDotExe);
        //        Show(resourcePath, ()=> { });
    }

    static void AFunctionThatDoesntDoAnythingDotExe() { }

    public static void Show(string resourcePath, Action callback, string nodeName = Yarn.Dialogue.DEFAULT_START)
    {
        var host = new GameObject("Dialogue host", new Type[] { typeof(DialogueView) }).GetComponent<DialogueView>();
        host.StartCoroutine(host.ShowDialogueR(resourcePath, callback, nodeName));
    }

    public IEnumerator ShowDialogueR(string resourcePath, Action callback, string nodeName = Yarn.Dialogue.DEFAULT_START)
    {
        SceneManager.LoadScene(DIASCENENAME, LoadSceneMode.Additive);
        yield return null;
        ThisIsUI myUI = FindObjectOfType<ThisIsUI>();
        myUI.callBack = () => { FinishDialogue(callback); };
        var myRunner = myUI.gameObject.GetComponent<Yarn.Unity.DialogueRunner>();
        myRunner.AddScript(Resources.Load<TextAsset>(System.IO.Path.Combine("chats", resourcePath)));
        myRunner.StartDialogue(nodeName);
    }

    private void FinishDialogue(Action callback)
    {
        callback();
        SceneManager.UnloadScene(DIASCENENAME);
        Destroy(gameObject);
    }


}
