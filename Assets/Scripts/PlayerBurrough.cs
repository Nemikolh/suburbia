using UnityEngine;
using System.Collections;

public class PlayerBurrough : MonoBehaviour {

    private Player m_player;

	// Use this for initialization
	void Start () {
	    foreach (TileInstance tile in m_player.tiles) {
            TileView.InstantiateWithParent(tile,this.transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
