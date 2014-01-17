// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public class EventClickedOnTileFromREM : IEvent<HandlerClickOnTileFromREM>
{
    public static Type<HandlerClickOnTileFromREM> TYPE = new Type<HandlerClickOnTileFromREM> ();

    private readonly TileInstance m_tile;

    public TileInstance tile {
        get{
            return this.m_tile;
        }
    }

    public EventClickedOnTileFromREM (TileInstance p_tile)
    {
        m_tile = p_tile;
    }

    public override void Dispatch (HandlerClickOnTileFromREM p_handler)
    {
        p_handler.HandleClickOnTileFromREM (this);
    }
    
    public override Type<HandlerClickOnTileFromREM> GetEventType ()
    {
        return TYPE;
    }
}