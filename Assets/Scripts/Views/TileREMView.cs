// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public class TileREMView : TileView
{
    public void OnMouseEnter ()
    {
        Suburbia.Bus.FireEvent (new EventShowTileInformation (true, this.m_tile.description));
    }
    
    public void OnMouseExit ()
    {
        Suburbia.Bus.FireEvent (new EventShowTileInformation (false, this.m_tile.description));
    }
    
    public void OnMouseDown ()
    {
        Suburbia.Bus.FireEvent (new EventClickOnTileFromREM (this.m_tile));
    }
}

