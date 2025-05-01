using System;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Console.WriteLine("COLLISION");
        // Überprüfen, ob das Objekt, das die Kollision verursacht hat, den Tag "Armi" hat
        if (collision.gameObject.CompareTag("Armi"))
        {
            Console.WriteLine("BREAKING BREAKING BREAKING");
            // Lösche das Objekt
            Destroy(gameObject);
        }
    }
}