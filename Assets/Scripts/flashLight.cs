using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashLight : MonoBehaviour
{
    [Tooltip("The spotlight for the flashlight")]
    public Light spotlight;
    [Tooltip("How long the enemy is stunned")]
    public float freezeDuration = 100f;
    [Tooltip("The enemy's Rigidbody (make sure it is tagged 'enemy')")]
    public Rigidbody enemyBody;
    [Tooltip("The battery duration when filled with new battery")]
    public float fullBattery = 400f;
    [Tooltip("The battery duration. use this to set a start duration.")]
    public float batteryLife = 400f;
    private bool flashLightOn;
    [Tooltip("UI text to show when the player can collect the item")]
    public GameObject grabBattTextUI; 
    [Tooltip("Maximum distance to interact with the battery")]
    public float interactionDistance = 5.0f;
    [Tooltip("Slider for the battery life bar goes here")]
    public Slider battBar;
    [Tooltip("how fast the battery drains")]
    public float drainSpeed = 1;

    private bool _isStunned;

    public bool isStunned{
        get {return _isStunned;}
        set {_isStunned = value;}
    }

    void Start()
    {
        if (grabBattTextUI != null)
        {
            grabBattTextUI.SetActive(false); 
            }

        // Initialize the slider
        if (battBar != null)
        {
            battBar.maxValue = fullBattery;
            battBar.value = batteryLife;
            }
            
        flashLightOn = false;
        spotlight.enabled = false;
    }

    void Update()
    {
        interactions();        
    }


    void flashlightToggle()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Use 'F' to toggle the flashlight
        {
            flashLightOn = !flashLightOn; 
            spotlight.enabled = flashLightOn; 
        }
    }

    void interactions(){
        flashlightToggle();
        enemyDetected();
        batteryUsage();
        lookingAtBattery();
    }

    // Coroutine to stun the enemy
    private IEnumerator stun()
    {
        isStunned = true;
        yield return new WaitForSeconds(freezeDuration);
        isStunned = false;
    }

    // Detect an enemy when the flashlight is on
    void enemyDetected()
    {
        if (flashLightOn && Input.GetMouseButtonDown(0)) // Left mouse button while flashlight is on
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("enemy"))
                {
                    StartCoroutine(stun());
                }
            }
        }
    }

    
    void batteryUsage()
    {
        if (flashLightOn)
        {
            batteryLife -= Time.deltaTime * drainSpeed;

            if (batteryLife <= 0)
            {
                flashLightOn = false;
                spotlight.enabled = false; 
            }
            battBar.value = batteryLife;
        }
    }


    void grabBatt(GameObject other){
        batteryLife = fullBattery;
        other.gameObject.SetActive(false); 

        if (grabBattTextUI != null)
        {
            grabBattTextUI.SetActive(false); 
        }
    }

    void lookingAtBattery()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform.CompareTag("battery"))
            {
                if (grabBattTextUI != null)
                {
                    grabBattTextUI.SetActive(true); // Show the UI text when in range and looking at the object
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    grabBatt(hit.transform.gameObject); 
                }
            }
            else
            {
                if (grabBattTextUI != null)
                {
                    grabBattTextUI.SetActive(false); // Hide the UI text if not looking at the object
                }
            }
        }
        else
        {
            if (grabBattTextUI != null)
            {
                grabBattTextUI.SetActive(false); // Hide the UI text if nothing is being hit
            }
        }
    }
}

