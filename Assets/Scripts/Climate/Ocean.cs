using UnityEngine;

public class Ocean : MonoBehaviour
{

	public float floatMinScale;
	public float floatMaxScale;

    public int speed = 1;

    float distance;
    float startTime;

    Vector3 minScale;
    Vector3 maxScale;

    Vector3 previous;
    Vector3 target;

    bool changing;

    void Awake() {
        maxScale = new Vector3(floatMaxScale, floatMaxScale, floatMaxScale);
        minScale = new Vector3(floatMinScale, floatMinScale, floatMinScale);
    }

    public void ChangeSeaLevels(bool rise) {
        if (rise)
            target = maxScale;
        else if (!rise) {
            print("minscale");
            target = minScale;
        }

        previous = transform.localScale;  // Scale is the same on all axis
        distance = Vector3.Distance(previous, target);
        startTime = Time.time;

        changing = true;
    }

    void Update() {
        if (changing) {
            print(previous + "" + target);
            Vector3 currentScale = transform.localScale;
            float distanceCovered = (Time.time - startTime) * speed;
            float interpolate = distanceCovered / distance;
            currentScale = Vector3.Lerp(previous, target, interpolate);

            transform.localScale = currentScale;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(!collision.collider.CompareTag("Earth") && !collision.collider.CompareTag("Poles"))
            Destroy(collision.gameObject);
    }


}
