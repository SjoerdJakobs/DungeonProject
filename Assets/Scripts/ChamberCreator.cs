using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChamberCreator : MonoBehaviour {
    [SerializeField]
    private int xSizeLimit = 100;
    [SerializeField]
    private int ySizeLimit = 100;
    [SerializeField]
    private int spawnChambersStartAmmount = 15;
    [SerializeField]
    private Material chamberMaterial;

    public List<GameObject> spawnedChambers = new List<GameObject>();

    // Use this for initialization
    void Start () {
        CreateRooms();
	}
	
    void CreateRooms()
    {
        for (int i = 0; i < spawnChambersStartAmmount; i++)
        {
            GameObject go = new GameObject("chamber");
            go.AddComponent<GeneratePlane>();
            Renderer renderer = go.GetComponent<Renderer>();
            renderer.material = chamberMaterial;
            go.AddComponent<BoxCollider2D>();
            go.transform.position = new Vector3(Random.Range(1, xSizeLimit), Random.Range(1, ySizeLimit), 0);
            CheckForCollisionWithOtherChamber(go);
            spawnedChambers.Add(go);
            
        }
    }

    void CheckForCollisionWithOtherChamber(GameObject obj)
    {
        GeneratePlane ownCorners = obj.GetComponent<GeneratePlane>();
        Rect rect = new Rect(new Vector2(obj.transform.position.x, obj.transform.position.y+ownCorners.ySize), new Vector2((ownCorners._corners.downRightCorner.x - ownCorners._corners.downLeftCorner.x),(ownCorners._corners.upperRightCorner.y - ownCorners._corners.downRightCorner.y)));
        foreach(GameObject G in spawnedChambers)
        {
            //print("moved1");
            GeneratePlane cornerInf = G.GetComponent<GeneratePlane>();
            //print((rect.Contains(cornerInf._corners.downLeftCorner) || rect.Contains(cornerInf._corners.downRightCorner) || rect.Contains(cornerInf._corners.upperLeftCorner) || rect.Contains(cornerInf._corners.upperRightCorner)));
            if (rect.Contains(cornerInf._corners.downLeftCorner) || rect.Contains(cornerInf._corners.downRightCorner) || rect.Contains(cornerInf._corners.upperLeftCorner) || rect.Contains(cornerInf._corners.upperRightCorner))
            {
                obj.transform.position = G.transform.position - new Vector3(ownCorners.xSize, ownCorners.ySize, 0);
                //print("moved");
            }
        }
    }

    void moveChamber()
    {

    }
	
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.zero + new Vector3(xSizeLimit/2, ySizeLimit/2,0), new Vector3(xSizeLimit, ySizeLimit, 0));
    }

}
