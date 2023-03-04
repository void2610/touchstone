using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateMapScript : MonoBehaviour
{
    Tilemap tilemap;
    public TileBase ground;
    public TileBase underground;
    //Vector3Int pos = new Vector3Int(0, 0, 0);
    int gHeight = 0;
    //int sHeight = 10;
    //int roomNum = 1;
    public int maxRoomNum = 10;
    public float enemyProbability = 0.3f;
    int[] leftSide = new int[50];
    int[] rightSide = new int[50];
    public GameObject slime;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        leftSide[0] = 0;
        rightSide[0] = leftSide[0] + Random.Range (10, 15);
        for(int i = 1; i < maxRoomNum; i++){
            leftSide[i] = rightSide[i-1]+1;
            rightSide[i] = leftSide[i] + Random.Range (10, 15);
        }

        for(int i = 0; i < maxRoomNum; i++){
            for(int j = leftSide[i]-1; j < rightSide[i]; j++){
                tilemap.SetTile(new Vector3Int(j,gHeight,0), ground);
                for(int k = gHeight-1; k > gHeight-20; k--){
                    tilemap.SetTile(new Vector3Int(j,k,0), underground);
                }
            }

            if(Random.value < enemyProbability){
                Instantiate(slime, new Vector3Int(rightSide[i]-1, gHeight+2,0), Quaternion.identity);
            }

            gHeight += Random.Range (-2, 2);
            if(gHeight > 4){
                gHeight = 3;
            }
            else if(gHeight < -4){
                gHeight = -3;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
