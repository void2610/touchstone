namespace NRoom
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class Room : MonoBehaviour
    {
        public Vector3Int position = new Vector3Int(0, 0, 0);

        public int length;

        public TileBase surface;

        public TileBase underground;

        //constructor
        public Room()
        {
            position = new Vector3Int(0, 0, 0);
            length = 0;
            surface = null;
            underground = null;
        }

        public Room(Vector3 pos, int len, TileBase sur, TileBase und)
        {
            //Vector3Intに変換してpositionに代入
            position = new Vector3Int((int) pos.x, (int) pos.y, (int) pos.z);
            length = len;
            surface = sur;
            underground = und;
        }

        public virtual void CreateFloor()
        {
        }

        public virtual void Start()
        {
            Debug.Log("Room Start");
        }

        public virtual void Update()
        {
        }
    }
}
