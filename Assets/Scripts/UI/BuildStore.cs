using UnityEngine;

public class BuildStore : MonoBehaviour
{
    public GameObject buildMenu;

    BuildMechanic buildMechanics;

    public GameObject selectors;

    void Awake() {
        buildMechanics = FindObjectOfType<BuildMechanic>();
    }

    public void OnPressExit() {
        buildMenu.SetActive(false);        
    }

    public void OnOpenBuildMenu() {
        buildMenu.SetActive(true);
    }

    public void OnPurchase(string itemAndPrice) {
        string item = itemAndPrice.Split('|')[0];
        float price = float.Parse(itemAndPrice.Split('|')[1]);
        print(item);
        buildMechanics.Purchase(item, price);
        buildMenu.SetActive(false);
    }

    public void OnSelectBuilding(GameObject set) {
        for(int i = 0; i < selectors.transform.childCount; i++) {
            if (selectors.transform.GetChild(i).gameObject.activeSelf) {
                selectors.transform.GetChild(i).gameObject.SetActive(false);
                break;
            }
        }
        set.SetActive(true);
    }
}
