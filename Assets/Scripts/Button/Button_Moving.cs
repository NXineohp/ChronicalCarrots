using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Moving : MonoBehaviour
{
    [Header("Target Object to Move")]
    public Transform targetObject;

    [Header("Target Position (when Hasi is on the plate)")]
    public Vector3 activatedPosition;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    private Vector3 originalPosition;
    private bool charactersOnPlate = false;

    private HashSet<Transform> riders = new HashSet<Transform>();
    private Vector3 lastTargetPosition;

    private PlatformRiderTracker riderTracker;

    private void Start()
    {
        if (targetObject != null)
        {
            originalPosition = targetObject.position;
            lastTargetPosition = targetObject.position;

            riderTracker = targetObject.GetComponent<PlatformRiderTracker>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            charactersOnPlate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            charactersOnPlate = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi") || collision.gameObject.CompareTag("Armi"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.collider.transform == targetObject)
                {
                    riders.Add(collision.transform);
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi") || collision.gameObject.CompareTag("Armi"))
        {
            riders.Remove(collision.transform);
        }
    }

    private void Update()
    {
        if (targetObject == null) return;

        Vector3 targetPos = charactersOnPlate ? activatedPosition : originalPosition;
        Vector3 oldPos = targetObject.position;
        Vector3 newPos = Vector3.MoveTowards(oldPos, targetPos, moveSpeed * Time.deltaTime);
        Vector3 movement = newPos - oldPos;

        targetObject.position = newPos;

        // Nur wenn Rider-Tracking vorhanden ist
        if (riderTracker != null)
        {
            riderTracker.MoveRiders(movement);
        }

        lastTargetPosition = targetObject.position;
    }
}
