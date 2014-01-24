// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;

public class EventBasedBehavior : MonoBehaviour
{
    public static void Destroy (UnityEngine.Object p_obj)
    {
        MonoBehaviour.Destroy (p_obj);
    
        if (p_obj as IHandler != null)
            Suburbia.Bus.FireEvent (new EventHandlerHasBeenDestroyed (p_obj as IHandler));
    }

    public static void Destroy (UnityEngine.Object p_obj, float p_t)
    {
        MonoBehaviour.Destroy (p_obj, p_t);
        
        if (p_obj as IHandler != null)
            Suburbia.Bus.FireEvent (new EventHandlerHasBeenDestroyed (p_obj as IHandler));
    }
}

