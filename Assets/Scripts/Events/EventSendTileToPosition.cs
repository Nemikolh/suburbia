// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //

public class EventSendTileToPosition : IEvent<HandlerSendTileToPosition>
{
    public static Type<HandlerSendTileToPosition> TYPE = new Type<HandlerSendTileToPosition> ();
    private readonly Player m_player;
    private readonly TilePosition m_position;
    private readonly int m_index_in_REM;

    public EventSendTileToPosition (Player p_player, TilePosition p_position, int p_index)
    {
        m_player = p_player;
        m_position = p_position;
        m_index_in_REM = p_index;
    }

    public override void Dispatch (HandlerSendTileToPosition p_handler)
    {
        p_handler.HandleSendTileToPosition (this);
    }

    public override Type<HandlerSendTileToPosition> GetEventType ()
    {
        return TYPE;
    }

    public Player player {
        get {
            return this.m_player;
        }
    }

    public TilePosition position {
        get {
            return this.m_position;
        }
    }

    public int index {
        get {
            return this.m_index_in_REM;
        }
    }
}
