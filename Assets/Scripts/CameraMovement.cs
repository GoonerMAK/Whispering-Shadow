using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private Vector3 dragOrigin;

    [SerializeField]
    private float zoomStep, miniCamSize, maxCamSize;

    //[SerializeField]
    //private Collider2D collider;

    //private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    //private void
    //{
    //    mapMinX = collider.transform.position.x - collider.bounds.size.x / 2f;
    //    mapMaxX = collider.transform.position.x + collider.bounds.size.x / 2f;

    //    mapMinY = collider.transform.position.y - collider.bounds.size.y / 2f;
    //    mapMaxY = collider.transform.position.y + collider.bounds.size.y / 2f;
    //}

    private void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position += difference;
        }

    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, miniCamSize, maxCamSize);

        //cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, miniCamSize, maxCamSize);

        //cam.transform.position = ClampCamera(cam.transform.position);
    }

    //private Vector3 ClampCamera(Vector3 targetPosition)
    //{
    //    float camHeight = cam.orthographicSize;
    //    float camWidth = cam.orthographicSize * cam.aspect;

    //    float minX = mapMinX + camWidth;
    //    float maxX = mapMaxX - camWidth;
    //    float minY = mapMinY + camHeight;
    //    float maxY = mapMaxY - camHeight;

    //    float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
    //    float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

    //    return new Vector3(newX, newY, targetPosition.z);
    //}
}
