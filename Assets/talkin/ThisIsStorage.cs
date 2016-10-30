using UnityEngine;
using System.Collections;
using Yarn;
using Yarn.Unity;
using System;

public class ThisIsStorage : VariableStorageBehaviour
{
    public override void ResetToDefaults()
    {
        
        //throw new NotImplementedException();
    }

    public override void SetValue(string variableName, Value value)
    {
        Debug.Log(variableName + " requested stored "+value.type);
        PlayerPrefs.SetString("Yarn" + variableName, value.AsString);
        PlayerPrefs.SetInt("Yarn" + variableName + "type", (int)value.type);
    }

    public override Value GetValue(string variableName)
    {
        Debug.Log(variableName + " requested");
        Debug.Log((Value.Type)PlayerPrefs.GetInt("Yarn" + variableName + "type", (int)Value.Type.Null));
        switch ((Value.Type)PlayerPrefs.GetInt("Yarn" + variableName + "type", (int)Value.Type.Null))//the inside code is quite fragile; if you have a variable type you should also have a value. adding defaults would not be hard but will do more masking than fixing.
        {
            case Value.Type.Bool:
                Debug.Log("returning bool");
                Debug.Log(PlayerPrefs.GetString("Yarn" + variableName));
                return new Value(bool.Parse(PlayerPrefs.GetString("Yarn" + variableName)));
            case Value.Type.Number:
                return new Value(float.Parse(PlayerPrefs.GetString("Yarn" + variableName)));
            case Value.Type.String:
                return new Value(PlayerPrefs.GetString("Yarn" + variableName));
            case Value.Type.Variable:
                throw new NotImplementedException("WHAT IS THIS I DON'T EVEN PLEASE TELL ROBERT WHAT VARIABLE MADE THIS HAPPEN AND HOW");
            case Value.Type.Null:
            default:
                return new Value(null);

        }
        //TODO: save YS<name>type: value.type.ToString() and YS<name>value or just YS<name> as value.ToString or with a switch statement appropriate type.
        //return PlayerPrefs.GetFloat("YarnVar" + variableName, 0f);
    }

    public override void Clear()
    {
        Debug.Log("clearing");
		PlayerPrefs.DeleteAll();
	}
}
