using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildMechanic : MonoBehaviour
{

    bool buildMode = false;
    string id;
    float price;
    PlayerBank playerBank;

    GameObject selectedPlacementPrefab;
    GameObject selectedBuildPrefab;
    public Transform buildingParent;

    public BuildPrefabs[] identification;
    public List<Building> buildings = new List<Building>();

    public bool debug;

    GameObject moveableBuilding;
    Vector3[] corners;

    void Awake() {
        playerBank = FindObjectOfType<PlayerBank>();
    }

    void Update() { 

        if (Input.GetKeyDown(KeyCode.B) && debug) {
            buildMode = true;
        }

        if(buildMode && selectedPlacementPrefab != null) {
            if(moveableBuilding == null) {
                moveableBuilding = Instantiate(selectedPlacementPrefab);
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit)) {
                if (rayHit.collider.CompareTag("Earth")) {
                    moveableBuilding.transform.localPosition = rayHit.point;
                    var direction = rayHit.transform.position - moveableBuilding.transform.position;
                    moveableBuilding.transform.rotation = Quaternion.FromToRotation(-Vector3.up, direction);
                    //moveableBuilding.transform.rotation = Quaternion.LookRotation(rayHit.transform.position - moveableBuilding.transform.position, moveableBuilding.transform.right);
                    moveableBuilding.transform.position += moveableBuilding.transform.up * 0.01f;
                }
            }

            if (Input.GetMouseButtonDown(0)) {
                if (CheckCollisions(moveableBuilding) && playerBank.BankBalance >= price) {
                    GameObject newBuilding = Instantiate(selectedBuildPrefab, buildingParent);
                    newBuilding.transform.position = moveableBuilding.transform.position;
                    newBuilding.transform.rotation = moveableBuilding.transform.rotation;

                    print(buildings);
                        
                    buildings.Add(new Building(id, newBuilding.transform.position, newBuilding.transform.rotation));

                    Destroy(moveableBuilding);
                    playerBank.ChangeBalance(-price);
                    //selectedBuildPrefab = null;
                    buildMode = false;
                }
            }
        }

    }

    public void Purchase(string id, float price) {
        for(int i = 0; i < identification.Length; i++) {
            if(id == identification[i].id) {
                selectedPlacementPrefab = identification[i].placementPrefab;
                selectedBuildPrefab = identification[i].buildPrefab;
                this.id = id;
                this.price = price;
            }
        }

        buildMode = true;
    }

    bool CheckCollisions(GameObject obj) {
        
        BoxCollider col = obj.GetComponent<BoxCollider>();

        var trans = col.transform;
        var min = (col.center - col.size * 0.5f);
        var max = (col.center + col.size * 0.5f);

        // Check each corner.
        corners = new Vector3[4]; // 1 = top left 2 = bottom left 3= top right 4 = bottom right
        corners[0] = trans.TransformPoint(min);
        corners[1] = trans.TransformPoint(new Vector3(min.x, min.y, max.z));
        corners[2] = trans.TransformPoint(new Vector3(max.x, min.y, max.z));
        corners[3] = trans.TransformPoint(new Vector3(max.x, min.y, min.z));


        foreach (Vector3 vec in corners) {
            Ray ray = new Ray(vec, -obj.transform.up);
            RaycastHit hit;
            Debug.DrawRay(vec, -obj.transform.up, Color.red, 10);
            print("Corner");
            if (Physics.Raycast(ray, out hit)) {
                print("Coll");
                if (!hit.collider.CompareTag("Earth")) {
                    // Failed to hit ground return false
                    print("fail");
                    return false;
                }
                print("PASS");
            }
            else {
                return false;
            }
        }

        print("success");

        return true;
    }


    void OnDrawGizmos() {
        foreach (Vector3 vec in corners) {
            Gizmos.DrawCube(vec, new Vector3(0.01f, 0.01f, 0.01f));
        }
    }
}

[Serializable]
public class Building {

    public string ID;
    public float px, py, pz;
    public float rw, rx, ry, rz;

    public Building(string id, Vector3 position, Quaternion rotation) {
        this.ID = id;
        Serialize(position, rotation);
    }

    void Serialize(Vector3 position, Quaternion rotation) {
        px = position.x;
        py = position.y;
        pz = position.z;

        rw = rotation.w;
        rx = rotation.x;
        ry = rotation.y;
        rz = rotation.z;
    }

    public Vector3 ReturnPos() {
        Vector3 pos = new Vector3(px, py, pz);

        return pos;
    }

    public Quaternion ReturnRot() {
        Quaternion rot = new Quaternion(rx, ry, rz, rw);

        return rot;
    }
}

[Serializable]
public struct BuildPrefabs {
    public string id;
    public GameObject placementPrefab;
    public GameObject buildPrefab;
}
