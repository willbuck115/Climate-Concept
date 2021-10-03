using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBuilding : MonoBehaviour {

    Climate climate;
    ResourceClass resource;
    PlayerBank bank;
    MainLoop main;

    public string resourceID;
    public float carbonValuePerMin;
    public int powerOutput;
    public float resourceDepletionPerMin;
    public float maintenanceCostPerMin;

    void Start() {
        climate = FindObjectOfType<Climate>();
        resource = FindObjectOfType<ResourceClass>();
        bank = FindObjectOfType<PlayerBank>();
        main = FindObjectOfType<MainLoop>();

        main.powerBuildings.Add(this);

        main.UpdatePower(powerOutput, true);
    }

    public void UpdateLoop(int counter) {

        float carbon = (carbonValuePerMin / 60) * counter;
        float cost = (maintenanceCostPerMin / 60) * counter;
        float resources = (resourceDepletionPerMin / 60) * counter;

        if (carbonValuePerMin > 0) {
            climate.UpdateEmissions(carbon, true);
        }
        
        if (resourceDepletionPerMin > 0) {
            resource.UpdateResource(resourceID, resources, true);
        }

        bank.ChangeBalance(-cost);
    }
}
