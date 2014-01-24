using UnityEngine;
using System.Collections;

public class TileDescriptionMenu : EventBasedBehavior, HandlerShowTileInformation {

    private Tile m_current_tile_shown;
    private bool m_show_tile;

	// Use this for initialization
	void Start () {
        Suburbia.Bus.AddHandler(EventShowTileInformation.TYPE, this);
	}

	// Update is called once per frame
	void OnGUI () {
        if(m_show_tile)
        {
            string immediate_effect = "";
            if(m_current_tile_shown.immediate_effect != null)
            {
                immediate_effect = m_current_tile_shown.immediate_effect.resource.ToString().ToLower()
                    + " " + Util.convertToSignedStr(m_current_tile_shown.immediate_effect.value);
            }
            // Screen.width, Screen.height
            GUI.TextArea(new Rect(50,50,200,200), "\n\t\t\tTile description :\n Name :\n\t"
                         + m_current_tile_shown.name
                         + "\n Immediate Effect : \n\t" + immediate_effect
                         + "\n Stack : \n\t" + m_current_tile_shown.letter
                         + "\n Price : \n\t" + "$" + m_current_tile_shown.price
                         + "\n Color : \n\t" + m_current_tile_shown.color);

        }
	}

    public void HandleShowTileInformation(EventShowTileInformation p_event)
    {
        if(p_event.IsAskingToBeShown())
        {
            this.m_current_tile_shown = p_event.tile;
            this.m_show_tile = true;
        }
        else
        {
            if(p_event.tile == this.m_current_tile_shown)
                m_show_tile = false;
        }
    }
}
