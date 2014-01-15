// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //

using System;
using SimpleJSON;

public sealed class Util
{
    private Util ()
    {
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
}

