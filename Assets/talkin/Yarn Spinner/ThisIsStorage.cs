using UnityEngine;
using System.Collections;
using Yarn;
using Yarn.Unity;
using System;

public class ThisIsStorage : VariableStorageBehaviour
{
    public override void ResetToDefaults()
    {
        PlayerPrefs.DeleteAll();
        //throw new NotImplementedException();
    }

    public override void SetNumber(string variableName, float number)
    {
        PlayerPrefs.SetFloat("YarnVar" + variableName, number);
    }

    public override float GetNumber(string variableName)
    {
        return PlayerPrefs.GetFloat("YarnVar" + variableName, 0f);
    }

    public override void Clear()
    {
        Debug.Log("clearing");
        //throw new System.NotImplementedException();
    }

}
