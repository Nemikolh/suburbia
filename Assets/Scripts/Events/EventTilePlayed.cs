using System;

public class EventTilePlayed : IEvent<HandlerTilePlayed>
{
    public static Type<HandlerTilePlayed> TYPE = new Type<HandlerTilePlayed> ();
    private TileInstance m_tile_played;

    public EventTilePlayed (TileInstance p_tile_played)
    {
        m_tile_played = p_tile_played;
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
}
