using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Light").GetComponent<Light>().color += new Color(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        GameObject.Find("Light").GetComponent<Light>().intensity += Random.Range(-0.2f, 0.2f);
        //RenderSettings.skybox.SetColor("_Tint", RenderSettings.skybox.GetColor("_Tint") + new Color(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
