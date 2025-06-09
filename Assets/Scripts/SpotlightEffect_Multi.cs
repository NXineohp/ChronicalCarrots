using UnityEngine;

public class SpotlightEffect_Multi : MonoBehaviour
{
    public Transform hasi;     // Spieler 1
    public Transform armi;     // Spieler 2
    public Material spotlightMat;
    public Camera cam;

    public float visibleRadius = 0.3f;
    public float hiddenRadius = 999f;
    public float lerpSpeed = 2f;

    private float radius1;
    private float radius2;
    private bool deactive = true;

    public void SetActive(bool value)
    {
        deactive = value;
    }

    void Start()
    {
        radius1 = hiddenRadius;
        radius2 = hiddenRadius;

        spotlightMat.SetFloat("_Radius1", radius1);
        spotlightMat.SetFloat("_Radius2", radius2);
        spotlightMat.SetVector("_Center1", new Vector4(0.5f, 0.5f, 0, 0));
        spotlightMat.SetVector("_Center2", new Vector4(0.5f, 0.5f, 0, 0));
    }

    void Update()
    {
        if (!cam || !spotlightMat || !hasi || !armi) return;

        Vector3 view1 = cam.WorldToViewportPoint(hasi.position);
        Vector3 view2 = cam.WorldToViewportPoint(armi.position);

        spotlightMat.SetVector("_Center1", new Vector4(view1.x, view1.y, 0, 0));
        spotlightMat.SetVector("_Center2", new Vector4(view2.x, view2.y, 0, 0));

        float targetRadius = deactive ? visibleRadius : hiddenRadius;

        radius1 = Mathf.Lerp(radius1, targetRadius, Time.deltaTime * lerpSpeed);
        radius2 = Mathf.Lerp(radius2, targetRadius, Time.deltaTime * lerpSpeed);

        spotlightMat.SetFloat("_Radius1", radius1);
        spotlightMat.SetFloat("_Radius2", radius2);
    }
}
