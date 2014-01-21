// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //

public interface HandlerGameStarted : IHandler
{
    void HandleGameStarted(EventGameStarted p_event);
}

public class EventGameStarted : IEvent<HandlerGameStarted>
{
    public static Type<HandlerGameStarted> TYPE = new Type<HandlerGameStarted> ();

    public EventGameStarted ()
    {

    }

    public override void Dispatch (HandlerGameStarted p_handler)
    {
        p_handler.HandleGameStarted (this);
    }

    public override Type<HandlerGameStarted> GetEventType ()
    {
        return TYPE;
    }

}
