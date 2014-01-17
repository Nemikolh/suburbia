// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public class EventRemoveFreePositionOfPlayer : IEvent<HandlerRemoveFreePositionOfPlayer>
{
    public static Type<HandlerRemoveFreePositionOfPlayer> TYPE = new Type<HandlerRemoveFreePositionOfPlayer> ();
    private readonly Player m_player;

    public Player player {
        get {
            return this.m_player;
        }
    }

    public EventRemoveFreePositionOfPlayer (Player p_player)
    {
        m_player = p_player;
    }

    public override void Dispatch (HandlerRemoveFreePositionOfPlayer p_handler)
    {
        p_handler.HandleRemoveFreePositionOfPlayer (this);
    }
    
    public override Type<HandlerRemoveFreePositionOfPlayer> GetEventType ()
    {
        return TYPE;
    }
}


