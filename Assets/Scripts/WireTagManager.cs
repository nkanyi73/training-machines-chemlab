using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTagManager : MonoBehaviour
{
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
                //liquidMaterial.GetComponent<MeshRenderer>().material = blueLiquidMaterial;
            } else
            {
                Transform liquidMaterial = other.gameObject.transform.Find("White Material");
                //liquidMaterial.GetComponent<MeshRenderer>().SetMaterials(whiteLiquidMaterial);
                liquidMaterial.GetComponent<MeshRenderer>().enabled = true;
            }
            other.gameObject.tag = gameObject.tag.ToString();
        }
    }
}
