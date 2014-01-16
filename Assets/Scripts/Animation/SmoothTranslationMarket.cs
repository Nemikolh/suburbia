// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //   
using System;

public class SmoothTranslationMarket : SmoothTranslation
{
    void OnMouseEnter ()
    {
        m_is_transition_started = true;
    }
    
    void OnMouseExit ()
    {
        m_is_transition_started = false;
    }

}


