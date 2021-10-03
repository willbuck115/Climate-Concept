using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour
{

	[SerializeField]int population = 58;
	int populationMaximum = 8000000; // 8 mil with full land capacity || 3.7 mil at lower land capacity

    int populationPerPowerPoint = 436;

    public void UpdatePopulation(int totalPowerPoints) {
        population = populationPerPowerPoint * totalPowerPoints;

        if (population > populationMaximum)
            population = populationMaximum;

        if (population <= 0) {
            // GAME OVER
        }

        UpdateText();
    }

    void UpdateText() {

    }
}
