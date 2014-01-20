using System;


public interface HandlerEndOfTurn : IHandler
{
    void HandleEndOfTurn (EventEndOfTurn p_event);
}


