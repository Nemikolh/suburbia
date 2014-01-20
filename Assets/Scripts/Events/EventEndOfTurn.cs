using UnityEngine;

public class EventEndOfTurn : IEvent<HandlerEndOfTurn>
{
    public static Type<HandlerEndOfTurn> TYPE = new Type<HandlerEndOfTurn> ();
    private readonly Player m_player;

    public EventEndOfTurn (Player p_player)
    {
        this.m_player = p_player;
    }

    public override void Dispatch (HandlerEndOfTurn p_handler)
    {
        p_handler.HandleEndOfTurn (this);
    }

    public override Type<HandlerEndOfTurn> GetEventType ()
    {
        return TYPE;
    }

    public Player player {
        get {
            return this.m_player;
        }
    }
}
