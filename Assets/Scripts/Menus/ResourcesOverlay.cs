// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;

public class ResourcesOverlay : MonoBehaviour, HandlerGameStarted, HandlerResourceAdjustment
{
    private bool m_show;
    private string m_description;

    private void Start ()
    {
        m_show = false;
        Suburbia.Bus.AddHandler (EventGameStarted.TYPE, this);
        Suburbia.Bus.AddHandler (EventResourceAdjustment.TYPE, this);
    }

    public void HandleGameStarted (EventGameStarted p_event)
    {
        m_show = true;
        Refresh ();
    }

    private void Refresh ()
    {
        m_description = "\n\t\t\tGame Status :\n\n";
        foreach (Player player in Suburbia.Manager.players) {
            m_description += "\n Player name : \t" + player.name;
            m_description += "\n\tIncome : \t\t\t" + Util.convertToSignedStr (player.income);
            m_description += "\n\tReputation : \t\t" + Util.convertToSignedStr (player.reputation);
            m_description += "\n\tMoney : \t\t$ " + player.money;
            m_description += "\n\tPopulation : \t\t" + player.population;
        } 
    }

    public void HandleResourceAdjustment (EventResourceAdjustment p_event)
    {
        Refresh ();
    }

    private void OnGUI ()
    {
        if (m_show) {
            // Screen.width, Screen.height
            GUI.TextArea (new Rect (Screen.width - 200 - 50, 50, 200, 220), m_description);
        }
    }
}


