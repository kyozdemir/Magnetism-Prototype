using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetismEffect : MonoBehaviour
{
    public static MagnetismEffect instance = null;


    public const float Permeability = 0.05f;

    // Singleton 
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public Magnet[] FindAllMagnets()
    {
        return FindObjectsOfType<Magnet>();
        
    }
    public Metal[] FindAllMetals()
    {
        return FindObjectsOfType<Metal>();
    }
    /* Force between two magnetic poles: F =  μ * qm1 * qm2 / 4 * PI * r^2  */
    public Vector3 CalculateGilbertForce(Magnet magnet1, Magnet magnet2)
    {
        Vector3 magnet1Position = magnet1.transform.position;
        Vector3 magnet2Position = magnet2.transform.position;

        Vector3 distanceInVector = magnet2Position - magnet1Position;
        float distance = distanceInVector.magnitude;

        float topSide = Permeability * magnet1.Magnetforce * magnet2.Magnetforce; // Upper side of division: μ * qm1 * qm2
        float bottomSide = 4 * Mathf.PI * distance;                               // Bottom side of division:  4 * PI * r^2

        float force = (topSide / bottomSide);

        if (magnet1.polarization == magnet2.polarization)
        {
            float temp = force;
            force = -temp;
        }

        return force * distanceInVector.normalized;

    }
    // An overload method for Metals
    /* Force between two magnetic poles: F =  μ * qm1 * qm2 / 4 * PI * r^2  */
    public Vector3 CalculateGilbertForce(Metal metal, Magnet magnet2)
    {
        Vector3 metalPosition = metal.transform.position;
        Vector3 magnet2Position = magnet2.transform.position;

        Vector3 distanceInVector = magnet2Position - metalPosition;
        float distance = distanceInVector.magnitude;

        float topSide = Permeability * metal.magnetForce * magnet2.Magnetforce; // Upper side of division: μ * qm1 * qm2
        float bottomSide = 4 * Mathf.PI * distance;                               // Bottom side of division:  4 * PI * r^2

        float force = (topSide / bottomSide);

        

        return force * distanceInVector.normalized;

    }
    
    
}
