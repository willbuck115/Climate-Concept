using System.Collections;
using UnityEngine;

public class IceCaps : MonoBehaviour
{

    public GameObject north;
    public GameObject south;
    SkinnedMeshRenderer northSMR;
    SkinnedMeshRenderer southSMR;

    bool melting;
	bool freezing;
    bool update;

    int interpolation;

    int stage = 0;
    int targetStage;
    // needs to be saved

    public float minutes;
    public int sensitivity;

	public bool Melting {
        get {
            return melting;
        }
        set {
            melting = value;
            freezing = !value;
        }
    }
    public bool Freezing {
        get {
            return freezing;
        }
        set {
            freezing = value;
            melting = !value;
        }
    }

    void Awake() {
        northSMR = north.GetComponent<SkinnedMeshRenderer>();
        southSMR = south.GetComponent<SkinnedMeshRenderer>();

        InvokeRepeating("CustomUpdate", 1, 1);
    }

    public void TemperatureUpdate(bool increasing, int target) {
        if(increasing) {
            Melting = true;
        }
        else {
            Freezing = true;
        }
        targetStage = target;
        StartCoroutine(Count());
    }

    void CustomUpdate() {
        if (stage == 5)
            stage = 4;
        if (update) {
            if (melting) {
                interpolation = interpolation + (1 * sensitivity);
            } 
            else if (freezing) {
                interpolation = (int)northSMR.GetBlendShapeWeight(stage) - (1 * sensitivity);
                if (interpolation < 0)
                    interpolation = 0;
            }

            if(stage == 0 && freezing) {
                interpolation = 0;
                return;
            }
            if(stage == 5 && melting) {
                interpolation = 0;
                return;
            }

            northSMR.SetBlendShapeWeight(stage, interpolation);
            southSMR.SetBlendShapeWeight(stage, interpolation);

            if(interpolation >= 100) {
                update = false;
                interpolation = 0;
                if (stage < targetStage)
                    StartCoroutine(Count());
                if (freezing && stage > 0) {
                    stage--;
                }
                if (melting && stage < 5)
                    stage++;
            } 
            if(interpolation == 0 && freezing) {
                update = false;
                interpolation = 0;
                southSMR.SetBlendShapeWeight(stage + 1, interpolation);
                if (stage < targetStage)
                    StartCoroutine(Count());
                if (freezing && stage > 0) {
                    stage--;
                }
                if (melting && stage < 5)
                    stage++;
            }
                
        }
    }

    IEnumerator Count() {
        yield return new WaitForSeconds(minutes * 60);
        update = true;
    }

}
