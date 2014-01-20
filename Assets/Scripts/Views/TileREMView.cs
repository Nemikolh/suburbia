// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public class TileREMView : TileView
{

    private int m_index;

    public int index {
        set {
            m_index = value;
        }
    }

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
        Suburbia.Bus.FireEvent (new EventClickOnTileFromREM (this.m_tile, Suburbia.ActivePlayer, m_index));
    }
}

