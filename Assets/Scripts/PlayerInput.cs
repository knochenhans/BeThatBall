using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float targetSpeed;
    public float forceMult;
    public float jumpImpuls;

    GameObject Forward;
    GameObject Camera;

    bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        Forward = GameObject.Find("ForwardDirection");
        Camera = GameObject.Find("OVRPlayerController");
    }

    void Update()
    {
        Vector3 targetVelocity = Forward.transform.forward * targetSpeed;
        Vector3 force = (targetVelocity - GetComponent<Rigidbody>().velocity) * forceMult;

        Vector3 targetVelocityStrafe = Camera.transform.right * targetSpeed;
        Vector3 forceStrafe = (targetVelocityStrafe - GetComponent<Rigidbody>().velocity) * forceMult;

        // Forward/backward
        GetComponent<Rigidbody>().AddForce(force * (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y));

        // Boost
        GetComponent<Rigidbody>().AddForce(force / 2 * (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger)), ForceMode.Impulse);

        // Strafing
        GetComponent<Rigidbody>().AddForce(forceStrafe * (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x));

        // Jumping
        if ((OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0.0f) && !jumping)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpImpuls * Time.deltaTime * forceMult, 0.0f), ForceMode.Impulse);
            jumping = true;
        }

        // Turning
        if (OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x != 0.0f)
        {
            Camera.transform.Rotate(new Vector3(0.0f, OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x * Time.deltaTime * 60.0f, 0.0f));
        }

        // Keyboard overrides
        if (Input.GetKey("w"))
        {
            GetComponent<Rigidbody>().AddForce(force);
        }
        if (Input.GetKey("a"))
        {
            Camera.transform.Rotate(new Vector3(0.0f, Time.deltaTime * -60.0f, 0.0f));
        }
        if (Input.GetKey("d"))
        {
            Camera.transform.Rotate(new Vector3(0.0f, Time.deltaTime * 60.0f, 0.0f));
        }
        if (Input.GetKey("s"))
        {
            GetComponent<Rigidbody>().AddForce(-1 * force);
        }
        if (Input.GetKey("f"))
        {
            Camera.transform.Rotate(new Vector3(Time.deltaTime * 60.0f, 0.0f, 0.0f));
        }
        if (Input.GetKey("t"))
        {
            Camera.transform.Rotate(new Vector3(Time.deltaTime * -60.0f, 0.0f, 0.0f));
        }
        if (Input.GetKey("q"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpImpuls * Time.deltaTime * forceMult, 0.0f), ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Ground")
            jumping = false;
    }
}