using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("The player's normal movement speed.")]
    public float normalSpeed = 3.0f;
    private float moveSpeed = 0.0f;
    [Tooltip("The player's sprint speed.")]
    public float sprintSpeed = 6.0f;

    [Tooltip("The max stamina allowed.")]
    public float maxStamina = 20.0f;  

    [Tooltip("The stamina regeneration rate.")]
    public float staminaRegenRate = 1.0f;

    private float currentStamina = 20.0f;
    private bool isSprinting = false;

    [Tooltip("Rigidbody of the player goes here.")]
    public Rigidbody playerBody;

    [Tooltip("Slider for the sprintbar goes here")]
    public Slider sprintBar;

    void Start(){
        currentStamina = maxStamina;
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;

        sprintBar.maxValue = maxStamina;
        sprintBar.value = currentStamina;
    }

    void Update()
    {
        PlayerMovement();
        Sprint();
        RegenerateStamina();
    }

    void PlayerMovement(){

        // Move forward / backward
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal;
        moveDirection.Normalize();


        // Set Rigidbody velocity
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = playerBody.velocity.y; 
        playerBody.velocity = velocity;
    }

    void Sprint(){

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0){

            moveSpeed = sprintSpeed;
            isSprinting = true; 
            currentStamina -= Time.deltaTime;

            if (currentStamina < 0)
            {
                currentStamina = 0;
                isSprinting = false;
                moveSpeed = normalSpeed;
            }
        }

        else
        {
            isSprinting = false;
            moveSpeed = normalSpeed;
        }  

        sprintBar.value = currentStamina;

    }

    void RegenerateStamina(){
        
        if (!isSprinting && currentStamina < maxStamina){
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina){
                currentStamina = maxStamina;
            }
        }
    }

}