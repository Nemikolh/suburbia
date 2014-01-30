// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //

public interface HandlerHandlerHasBeenDestroyed : IHandler
{
    void HandleHandlerHasBeenDestroyed (EventHandlerHasBeenDestroyed p_event);
}

public class EventHandlerHasBeenDestroyed : IEvent<HandlerHandlerHasBeenDestroyed>
{
    public static Type<HandlerHandlerHasBeenDestroyed> TYPE = new Type<HandlerHandlerHasBeenDestroyed> ();
    private readonly IHandler m_handler;

    public IHandler handler {
        get {
            return  this.m_handler;
        }
    }

    public EventHandlerHasBeenDestroyed (IHandler p_handler)
    {
        m_handler = p_handler;
    }

    public override void Dispatch (HandlerHandlerHasBeenDestroyed p_handler)
    {
        p_handler.HandleHandlerHasBeenDestroyed (this);
    }

    public override Type<HandlerHandlerHasBeenDestroyed> GetEventType ()
    {
        return TYPE;
    }

}
