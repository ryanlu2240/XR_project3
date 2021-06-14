using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Virus : MonoBehaviour
{
    public GameObject taskStatus=null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        if (taskStatus != null) {
            taskStatus.GetComponent<Text>().text = "<size=24>完成</size>";
            taskStatus.GetComponent<taskStatus>().setStatus(true);
            taskStatus.GetComponent<taskStatus>().Play();
        }
        else
        {
            Debug.Log("null task");
        }
    }
}
