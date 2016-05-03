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

    public override void SetValue(string variableName, Value value)
    {
        PlayerPrefs.SetString("Yarn" + variableName, value.ToString());
        PlayerPrefs.SetInt("Yarn" + variableName + "type", (int)value.type);
    }

    public override Value GetValue(string variableName)
    {

        switch ((Value.Type)PlayerPrefs.GetInt("Yarn" + variableName + "type", (int)Value.Type.Null))//the inside code is quite fragile; if you have a variable type you should also have a value. adding defaults would not be hard but will do more masking than fixing.
        {
            case Value.Type.Bool:
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
        //throw new System.NotImplementedException();
    }

}
