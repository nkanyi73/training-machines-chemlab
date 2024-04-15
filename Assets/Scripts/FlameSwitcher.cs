using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlameSwitcher : MonoBehaviour
{
    [Header ("Flames")]
    public VisualEffect luminousFlame;
    public VisualEffect calciumFlame;
    public VisualEffect potassiumFlame;
    public VisualEffect lithiumFlame;
    public VisualEffect copperFlame;
    public VisualEffect sodiumFlame;
    private VisualEffect effectPlaying;

    private bool isBurning;
    // Start is called before the first frame update
    void Start()
    {
        luminousFlame.Play();
        calciumFlame.Stop();
        potassiumFlame.Stop();
        copperFlame.Stop();
        lithiumFlame.Stop();
        sodiumFlame.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.layer == 6) 
        {
            if (!isBurning)
            {
                switch(other.gameObject.tag)
                {
                    case "CalciumChloride":
                        luminousFlame.Stop();
                        calciumFlame.Play();
                        isBurning = true;
                        effectPlaying = calciumFlame;
                        break;

                    case "PotassiumChloride":
                        luminousFlame.Stop();
                        potassiumFlame.Play();
                        isBurning = true;
                        effectPlaying = potassiumFlame;
                        break;

                    case "LithiumChloride":
                        luminousFlame.Stop();
                        lithiumFlame.Play();
                        isBurning = true;
                        effectPlaying = lithiumFlame;
                        break;

                    case "CopperChloride":
                        luminousFlame.Stop();
                        copperFlame.Play();
                        isBurning = true;
                        effectPlaying = copperFlame;
                        break;

                    case "SodiumChloride":
                        luminousFlame.Stop();
                        sodiumFlame.Play();
                        isBurning = true;
                        effectPlaying = sodiumFlame;
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            effectPlaying.Stop();
            isBurning = false;
            luminousFlame.Play();

            if (other.gameObject.tag == "CopperChloride")
            {
                Transform liquidMaterial = other.gameObject.transform.Find("Liquid Material");
                liquidMaterial.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                Transform liquidMaterial = other.gameObject.transform.Find("White Material");
                liquidMaterial.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
