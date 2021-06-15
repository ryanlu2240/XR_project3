using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showbox : MonoBehaviour
{
	public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void tt(){
    	Vector3 pos = gameObject.transform.position;
    	pos.y -= 0.09f;
    	// Vector3 rot = gameObject.transform.rotation.eulerAngles;
    	if(GameObject.Find("Box(Clone)")==null){
    		Instantiate(box, pos, gameObject.transform.rotation);
    	    gameObject.SetActive(false);
    	}

    }
}
