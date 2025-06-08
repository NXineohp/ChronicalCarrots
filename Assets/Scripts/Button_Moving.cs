using System;
using UnityEngine;

public class PressurePlateMover : MonoBehaviour
{
    [Header("Target Object to Move")]
    public Transform targetObject;

    [Header("Target Position (when Hasi is on the plate)")]
    public Vector3 activatedPosition;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    private Vector3 originalPosition;
    private bool charactersOnPlate = false;

    private void Start()
    {
        if (targetObject != null)
        {
            Debug.Log("Target object found: " + targetObject.name);
            originalPosition = targetObject.position;
        }
        else
        {
            Debug.LogWarning("PressurePlateMover: targetObject is not assigned!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            Debug.Log("Hais");
            charactersOnPlate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            Debug.Log("Hais");
            charactersOnPlate = false;
        }
    }

    private void Update()
    {
        if (targetObject == null) return;
        Debug.Log("UPDATING");
        Vector3 targetPos = charactersOnPlate ? activatedPosition : originalPosition;

        targetObject.position = Vector3.MoveTowards(
            targetObject.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );
    }
}
