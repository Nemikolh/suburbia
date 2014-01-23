// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;

public class TileREMView : TileView, HandlerSendTileToPosition
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
    
    public void OnMouseOver ()
    {
        if(Input.GetMouseButtonDown(0))
            Suburbia.Bus.FireEvent (new EventClickOnTileFromREM (this.m_tile, Suburbia.ActivePlayer, m_index));
        else if(Input.GetMouseButtonDown(1))
        {
            this.m_tile.SwitchWithLake();
            Suburbia.Bus.FireEvent (new EventClickOnTileFromREM (this.m_tile, Suburbia.ActivePlayer, m_index));
        }
    }

    public void Start ()
    {
        Suburbia.Bus.AddHandler (EventSendTileToPosition.TYPE, this);
    }

    public void HandleSendTileToPosition (EventSendTileToPosition p_event)
    {
        // TODO perform a smooth transition here instead of a rough destruction.
        if(p_event.index == this.m_index)
            Destroy (this.gameObject);
    }
}

