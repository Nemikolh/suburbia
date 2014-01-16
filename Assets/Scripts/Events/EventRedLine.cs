using UnityEngine;

public class EventRedLine : IEvent<HandlerRedLine>
{
    public static Type<HandlerRedLine> TYPE = new Type<HandlerRedLine> ();
    private readonly Player m_player;
    private readonly int m_nb_red_lines;

    public EventRedLine (Player p_player, int p_nb_red_lines)
    {
        this.m_player = p_player;
        this.m_nb_red_lines = p_nb_red_lines;
    }

    public override void Dispatch (HandlerRedLine p_handler)
    {
        p_handler.HandleRedLinePassed (this);
    }

    public override Type<HandlerRedLine> GetEventType ()
    {
        return TYPE;
    }

    public Player player {
        get {
            return this.m_player;
        }
    }

    public int nb_red_lines {
        get {
            return this.m_nb_red_lines;
        }
    }

}
