using UnityEngine;

public class EventEndOfTurn : IEvent<HandlerEndOfTurn>
{
    public static Type<HandlerEndOfTurn> TYPE = new Type<HandlerEndOfTurn> ();

    public EventEndOfTurn ()
    {
    }

    public override void Dispatch (HandlerEndOfTurn p_handler)
    {
        p_handler.HandleEndOfTurn (this);
    }

    public override Type<HandlerEndOfTurn> GetEventType ()
    {
        return TYPE;
    }
}
