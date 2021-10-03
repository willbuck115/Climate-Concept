using UnityEngine;

public class CameraMotor : MonoBehaviour {

    Camera cam;
    float distanceToTarget = 20;
    Vector3 previousPosition;

    Transform earth;
    Transform target;

    public float scrollSensitivity;
    public float maxDistance;

    void Awake() {
        cam = GetComponent<Camera>();
        earth = GameObject.Find("Earth").transform;
        target = earth;

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        UpdateCamera();
    }

    void Update() {
        Cursor.lockState = CursorLockMode.None;
        if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1)) { // right click is for mac support.
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        } else if (Input.GetMouseButton(2) || Input.GetMouseButton(1)) { // right click for mac trackpad.
            Cursor.lockState = CursorLockMode.Confined;
            UpdateCamera();
        }
        /*
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            Debug.Log("raycast");

            if(Physics.Raycast(ray, out raycastHit)) {
                target = raycastHit.collider.transform;
                UpdateCamera();
            }
        }
        */

        float fov = cam.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -scrollSensitivity;
        fov = Mathf.Clamp(fov, distanceToTarget, maxDistance);
        Camera.main.fieldOfView = fov;
    }

    void UpdateCamera() {
        Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = previousPosition - newPosition;

        float rotationAroundYAxis = -direction.x * 360; // camera moves horizontally
        float rotationAroundXAxis = direction.y * 360; // camera moves vertically

        cam.transform.position = target.position;

        cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }
}