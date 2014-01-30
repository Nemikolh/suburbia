// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //

public interface HandlerTileRemovedFromREM : IHandler
{
    void HandleTileRemovedFromREM (EventTileRemovedFromREM p_event);
}

public class EventTileRemovedFromREM : IEvent<HandlerTileRemovedFromREM>
{
    public static Type<HandlerTileRemovedFromREM> TYPE = new Type<HandlerTileRemovedFromREM> ();
    private int m_index_from_REM;
    private TileInstance m_new_tile;

    public EventTileRemovedFromREM (TileInstance p_new_tile, int p_index_from_REM)
    {
        m_index_from_REM = p_index_from_REM;
        m_new_tile = p_new_tile;
    }

    public override void Dispatch (HandlerTileRemovedFromREM p_handler)
    {
        p_handler.HandleTileRemovedFromREM (this);
    }

    public override Type<HandlerTileRemovedFromREM> GetEventType ()
    {
        return TYPE;
    }

    public int index {
        get {
            return this.m_index_from_REM;
        }
    }

    public TileInstance newTile {
        get {
            return this.m_new_tile;
        }
    }

}
