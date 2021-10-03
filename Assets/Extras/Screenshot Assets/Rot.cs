using UnityEngine;

public class Rot : MonoBehaviour
{

    public float speed;

    void Update() {
        if (transform.rotation.y < 360) {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }

}
