using UnityEngine;
using System.Collections;

public class MainCameraTransition : ImprovedBehavior, HandlerEndOfTurn, HandlerGameStarted
{
    private static float TAN_FOVV = Mathf.Tan (Camera.main.fieldOfView);
    private static float TAN_FOVV2 = Mathf.Tan (Camera.main.fieldOfView / 2.0f);
    private bool m_go_to_second_player;
    //public Vector3 m_jump;
    private Vector3 m_origin;

    private void Start ()
    {
        m_go_to_second_player = false;
        Suburbia.Bus.AddHandler (EventEndOfTurn.TYPE, this);
        Suburbia.Bus.AddHandler (EventGameStarted.TYPE, this);
    }
    
    public void HandleEndOfTurn (EventEndOfTurn p_event)
    {
        m_go_to_second_player = !m_go_to_second_player;
        this.Delay (UpdatePositionCamera, 1);
    }

    public void HandleGameStarted (EventGameStarted p_event)
    {
        this.Delay (UpdatePositionCamera, 0.2f);
    }

    private void UpdatePositionCamera ()
    {
        GameObject burrough = Suburbia.CurrentBurrough.gameObject;
        Bounds bounds = new Bounds (burrough.transform.position, Vector3.one);
        foreach (Renderer renderer in burrough.GetComponentsInChildren<Renderer>()) {
            bounds.Encapsulate (renderer.bounds);
        }
        FocusOnBounds(bounds.size.x + 2, bounds.size.z + 2);
    }

    private void FocusOnBounds (float p_maxz, float p_maxx)
    {
        Vector3 pos;
        float y, TAN_FOVH2;
        pos = transform.localPosition;
        y = p_maxz / TAN_FOVV;
        TAN_FOVH2 = TAN_FOVV2 / Camera.main.aspect;
        y = Mathf.Max (y, p_maxx / TAN_FOVH2);
        this.transform.localPosition = new Vector3(pos.x, y, pos.z);

        if (m_go_to_second_player) {
            transform.localRotation = Quaternion.Euler (new Vector3 (0, 180, 0) 
                + new Vector3 (transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z));
        } else {
            transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0) 
                + new Vector3 (transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z));
        }
    }
}
