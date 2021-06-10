using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar : MonoBehaviour
{
    public const int scale = 2;
    public const int maxHealth = 100;
    public int currentHealth = 0;
	public const int maxCleaner = 100;
	public int currentCleaner = 0;
    public AudioSource sound;

    public RectTransform HealthBar;
	public RectTransform CleanerBar;

    private float timer;
    private float timer2;
    private float timer3;
    public GameObject maskStatus;

    // Start is called before the first frame update
    void Start()
    {
        timer = timer2 = timer3 = Time.time;
        HealthBar.sizeDelta = new Vector2(0, HealthBar.sizeDelta.y);
        CleanerBar.sizeDelta = new Vector2(0, CleanerBar.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (maskStatus.GetComponent<taskStatus>().status) {
            float currentTime = Time.time;
            if (currentTime - timer > 10) {
                DecreaseHealth();
                timer = Time.time;
            }
            if (currentTime - timer2 > 10 && currentHealth <= 40) {
                Debug.Log("Low hp");
                sound.Play();
                timer2 = Time.time;
                timer3 = currentTime-5;
            }
            if (currentTime - timer3 > 10 && currentCleaner <= 20) {
                Debug.Log("Low mp");
                maskStatus.GetComponent<AudioSource>().Play();
                timer3 = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.H)) {
            DecreaseHealth();
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            IncreaseHealth();
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            DecreaseCleaner();
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            IncreaseCleaner();
        }

    }

    public void DecreaseHealth(int delta=10)
    {
        if (currentHealth > 0) {
            currentHealth = Mathf.Max(0, currentHealth - delta);

        }
        HealthBar.sizeDelta = new Vector2(currentHealth * scale, HealthBar.sizeDelta.y);
    }

    public void IncreaseHealth()
    {
        if (currentHealth < 100) {
            currentHealth = 100;
        }
        HealthBar.sizeDelta = new Vector2(currentHealth * scale, HealthBar.sizeDelta.y);
    }

    public void DecreaseCleaner()
    {
        if (currentCleaner > 0) {
            currentCleaner = Mathf.Max(0, currentCleaner - 10);
        }
        CleanerBar.sizeDelta = new Vector2(currentCleaner * scale, CleanerBar.sizeDelta.y);
    }

    public void IncreaseCleaner()
    {
        if (currentCleaner < 100) {
            currentCleaner = 100;
        }
        CleanerBar.sizeDelta = new Vector2(currentCleaner * scale, CleanerBar.sizeDelta.y);
    }
}
