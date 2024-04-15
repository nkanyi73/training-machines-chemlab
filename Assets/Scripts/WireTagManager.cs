using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTagManager : MonoBehaviour
{
    private bool isOtherRendererOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (gameObject.tag == "CopperChloride")
            {
                Transform liquidMaterial = other.gameObject.transform.Find("Liquid Material");
                liquidMaterial.GetComponent<MeshRenderer>().enabled = true;
                if (isOtherRendererOn)
                {
                    other.gameObject.transform.Find("White Material").GetComponent<MeshRenderer>().enabled = false;
                    isOtherRendererOn = true;
                }
            } else
            {
                Transform liquidMaterial = other.gameObject.transform.Find("White Material");
                liquidMaterial.GetComponent<MeshRenderer>().enabled = true;
                if (isOtherRendererOn)
                {
                    other.gameObject.transform.Find("Liquid Material").GetComponent<MeshRenderer>().enabled = false;
                    isOtherRendererOn = true;
                }
            }
            other.gameObject.tag = gameObject.tag.ToString();
        }
    }
}
