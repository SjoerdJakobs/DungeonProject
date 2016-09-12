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

    private bool isAlone;

    public List<GameObject> spawnedChambers = new List<GameObject>();

    // Use this for initialization
    void Start () {
        CreateRooms();
	}
	
    void CreateRooms()
    {
        for (int i = 0; i < spawnChambersStartAmmount; i++)
        {
            isAlone = false;
            GameObject go = new GameObject("chamber"+spawnedChambers.Count);
            go.transform.position = new Vector3(Random.Range(1, xSizeLimit), Random.Range(1, ySizeLimit), 0);
            go.AddComponent<GeneratePlane>();
            Renderer renderer = go.GetComponent<Renderer>();
            renderer.material = chamberMaterial;
            //go.AddComponent<BoxCollider2D>();
            while (!isAlone)
            {
                CheckForCollisionWithOtherChamber(go);
            }
            spawnedChambers.Add(go);
        }
    }

    void CheckForCollisionWithOtherChamber(GameObject obj)
    {
        GeneratePlane ownCorners = obj.GetComponent<GeneratePlane>();
        //Rect rect = new Rect(new Vector2(obj.transform.position.x, obj.transform.position.y+ownCorners.ySize), new Vector2((ownCorners._corners.downRightCorner.x - ownCorners._corners.downLeftCorner.x),(ownCorners._corners.upperRightCorner.y - ownCorners._corners.downRightCorner.y)));
        if (spawnedChambers.Count > 0)
        {
            foreach (GameObject G in spawnedChambers)
            {
                //print("moved1");
                GeneratePlane cornerInf = G.GetComponent<GeneratePlane>();

                /*
                print((G.transform.position.x + cornerInf.xSize > obj.transform.position.x &&
                     G.transform.position.x < obj.transform.position.x + ownCorners.xSize &&
                     G.transform.position.y + cornerInf.xSize < obj.transform.position.y &&
                     G.transform.position.y < obj.transform.position.y + ownCorners.ySize));
                if (G.transform.position.x + cornerInf.xSize > obj.transform.position.x  &&
                     G.transform.position.x < obj.transform.position.x + ownCorners.xSize &&
                     G.transform.position.y + cornerInf.xSize < obj.transform.position.y  &&
                     G.transform.position.y < obj.transform.position.y + ownCorners.ySize)
                {

                    obj.transform.position = G.transform.position - new Vector3(ownCorners.xSize, 0, 0);

                }
                else
                {
                    isAlone = true;
                }*/

                //print(cornerInf._corners.downLeftCorner + "down left");
                //print(cornerInf._corners.downRightCorner + "down right");
                //print(cornerInf._corners.upperLeftCorner + "upper left");
                //print(cornerInf._corners.upperRightCorner + "upper right");
                //print((ownCorners.checkCollisions(cornerInf._corners.downLeftCorner) || ownCorners.checkCollisions(cornerInf._corners.downRightCorner) || ownCorners.checkCollisions(cornerInf._corners.upperLeftCorner) || ownCorners.checkCollisions(cornerInf._corners.upperRightCorner))+ " " + obj.name);
                if (ownCorners.checkCollisions(cornerInf._corners.downLeftCorner) || 
                    ownCorners.checkCollisions(cornerInf._corners.downRightCorner) || 
                    ownCorners.checkCollisions(cornerInf._corners.upperLeftCorner) || 
                    ownCorners.checkCollisions(cornerInf._corners.upperRightCorner))
                {
                    obj.transform.position = G.transform.position - new Vector3(ownCorners.xSize+1, 0, 0);
                    //obj.transform.position = new Vector3(200, 300, 300);
                    print("moved"+ obj.name);
                    print(isAlone);
                    //isAlone = true;
                }
                else
                {
                    isAlone = true;
                }
            }
        }
        else
        {
            isAlone = true;
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
