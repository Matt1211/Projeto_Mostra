using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public float yOffset;
    public Transform target;
    public Camera cam; // Reference to the Camera component
    public float zoomLevel = 5f; // Adjust this value to set the zoom level

    private void Start()
    {
        cam = GetComponent<Camera>(); // Get the Camera component
        cam.orthographicSize = zoomLevel; // Set the zoom level
    }

    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
