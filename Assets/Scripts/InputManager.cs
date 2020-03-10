using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    //All input processes are here include Restart button.
    bool newTouchAvaliable;
    [SerializeField]
    float forceMultiplier = 20f;
    Vector3 touchPosition;
    Touch touch;
    GameObject selectedMagnet;
    
    // Start is called before the first frame update
    void Start()
    {
        newTouchAvaliable = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            //Just one magnet you can pull
            touch = Input.GetTouch(0);
            
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Select();
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Dragging");
                    Drag(touchPosition);
                    break;
                case TouchPhase.Stationary:
                    
                    break;
                case TouchPhase.Ended:
                    Drop();
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
    }
    //if the touch input is ended, leave the magnet alone. You can't drag magnets while the flag is true
    private void Drop()
    {
        newTouchAvaliable = true;
        selectedMagnet.GetComponent<Rigidbody>().useGravity = true;
        
    }
    //Dragging Selected Magnet
    private void Drag(Vector3 target)
    {
        if (newTouchAvaliable == false && selectedMagnet != null)
        {
            
            Vector3 direction = selectedMagnet.transform.position - target;
            float distance = direction.magnitude;
            Debug.Log("Distance: " + distance);
            
            Vector3 force = -direction.normalized * forceMultiplier;
            Debug.Log("Force: " + force);
            selectedMagnet.GetComponent<Rigidbody>().AddForce(force.x,0f,force.z,ForceMode.Acceleration);
        }
    }
    //Selecting only magnets
    private void Select()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit);
        
        if (hit.collider != null && hit.collider.tag == "Draggable")
        {
            newTouchAvaliable = false;
            Debug.Log(hit.collider.name);
            selectedMagnet = hit.collider.gameObject;
            
            selectedMagnet.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
