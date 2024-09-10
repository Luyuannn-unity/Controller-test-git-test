using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    private bool holdingItem;
    private Vector2 moveInputValue;
    private GameObject heldItem;
    private Item itemScript;
    [SerializeField] private float rayDistance;
    [SerializeField] private Vector3 forward;
    private void OnMove(InputValue v)
    {
        moveInputValue = v.Get<Vector2>();
        if (moveInputValue != Vector2.zero)
        {
            // Calculate the angle to rotate based on the velocity
            float angle = Mathf.Atan2(moveInputValue.y, moveInputValue.x) * Mathf.Rad2Deg;

            // Rotate the GameObject to align its right direction to the velocity
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnInteract(InputValue v)
    {
        if (holdingItem)
        {
            DropItem();
        }
        else
        {
            Vector2 rayOrigin = transform.position; // Ray starts from the GameObject's position
            Vector2 rayDirection = transform.right; // Cast the ray in the right direction of the object

            // Perform the 2D raycast
            RaycastHit2D hit = Physics2D.CircleCast(rayOrigin, 0.5f, rayDirection, rayDistance, LayerMask.GetMask("item"));
            // Perform the raycast
            Debug.DrawRay(transform.position, transform.right * rayDistance, Color.red, 2f);
            if (hit.collider != null)
            {
                // Check if the object hit has an Item script attached
                GameObject item = hit.collider.gameObject;

                if (item != null)
                {
                    PickUpItem(item);
                }
                else
                {
                    Debug.Log("item is null");
                }
            }
            else
            {
                Debug.Log("Miss");
            }
        }
    }

    private void PickUpItem(GameObject item)
    {
        itemScript = item.GetComponent<Item>();
        speed *= itemScript.movementPenalty;
        heldItem = item;
        holdingItem = true;
        itemScript.PickUp(this.gameObject);
    }

    private void DropItem()
    {
        speed /= itemScript.movementPenalty;
        itemScript.Drop();
        itemScript = null;
        heldItem = null;
        holdingItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = moveInputValue * speed;
        forward = transform.right;
    }
}
