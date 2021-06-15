using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderShow : MonoBehaviour
{
    public GameObject myPrefab;
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
        GameObject t = GameObject.Find("OnHandVirus(Clone)");
        if(other.gameObject.name=="GroupOfVirus" && t==null){
            Debug.Log(t);
            Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    	if(other.gameObject.name=="Cube" && t==null){
            Debug.Log(t);
    		Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    	}
        
    }
}
