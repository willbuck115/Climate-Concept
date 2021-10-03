using System.Collections;
using UnityEngine;

public class TurbineSpin : MonoBehaviour
{

    public float speed;
    bool update;

    Vector3 direction;

    void Start() {
        StartCoroutine(Count());
        if (name == "Mill")
            direction = Vector3.forward;
        if (name.Contains("Turbine")) {
            direction = Vector3.right;
        }
        print(direction);

    }

    void Update() {
        if (update)
            transform.Rotate(direction * speed * Time.deltaTime);
    }

    IEnumerator Count() {
        yield return new WaitForSeconds(Random.Range(0.0f, 2.0f));
        update = true;
        print(update);
    }
    
    private void Srar() {
        if (1 == 1){

        }
    }
}
