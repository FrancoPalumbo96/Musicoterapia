using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotation : MonoBehaviour
{
    public void RotateUp()
    {
        this.gameObject.transform.Rotate(new Vector3(20,0,0));
    }
    
    public void RotateDown()
    {
        this.gameObject.transform.Rotate(new Vector3(-20,0,0));
    }
}
