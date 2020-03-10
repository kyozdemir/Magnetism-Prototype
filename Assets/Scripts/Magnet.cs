using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    public Rigidbody rb;
    public Polarization polarization;
    public float Magnetforce;
    MagnetismEffect magnetism;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        magnetism = MagnetismEffect.instance;
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Magnet[] magnets = magnetism.FindAllMagnets();
        foreach (Magnet item in magnets)
        {
            // Check for own gameobject
            if (item.gameObject == gameObject)
            {
                continue;
            }
            PullMagnet(item);
        }
        
        
    }

    private void PullMagnet(Magnet otherMagnet)
    {
        Vector3 MagnetismForce = magnetism.CalculateGilbertForce(this, otherMagnet);
        rb.AddForce(MagnetismForce);
    }

    public enum Polarization
    {
        Negative,Positive
    }
}
