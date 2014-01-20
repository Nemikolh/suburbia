using UnityEngine;
using System.Collections;

public class MainCameraTransition : MonoBehaviour, HandlerEndOfTurn
{

    private bool m_go_to_second_player;
    public Vector3 m_jump;
    private Vector3 m_origin;

    private void Start ()
    {
        m_go_to_second_player = false;
        Suburbia.Bus.AddHandler (EventEndOfTurn.TYPE, this);
    }
    
    public void HandleEndOfTurn (EventEndOfTurn p_event)
    {
        m_go_to_second_player = !m_go_to_second_player;

        if (m_go_to_second_player) {
            m_origin = transform.position;
            transform.position += m_jump;
            transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0) 
                + new Vector3 (transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z));
        } else {
            m_origin = transform.position;
            transform.position += m_jump;
            transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0) 
                + new Vector3 (transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z));
        }

        SmoothTranslation trans = this.GetComponent<SmoothTranslation>();
        if(trans != null)
            Destroy(trans);

        trans = this.gameObject.AddComponent<SmoothTranslation>();
        trans.InitWith(m_origin);
        trans.StartTransition ();
    }
}
