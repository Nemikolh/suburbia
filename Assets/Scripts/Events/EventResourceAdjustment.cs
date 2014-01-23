using UnityEngine;

public class EventResourceAdjustment : IEvent<HandlerResourceAdjustment>
{
    public static Type<HandlerResourceAdjustment> TYPE = new Type<HandlerResourceAdjustment> ();
    private int m_value;
    private ETileResource m_resource;
    private Player m_player;
    private TileInstance m_tile;

    public EventResourceAdjustment (Player p_player, ETileResource p_resource, int p_value, TileInstance p_tile)
    {
        m_player = p_player;
        m_resource = p_resource;
        m_value = p_value;
        m_tile = p_tile;
    }

    public override void Dispatch (HandlerResourceAdjustment p_handler)
    {
        p_handler.HandleResourceAdjustment (this);
    }

    public override Type<HandlerResourceAdjustment> GetEventType ()
    {
        return TYPE;
    }

    public int value {
        get {
            return this.m_value;
        }
    }

    public ETileResource resource {
        get {
            return this.m_resource;
        }
    }

    public Player player {
        get {
            return this.m_player;
        }
    }

}
