using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskManager : MonoBehaviour
{

    public GameObject maskStatus;
    public GameObject phoneStatus;
    public GameObject doorStatus;
    public GameObject elevatorStatus;
    public GameObject cleanerStatus;

    public AudioSource sound;
    public AudioSource BGM;

    void Awake()
    {
        BGM.Play();
        Invoke("Play", 1);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            checkMask();
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            checkPhone();
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            checkDoor();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            checkEleva();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            checkCleaner();
        }
    }

    public void checkMask()
    {
        if (!maskStatus.GetComponent<taskStatus>().status)
        {
            maskStatus.GetComponent<Text>().text = "<size=24>完成</size>";
            maskStatus.GetComponent<taskStatus>().setStatus(true);
            maskStatus.GetComponent<AudioSource>().Play();
        }
    }
    public void checkPhone()
    {
        phoneStatus.GetComponent<Text>().text = "<size=24>完成</size>";
        phoneStatus.GetComponent<AudioSource>().Play();
    }
    public void checkDoor()
    {
        doorStatus.GetComponent<Text>().text = "<size=24>完成</size>";
        doorStatus.GetComponent<AudioSource>().Play();
    }
    public void checkEleva()
    {
        elevatorStatus.GetComponent<Text>().text = "<size=24>完成</size>";
        elevatorStatus.GetComponent<AudioSource>().Play();
    }
    public void checkCleaner()
    {
        cleanerStatus.GetComponent<Text>().text = "<size=24>完成</size>";
        cleanerStatus.GetComponent<taskStatus>().setStatus(true);
        cleanerStatus.GetComponent<AudioSource>().Play();
    }

    private void Play()
    {
        sound.Play();
    }

}
