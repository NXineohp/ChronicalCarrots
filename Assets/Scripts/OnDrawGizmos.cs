using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonColliderVisualizer : MonoBehaviour
{
    public Color gizmoColor = new Color(0f, 1f, 0f, 0.25f); // Transparentes Grün

    private void OnDrawGizmos()
    {
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        if (collider == null || !collider.enabled) return;

        Gizmos.color = gizmoColor;

        for (int p = 0; p < collider.pathCount; p++)
        {
            Vector2[] points = collider.GetPath(p);
            Vector3[] worldPoints = new Vector3[points.Length];

            for (int i = 0; i < points.Length; i++)
                worldPoints[i] = transform.TransformPoint(points[i]);

            Gizmos.DrawLineStrip(worldPoints, true);
        }
    }
}
