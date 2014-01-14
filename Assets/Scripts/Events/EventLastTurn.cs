using UnityEngine;

public class EventLastTurn : IEvent<HandlerLastTurn>
{
    public static Type<HandlerLastTurn> TYPE = new Type<HandlerLastTurn> ();

    public EventLastTurn ()
    {
    }

    public override void Dispatch (HandlerLastTurn p_handler)
    {
        p_handler.HandleOneMoreTurnEvent (this);
    }

    public override Type<HandlerLastTurn> GetEventType ()
    {
        return TYPE;
    }
}
