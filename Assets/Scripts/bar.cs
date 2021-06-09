using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar : MonoBehaviour
{
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
	public const int maxCleaner = 100;
	public int currentCleaner = maxCleaner;
    
    public RectTransform HealthBar;
	public RectTransform CleanerBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            if (currentHealth > 0) {
                currentHealth = currentHealth - 10;
            }
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            if (currentHealth < 100) {
                currentHealth = 100;
            }
        }

        HealthBar.sizeDelta = new Vector2(currentHealth, HealthBar.sizeDelta.y);

        if (Input.GetKeyDown(KeyCode.G)) {
            if (currentCleaner > 0) {
                currentCleaner = currentCleaner - 10;
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            if (currentCleaner < 100) {
                currentCleaner = 100;
            }
        }

        CleanerBar.sizeDelta = new Vector2(currentCleaner, CleanerBar.sizeDelta.y);
    }
}
