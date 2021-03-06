using System;

public class EventTilePlayed : IEvent<HandlerTilePlayed>
{
    public static Type<HandlerTilePlayed> TYPE = new Type<HandlerTilePlayed> ();
    private TileInstance m_tile_played;
    private int m_price_overhead;
    private bool m_setuptile;

    public EventTilePlayed (TileInstance p_tile_played, int p_price_overhead)
    {
        m_tile_played = p_tile_played;
        m_price_overhead = p_price_overhead;
        m_setuptile = false;
    }

    public EventTilePlayed (TileInstance p_tile_played, int p_price_overhead, bool p_setuptile)
    {
        m_tile_played = p_tile_played;
        m_price_overhead = p_price_overhead;
        m_setuptile = p_setuptile;
    }

    public override void Dispatch (HandlerTilePlayed p_handler)
    {
        p_handler.HandleTilePlayed (this);
    }

    public override Type<HandlerTilePlayed> GetEventType ()
    {
        return TYPE;
    }

    public TileInstance tile_played {
        get {
            return this.m_tile_played;
        }
    }

    public int price_overhead {
        get {
            return this.m_price_overhead;
        }
    }

    public bool setuptile {
        get {
            return this.m_setuptile;
        }
    }
}
