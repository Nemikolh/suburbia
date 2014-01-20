// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //

public class EventClickOnFreePosition : IEvent<HandlerClickOnFreePosition>
{
    public static Type<HandlerClickOnFreePosition> TYPE = new Type<HandlerClickOnFreePosition> ();
    private Player m_player;
    private TilePosition m_position;

    public EventClickOnFreePosition (Player p_player, TilePosition p_position)
    {
        m_player = p_player;
        m_position = p_position;
    }

    public override void Dispatch (HandlerClickOnFreePosition p_handler)
    {
        p_handler.HandleClickOnFreePosition (this);
    }

    public override Type<HandlerClickOnFreePosition> GetEventType ()
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
}
