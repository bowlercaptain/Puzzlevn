using UnityEngine;
using System.Collections;

public class sandbox : MonoBehaviour {

    //bool flag = false;

	// Use this for initialization
	void Start () {
        //if (PlayerPrefs.GetInt("testt", 0) == 1)
        //{
        //    flag = true;
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("testt", 1);
        //    PlayerPrefs.Save();
        //}
        DialogueView.Show("dialogueone");
	}
	
	//// Update is called once per frame
	//void Update () {
 //       if (flag)
 //       {
 //           transform.position = transform.position + Vector3.right;
 //       }
	//}

 //   void OnApplicationQuit()
 //   {
 //       PlayerPrefs.SetInt("testt", 0);
 //   }
}
