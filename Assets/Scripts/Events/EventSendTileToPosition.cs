// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //

public class EventSendTileToPosition : IEvent<HandlerSendTileToPosition>
{
    public static Type<HandlerSendTileToPosition> TYPE = new Type<HandlerSendTileToPosition> ();
    private Player m_player;
    private TilePosition m_position;

    public EventSendTileToPosition (Player p_player, TilePosition p_position)
    {
        m_player = p_player;
        m_position = p_position;
    }

    public override void Dispatch (HandlerSendTileToPosition p_handler)
    {
        p_handler.HandleSendTileToPosition (this);
    }

    public override Type<HandlerSendTileToPosition> GetEventType ()
    {
        return TYPE;
    }

}
