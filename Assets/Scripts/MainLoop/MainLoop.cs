using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoop : MonoBehaviour
{

    public List<PowerBuilding> powerBuildings = new List<PowerBuilding>();
    [SerializeField] Population population;

    /// <summary>
    /// UI components
    /// </summary>

    public int loopTime = 1;

    public int totalPower;
    // Seconds until the update

    void Start() {
        StartCoroutine(Loop());
    }

    public void UpdatePower(int val, bool increase) {
        if (increase)
            totalPower += val;
        else
            totalPower -= val;

        population.UpdatePopulation(totalPower);
    }

    void UpdateLoop() {
        print("updateloop");
        foreach(PowerBuilding p in powerBuildings) {
            p.UpdateLoop(loopTime);
        }
        UpdateText();
        StartCoroutine(Loop());
    }

    void UpdateText() {

    }

    IEnumerator Loop() {
        yield return new WaitForSeconds(loopTime);
        UpdateLoop();
    }

}
