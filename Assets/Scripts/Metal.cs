using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    Rigidbody rb;
    MagnetismEffect magnetism;
    public float magnetForce = 5;
    // Start is called before the first frame update
    void Start()
    {
        magnetism = MagnetismEffect.instance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Magnet[] magnets = magnetism.FindAllMagnets();
        foreach (Magnet item in magnets)
        {
            MagneticPulse(item);
        }
    }

    void MagneticPulse(Magnet magnet)
    {
        Vector3 pulse = magnetism.CalculateGilbertForce(this, magnet);
        rb.AddForce(pulse);
    }
}
