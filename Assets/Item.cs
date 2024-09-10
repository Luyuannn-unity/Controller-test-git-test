using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isHeld;
    [SerializeField] public float hp;
    [SerializeField] public float edible;
    public GameObject holder;
    [SerializeField] public float movementPenalty;
    private void Update()
    {
        if (isHeld)
        {
            transform.position = holder.transform.position + holder.transform.right * 0.8f;
        }
    }

    public void PickUp(GameObject g)
    {
        holder = g;
        isHeld = true;
    }

    public void Drop()
    {
        holder = null;
        isHeld = false;
    }
}

