using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskStatus : MonoBehaviour
{
	public Text maskStatus;
	public Text phoneStatus;
	public Text doorStatus;
	public Text elevatorStatus;
	public Text wallStatus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
        	maskStatus.text = "<size=24>完成</size>";
        }
        if (Input.GetKeyDown(KeyCode.P)) {
        	phoneStatus.text = "<size=24>完成</size>";
        }
        if (Input.GetKeyDown(KeyCode.D)) {
        	doorStatus.text = "<size=24>完成</size>";
        }
        if (Input.GetKeyDown(KeyCode.E)) {
        	elevatorStatus.text = "<size=24>完成</size>";
        }
        if (Input.GetKeyDown(KeyCode.W)) {
        	wallStatus.text = "<size=24>完成</size>";
        }
    }
}
