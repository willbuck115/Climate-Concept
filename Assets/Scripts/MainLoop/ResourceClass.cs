using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceClass : MonoBehaviour
{

    public Resource[] resources;
    Resource resourceUpdating;

    //resource text

    public void UpdateResource(string resourceID, float val, bool increase) {
        if (increase) {
            for(int i = 0; i < resources.Length; i++) {
                if(resources[i].id == resourceID) {
                    resourceUpdating = resources[i];

                    resourceUpdating.UpdateResources(val, increase);
                }
            }
        }
    }
}
