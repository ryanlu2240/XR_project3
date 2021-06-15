using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneVirus : MonoBehaviour
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
        GameObject t;
        t = GameObject.Find("clonevirus(Clone)");
    	if(other.gameObject.name=="Box" && t==null){
    		Instantiate(myPrefab, gameObject.transform.position, Quaternion.identity);
    	}
        
    }
}
