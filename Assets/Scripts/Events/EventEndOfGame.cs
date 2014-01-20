using UnityEngine;

public class EventEndOfGame : IEvent<HandlerEndOfGame>
{
    public static Type<HandlerEndOfGame> TYPE = new Type<HandlerEndOfGame> ();

    public EventEndOfGame ()
    {
    }

    public override void Dispatch (HandlerEndOfGame p_handler)
    {
        p_handler.HandleEndOfGame (this);
    }

    public override Type<HandlerEndOfGame> GetEventType ()
    {
        return TYPE;
    }
}
