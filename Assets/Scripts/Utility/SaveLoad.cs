using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour
{

    BuildMechanic buildingScript;

    List<Building> buildings;

    public Transform parent;

    void Awake() {
        buildingScript = FindObjectOfType<BuildMechanic>();

        Load();
        // Load and resave
        Save();
    }

    void Update() {
        if (buildingScript.debug) {
            if (Input.GetKeyDown(KeyCode.S)) {
                Save();
            }
        }
    }

    public void Save() {
        buildings = buildingScript.buildings;

        FileStream fs = File.Create(Application.persistentDataPath + "/Buildings.dat");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, buildings);
        fs.Close();
    }

    void Load() {
        string path = Application.persistentDataPath + "/Buildings.dat";
        if (File.Exists(path)) {
            FileStream fs = File.Open(Application.persistentDataPath + "/Buildings.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            if (fs.Length != 0) {
                Reinstantiate((List<Building>)bf.Deserialize(fs));
                buildingScript.buildings = buildings;
            }
            else {
                buildingScript.buildings = new List<Building>();
            }
            fs.Close();
        }
        else {
            FileStream fs = File.Create(Application.persistentDataPath + "/Buildings.dat");
        }
    }

    void Reinstantiate(List<Building> objectsToCreate) {
        for (int i = 0; i < objectsToCreate.Count; i++) {

            for(int x = 0; x < buildingScript.identification.Length; x++) {
                if(objectsToCreate[i].ID == buildingScript.identification[x].id) {
                    GameObject obj = Instantiate(buildingScript.identification[x].buildPrefab, parent);
                    obj.transform.position = objectsToCreate[i].ReturnPos();
                    obj.transform.rotation = objectsToCreate[i].ReturnRot();
                    break;
                }
            }
        }

        buildings = objectsToCreate;
    }
}
[Serializable]
public struct Identification {
    public string id;
    public GameObject prefab;
}

