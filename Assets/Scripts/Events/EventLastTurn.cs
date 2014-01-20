using UnityEngine;

public class EventLastTurn : IEvent<HandlerLastTurn>
{
    public static Type<HandlerLastTurn> TYPE = new Type<HandlerLastTurn> ();
    private readonly Player m_player;

    public EventLastTurn (Player p_player)
    {
        this.m_player = p_player;
    }


    public EventLastTurn ()
    {
    }

    public override void Dispatch (HandlerLastTurn p_handler)
    {
        p_handler.HandleOneMoreTurn (this);
    }

    public override Type<HandlerLastTurn> GetEventType ()
    {
        return TYPE;
    }

    public Player player {
       get {
            return this.m_player;
        }
    }
}
