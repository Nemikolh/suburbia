using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
    }
    
    // Update is called once per frame
    void Update ()
    {
    }

    void OnGUI ()
    {
        if (GUI.Button (new Rect (50, 50, 200, 60), "Start Two Player Game")) {
            Suburbia.App.StartGame (2);
            Instantiate (Resources.Load ("Prefabs/RealEstateMarket"));
            this.gameObject.AddComponent<TileDescriptionMenu> ();
            Destroy(this);
        }
    }
}
