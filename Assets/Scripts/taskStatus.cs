using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskStatus : MonoBehaviour
{
	public bool status = false;
	// public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	public void setStatus(bool st)
	{
		status = st;
	}

	public void Play()
	{
		GetComponent<AudioSource>().Play();
	}

}
