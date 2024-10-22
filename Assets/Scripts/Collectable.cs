using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Collectable : MonoBehaviour
{
    [Tooltip("UI text to show when the player can collect the item")]
    public GameObject collectTextUI; 
    private float items = 0;
    [Tooltip("Maximum distance to interact with the collectable")]
    public float interactionDistance = 3.0f;
    [Tooltip("UI text to show when the player can collect the item")]
    public GameObject WinScreen;
    public float numOfCollectables = 8;

    void Start()
    {
        if (collectTextUI != null)
        {
            collectTextUI.SetActive(false); 
        }

        WinScreen.SetActive(false);
    }

    void Update()
    {
        lookingAtCollectable();
        CollectableCount(); 
    }

    void FixedUpdate(){
        
    }

    void CollectableCount(){
        if (items == numOfCollectables){
            WinScreen.SetActive(true);
        }
    }

    void PickUp()
    {
        items += 1; 
        if (gameObject.CompareTag("Collectable")) 
       {
           gameObject.SetActive(false);
       } 

        if (collectTextUI != null)
        {
            collectTextUI.SetActive(false); 
        }
    }

    void lookingAtCollectable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform == transform)
            {
                if (collectTextUI != null)
                {
                    collectTextUI.SetActive(true); // Show the UI text when in range and looking at the object
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUp(); 
                }
            }
            else
            {
                if (collectTextUI != null)
                {
                    collectTextUI.SetActive(false); // Hide the UI text if not looking at the object
                }
            }
        }
        else
        {
            if (collectTextUI != null)
            {
                collectTextUI.SetActive(false); // Hide the UI text if nothing is being hit
            }
        }
    }
}