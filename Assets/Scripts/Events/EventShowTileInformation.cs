// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public class EventShowTileInformation : IEvent<HandlerShowTileInformation>
{
    public static Type<HandlerShowTileInformation> TYPE = new Type<HandlerShowTileInformation> ();
    private bool m_onenter;
    private Tile m_tile_description;

    public EventShowTileInformation (bool p_onenter, Tile p_tile_description)
    {
        m_onenter = p_onenter;
        m_tile_description = p_tile_description;
    }
    
    public override void Dispatch (HandlerShowTileInformation p_handler)
    {
        p_handler.HandleShowTileInformation (this);
    }
    
    public override Type<HandlerShowTileInformation> GetEventType ()
    {
        return TYPE;
    }

    public Tile tile {
        get {
            return this.m_tile_description;
        }
    }

    public bool IsAskingToBeShown ()
    {
        return this.m_onenter;
    }
}