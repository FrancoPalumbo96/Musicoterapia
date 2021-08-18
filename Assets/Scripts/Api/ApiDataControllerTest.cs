using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiDataControllerTest : MonoBehaviour
{
    private ApiDataController _apiDataController;
   

    private float time;
    private String name = "pandu";
    
    private void Awake()
    {
        _apiDataController = gameObject.GetComponent<ApiDataController>();
        _apiDataController.createUser(name);
        
    }


    // Update is called once per frame
    void Update()
    {
        if (time > 3)
        {
            _apiDataController.updateUserDataPOST("KSHZ645HDQILL", "v01", "m01", 20);
            time = -1;
        }
        else if(time >= 0)
        {
            
            time += Time.deltaTime;
        }
        
    }
}
