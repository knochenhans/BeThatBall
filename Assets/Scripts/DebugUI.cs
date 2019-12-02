using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    bool inMenu;
    string text;
    Vector3 offset;
    GameObject Sphere;

    float refreshTime = 0.0f;

    string debugText;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = DebugUIBuilder.instance.AddLabel("Test");
        rt.sizeDelta = new Vector2(1000, 300);
        DebugUIBuilder.instance.GetComponentInChildren<Text>().alignment = TextAnchor.UpperLeft;
        DebugUIBuilder.instance.GetComponentInChildren<Text>().fontSize = 16;
        DebugUIBuilder.instance.GetComponentInChildren<Text>().resizeTextForBestFit = false;
        DebugUIBuilder.instance.Show();

        //inMenu = true;

        Sphere = GameObject.Find("Sphere");

        offset = transform.position - Sphere.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu) DebugUIBuilder.instance.Hide();
            else DebugUIBuilder.instance.Show();
            inMenu = !inMenu;
        }*/

        if (refreshTime >= 0.3f)
        {
            debugText += "OVR forward: " + GameObject.Find("OVR").transform.forward + "\n";
            debugText += "OVRPlayerController forward: " + GameObject.Find("OVRPlayerController").transform.forward + "\n";
            debugText += "OVRCameraRig forward: " + GameObject.Find("OVRCameraRig").transform.forward + "\n";
            debugText += "OVRCameraRig right: " + GameObject.Find("OVRCameraRig").transform.right + "\n";
            debugText += "ForwardDirection: " + GameObject.Find("ForwardDirection").transform.forward + "\n";


            DebugUIBuilder.instance.GetComponentInChildren<Text>().text = debugText;
            debugText = "";
            refreshTime = 0.0f;
        }
        else
        {
            refreshTime += Time.deltaTime;
        }
        DebugUIBuilder.instance.Show();
    }

    void LateUpdate()
    {
        transform.position = Sphere.transform.position + offset;
    }
}
