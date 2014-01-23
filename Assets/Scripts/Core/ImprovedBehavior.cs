// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;

public abstract class ImprovedBehavior<T, Arg> : MonoBehaviour
    where T : ImprovedBehavior<T, Arg>
{
    public static T Create(GameObject p_obj, Arg p_arg1)
    {
        T behavior = p_obj.AddComponent<T>();
        behavior.Construct(p_arg1);
        return behavior;
    }

    public abstract void Construct(Arg p_arg1);
}

