using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider");
        Debug.Log(other);
        Debug.Log(other.gameObject.name);
    	if(other.gameObject.name=="Cube"){
    		ShowCube.show=1;
    	}
        
    }
}
