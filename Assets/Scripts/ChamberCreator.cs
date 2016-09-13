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


    private int countForSepparatedRooms;


    private bool isAlone;
    private bool startSepperations;
    private bool startEllimination;


    // Use this for initialization
    void Start () {
        countForSepparatedRooms = 1;
        startSepperations = false;
        startEllimination = false;
        CreateRooms();
        Invoke("moveChamber", 5);
	}

    void Update()
    {

    }
	
    void CreateRooms()
    {
        for (int i = 0; i < spawnChambersStartAmmount; i++)
        {
            isAlone = false;
            GameObject chamber = new GameObject("chamber"+GeneratePlanesList.generatedPlanes.Count);
            chamber.transform.position = new Vector3(Random.Range(1, xSizeLimit), Random.Range(1, ySizeLimit), 0);
            chamber.AddComponent<GeneratePlane>();
            Renderer renderer = chamber.GetComponent<Renderer>();
            renderer.material = chamberMaterial;
            chamber.AddComponent<BoxCollider2D>();
            BoxCollider2D coll = chamber.GetComponent<BoxCollider2D>();
            //coll.size
            chamber.AddComponent<Rigidbody2D>();
            Rigidbody2D rigid = chamber.GetComponent<Rigidbody2D>();
            rigid.drag = 5;
            rigid.gravityScale = 0;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }


    void moveChamber()
    {
        for(int i = 0; i < spawnChambersStartAmmount*0.80f; i++)
        {
            Destroy(GeneratePlanesList.generatedPlanes[Random.Range(1,(spawnChambersStartAmmount / 3) + i)]);
        }
    }
	
    void OnDrawGizmos()
    {       
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.zero + new Vector3(xSizeLimit/2, ySizeLimit/2,0), new Vector3(xSizeLimit, ySizeLimit, 0));
    }

    void CheckForCollisionWithOtherChamber(GameObject obj)
    {
        GeneratePlane ownCorners = obj.GetComponent<GeneratePlane>();
        //Rect rect = new Rect(new Vector2(obj.transform.position.x, obj.transform.position.y+ownCorners.ySize), new Vector2((ownCorners._corners.downRightCorner.x - ownCorners._corners.downLeftCorner.x),(ownCorners._corners.upperRightCorner.y - ownCorners._corners.downRightCorner.y)));
        foreach (GameObject G in GeneratePlanesList.generatedPlanes)
        {
            if (G != obj)
            {
                //print("moved1");
                GeneratePlane cornerInf = G.GetComponent<GeneratePlane>();

                //print(cornerInf._corners.downLeftCorner + "down left");
                //print(cornerInf._corners.downRightCorner + "down right");
                //print(cornerInf._corners.upperLeftCorner + "upper left");
                //print(cornerInf._corners.upperRightCorner + "upper right");
                print((ownCorners.checkCollisions(cornerInf._corners.downLeftCorner) || ownCorners.checkCollisions(cornerInf._corners.downRightCorner) || ownCorners.checkCollisions(cornerInf._corners.upperLeftCorner) || ownCorners.checkCollisions(cornerInf._corners.upperRightCorner)) + " " + obj.name);
                if (ownCorners.checkCollisions(cornerInf._corners.downLeftCorner) ||
                    ownCorners.checkCollisions(cornerInf._corners.downRightCorner) ||
                    ownCorners.checkCollisions(cornerInf._corners.upperLeftCorner) ||
                    ownCorners.checkCollisions(cornerInf._corners.upperRightCorner) ||
                    ownCorners.checkCollisions(cornerInf._corners.middle))
                {
                    obj.transform.position = G.transform.position - new Vector3(ownCorners.xSize + 1, 0, 0);
                    //obj.transform.position = new Vector3(200, 300, 300);
                    print("moved" + obj.name);
                    print(isAlone);
                    //CheckForCollisionWithOtherChamber(obj);
                    //isAlone = true;
                }
                else
                {
                    isAlone = true;
                    print("im done " + obj.name);
                }
            }
            else
            {
                isAlone = true;
            }
        }
    }
}
