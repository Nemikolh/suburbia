using UnityEngine;
using System.Collections;

public class GameController : SmoothTranslation
{

    // Use this for initialization
    void Start ()
    {
    }
    
    // Update is called once per frame
    protected void Update ()
    {
        base.Update();
    }

    void OnGUI ()
    {
        Event ev = Event.current;
        if (ev.isKey) {

            switch (ev.keyCode) {
            // We move to the upper player
            case KeyCode.Z:
                this.InitWith(new Vector3(0,2.3f,0));
                break;
            // We move to the down player
            case KeyCode.S:
                this.InitWith(new Vector3());
                break;
            // We move to the left player
            case KeyCode.Q:
                break;
            // We move to the right player
            case KeyCode.D:
                break;
            default:
                break;
            }

        }
    }
}
