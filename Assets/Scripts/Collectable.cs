using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Collectable : MonoBehaviour
{
    private float items = 0;
    public float collectableNum = 8;

    void Start(){
        winScreen.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Collectable")){
             other.gameObject.SetActive(false)
             items += 1; 

             if (items == collectableNum){
                winScreen.SetActive(true);
             }
        }
    }
}
