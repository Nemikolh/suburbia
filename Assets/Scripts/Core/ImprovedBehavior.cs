// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using System.Collections;
using UnityEngine;

public delegate void DelayedFunc();

public abstract class ImprovedBehavior : EventBasedBehavior
{
    public void Delay(DelayedFunc f, float p_delay)
    {
        StartCoroutine(DelayHelper(f, p_delay));
    }
    
    private IEnumerator DelayHelper(DelayedFunc f, float p_delay)
    {
        //Debug.Log("Delay start");
        yield return new WaitForSeconds(p_delay);
        f(); 
    }
}

public abstract class ImprovedBehavior<T, Arg> : ImprovedBehavior
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