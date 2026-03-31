using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    // Player Movement
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    public LayerMask solidObjectsLayer; // Layer in the Editor "SolidObjects"
    public LayerMask grassLayer; // Layer in the Editor "LongGrass"
    
    // Player Model Animation
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving) // If the player is currently moving, ignore input
        {
            // Read player input; GetAxisRaw always gets +1 or -1 for movement
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            // Prevent diagonal movement
            if (input.x != 0)
            {
                input.y = 0;
            }

            // Check: Did the player pressing a direction?
            if (input != Vector2.zero)
            {
                // Player Model Animation: Determine which way to face via input
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                
                // Player Movement
                var targetPos = transform.position; // Current player position
                targetPos.x += input.x; // Move target one tile in input direction
                targetPos.y += input.y; // Move target one tile in input direction
                
                // Check to see if targetPos is valid (not a solid object)
                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        
        animator.SetBool("isMoving", isMoving);
    }

    // Coroutine: Move the player from the current position -> target position over time
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true; // Prevent further input until character moves
        
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos; // Make sure the player model snaps to the target tile
        isMoving = false;
        
        // Random Encounter Logic: Called when the player is walking through long grass
        CheckForEncounters();
    }
    
    // Check to see if tile player wants to walk into is solid
    private bool IsWalkable(Vector3 targetPos)
    {
        // If there is a solid object in the targetPos, don't allow the player to move into it
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }

        // Otherwise, the targetPos is valid -> Allow player to move there
        return true;
    }

    private void CheckForEncounters()
    {
        // If there is a grass tile where the player is standing...
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if (Random.Range(1, 101) <= 10) // 10% Chance of triggering a battle in long grass
            {
                Debug.Log("Encountered an internal Saboteur!");
            }
        }
    }
    
}
