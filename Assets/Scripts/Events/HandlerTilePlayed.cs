using System;

public interface HandlerTilePlayed : IHandler
{
    void HandleTilePlayed(EventTilePlayed p_event);
}
