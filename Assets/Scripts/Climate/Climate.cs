using UnityEngine;

public class Climate : MonoBehaviour
{

	IceCaps iceControl;
	Ocean ocean;

	float carbonUnits;


	float climatePoints = 100;
	// 100 points = 0 temp increase
	// 80 = .5 temp increase
	// 60 = 1 temp increase
	// 40 = 1.5 temp increase
	// 20 = 2 temp increase
	// 0 = breaking point ;)

	bool tempIncreasing;

	public float globalTempIncrease;

	public float GlobalTempIncrease {
        get {
			float temp = 0;
			if(climatePoints > 80) {
				temp = 0;
            } else if(climatePoints > 60) {
				temp = .5f;
            } else if(climatePoints > 40) {
				temp = 1f;
            } else if(climatePoints > 20) {
				temp = 1.5f;
            } else if(climatePoints > 0) {
				temp = 2f;
            } else if(climatePoints <= 0) {
				temp = 2.5f;
            }

			globalTempIncrease = temp;
			return temp;
		}
    }

    void Awake() {
		iceControl = GetComponent<IceCaps>();
		ocean = FindObjectOfType<Ocean>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
			UpdateClimatePoints(-21f);
        }
		if (Input.GetKeyDown(KeyCode.CapsLock)) {
			UpdateClimatePoints(21f);
		}
	}

    public void UpdateClimatePoints(float p) {
		climatePoints = climatePoints + p;
		if (p > 0)
			tempIncreasing = false;
		else if (p < 0)
			tempIncreasing = true;
		UpdateChanges();
    }

	void UpdateChanges() {
        switch (GlobalTempIncrease) {
			case 0:
				if(!tempIncreasing)
					iceControl.TemperatureUpdate(tempIncreasing, 1);
				break;
			case .5f:
				if (!tempIncreasing)
					ocean.ChangeSeaLevels(false);
				iceControl.TemperatureUpdate(tempIncreasing, 0);
				break;
			case 1f:
				// melt or freeze to stage 2
				iceControl.TemperatureUpdate(tempIncreasing, 1);
				break;
			case 1.5f:
				// melt or freeze to stage 3
				if (!tempIncreasing)
					ocean.ChangeSeaLevels(false);
                else
					ocean.ChangeSeaLevels(true);
				iceControl.TemperatureUpdate(tempIncreasing, 2);
				break;
			case 2f:
				iceControl.TemperatureUpdate(tempIncreasing, 3);
				break;
			case 2.5f:
				iceControl.TemperatureUpdate(tempIncreasing, 4);
				break;
        }
    }

    public void UpdateEmissions(float carbonValue, bool increase) {
		if (increase)
			carbonUnits += carbonValue;
		else
			carbonUnits -= carbonValue;

		int divis = (int)(carbonUnits / 100000);
		climatePoints = 100 - divis;
    }

}
