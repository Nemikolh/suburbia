// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //

using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public sealed class Util
{
    private Util ()
    {
    }

    public static string convertToSignedStr(int p_value)
    {
        return ((p_value > 0) ? "+" + p_value : "" + p_value);
    }

    public static T getValue<T> (JSONClass p_json, string p_attribute)
    {
        object temp = null;
        if(typeof(T) == typeof(string))
        {
            temp = p_json [p_attribute].Value;
        }
        else if(typeof(T) == typeof(int))
        {
            temp = p_json [p_attribute].AsInt;
        }
        else if(typeof(T) == typeof(float))
        {
            temp = p_json [p_attribute].AsFloat;
        }
        else if(typeof(T) == typeof(bool))
        {
            temp = p_json [p_attribute].AsBool;
        }
        return (T)temp;
    }

    public static T parseEnum<T> (string p_value)
    {
        return (T)Enum.Parse (typeof(T), p_value, true);
    }

    public static T tryEnum<T> (string p_value)
    {
        try {
            return  (T)Enum.Parse (typeof(T), p_value, true);
        } catch (ArgumentException) {
            return (T)Enum.Parse (typeof(T), "NULL", true);
        }
    }

    public delegate void DelayedFunc();

    public static IEnumerator Delay(DelayedFunc f, float p_delay)
    {
        yield return new WaitForSeconds(p_delay);
        f();
    }
}

