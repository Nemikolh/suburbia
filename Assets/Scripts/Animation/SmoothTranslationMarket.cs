// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //   
using System;

public class SmoothTranslationMarket : SmoothTranslation
{

    void Start()
    {
        this.Construct(TranslationType.KEEP_ON_DESTINATION);
    }

    void OnMouseEnter ()
    {
        this.SwitchTransition();
    }
    
    void OnMouseExit ()
    {
        this.SwitchTransition();
    }

}


