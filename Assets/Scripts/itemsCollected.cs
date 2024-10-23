using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemsCollected : MonoBehaviour
{
    public Collectable collectable;
    public float numOfCollectables = 8;

    [Tooltip("UI to show player then won")]
    public GameObject WinScreen;

    

    void FixedUpdate(){
        CollectableCount();
    }

    void CollectableCount(){
        if (collectable != null && collectable.items == numOfCollectables){
            WinScreen.SetActive(true);
        }
    }
}
