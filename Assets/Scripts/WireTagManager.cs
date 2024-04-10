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
            other.gameObject.tag = gameObject.tag.ToString();
        }
    }
}
