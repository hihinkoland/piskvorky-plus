using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        if (bounds == null)
            bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(5, 5, 0));
		(Vector3 center, float size) = CalculateCameraSize();
        Camera.main.transform.position = center;
        Camera.main.orthographicSize = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public (Vector3 center, float size) CalculateCameraSize()
    {
        foreach (Collider2D col in FindObjectsOfType<Collider2D>()) bounds.Encapsulate(col.bounds);

        bounds.Expand(1f);

        float vertical = bounds.size.y;
		float horizontal = bounds.size.x / Camera.main.aspect;

        float size = Mathf.Max(horizontal, vertical) * 0.5f;
        Vector3 center = bounds.center + new Vector3(0, 0, -10);
        return (center, size);
    }
}