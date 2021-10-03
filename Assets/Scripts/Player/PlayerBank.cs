using UnityEngine;

public class PlayerBank : MonoBehaviour
{
    [SerializeField]
    float bankBalance = 100000;
    float omegaKey = 99999;
    float delta;

    public void Awake() {
        if (!PlayerPrefs.HasKey("PlayerBalance")) {
            PlayerPrefs.SetFloat("PlayerBalance", 10000);
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetFloat("PlayerBalance", 1000000);
        bankBalance = PlayerPrefs.GetFloat("PlayerBalance");
        RandomiseHash();
    }

    public float BankBalance {
        get {
            return bankBalance;
        }
        set {
            bankBalance = value;
            PlayerPrefs.SetFloat("PlayerBalance", value);
            PlayerPrefs.Save();
            UpdateText();
        }
    }

    void UpdateText() {

    }

	public void ChangeBalance(float value) {
        BankBalance = BankBalance + value;
        print("balupd");
        if (CheckHash())
            RandomiseHash();
    }

    public void RandomiseHash() {
        omegaKey = Random.Range(-9999, 9999);
        delta = BankBalance + omegaKey;
    }

    bool CheckHash() {
        if (delta == BankBalance + omegaKey) {
            return true;
        }
        else
            // Value has been cheated
            Application.Quit();
            return false;
    }

}
