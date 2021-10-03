using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour{
    public string id;
    [HideInInspector] public float stock;

    public void UpdateResources(float val, bool increase) {
        if (increase)
            stock += val;
        else
            stock -= val;
    }
}
