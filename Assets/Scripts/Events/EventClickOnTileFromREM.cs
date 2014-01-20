// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public class EventClickOnTileFromREM : IEvent<HandlerClickOnTileFromREM>
{
    public static Type<HandlerClickOnTileFromREM> TYPE = new Type<HandlerClickOnTileFromREM> ();
    private readonly TileInstance m_tile;
    private readonly Player m_current;

    public TileInstance tile {
        get {
            return this.m_tile;
        }
    }

    public Player current {
        get {
            return this.m_current;
        }
    }

    public EventClickOnTileFromREM (TileInstance p_tile, Player p_player)
    {
        m_tile = p_tile;
        m_current = p_player;
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