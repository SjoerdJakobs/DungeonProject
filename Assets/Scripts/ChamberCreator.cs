using UnityEngine;
using System.Collections;

public class ChamberCreator : MonoBehaviour {
    [SerializeField]
    private int xSizeLimit = 100;
    [SerializeField]
    private int ySizeLimit = 100;
    [SerializeField]
    private int chambers = 15;
    [SerializeField]
    private Material chamberMaterial;

    // Use this for initialization
    void Start () {
        CreateRooms();
	}
	
    void CreateRooms()
    {
        for (int i = 0; i < chambers; i++)
        {
            GameObject go = new GameObject("chamber");
            go.AddComponent<GeneratePlane>();
            Renderer renderer = go.GetComponent<Renderer>();
            renderer.material = chamberMaterial;
            go.AddComponent<BoxCollider2D>();
            go.transform.position = new Vector3(Random.Range(1, xSizeLimit), Random.Range(1, ySizeLimit), 0);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.zero + new Vector3(xSizeLimit/2, ySizeLimit/2,0), new Vector3(xSizeLimit, ySizeLimit, 0));
    }
}
