using System;


public interface HandlerEndOfGame : IHandler
{
    void HandleEndOfGame (EventEndOfGame p_event);
}


