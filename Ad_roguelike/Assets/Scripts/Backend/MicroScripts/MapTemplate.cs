using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapTemplate : MonoBehaviour
{
    [SerializeField] GameObject RoomPrefab, CorridorPrefab;
    public Room[,] Rooms = new Room[4, 4];
    MapGenerator generator;
    void Start()
    {
        generator = Camera.main.GetComponent<MapGenerator>();
        generator.ConnectMap(this);
        if (generator != null) RenderMap();

        WindowManager.Up(transform.parent.parent.GetComponent<Window>());
    }

    public void RefreshIcons()
    {
        for (int i = 0; i < Rooms.GetLength(0); i++)
        {
            for (int j = 0; j < Rooms.GetLength(1); j++)
            {
                if (Rooms[i, j] != null)
                {
                    Rooms[i, j].SetType(generator.rooms[new Vector2Int(i, j)]);
                }
            }
        }
    }

    public void RenderMap()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (generator.rooms.ContainsKey(new Vector2Int(x, y)))
                {
                    Rooms[x, y] = (Instantiate(RoomPrefab, new Vector2(x, y) * 0.07f, Quaternion.identity, transform.GetChild(1))).GetComponent<Room>();
                    Rooms[x, y].SetType(generator.rooms[new Vector2Int(x, y)]);
                }
            }
        }

        for (int i = 0; i < generator.transitions1.Count; i++)
        {
            Quaternion q = Quaternion.identity;

            if (generator.transitions2[i].x - generator.transitions1[i].x == 1)
            {
                q = Quaternion.Euler(0, 0, 0);
            }
            if (generator.transitions2[i].y - generator.transitions1[i].y == 1)
            {
                q = Quaternion.Euler(0, 0, 90);
            }
            if (generator.transitions2[i].x - generator.transitions1[i].x == -1)
            {
                q = Quaternion.Euler(0, 0, 180);
            }
            if (generator.transitions2[i].y - generator.transitions1[i].y == -1)
            {
                q = Quaternion.Euler(0, 0, -90);
            }

            //Instantiate(CorridorPrefab, 7 * new Vector2((transitions1[i].x + transitions2[i].x) / 2, (transitions1[i].y + transitions2[i].y) / 2), q);
            Instantiate(CorridorPrefab, 0.07f * new Vector2(generator.transitions1[i].x, generator.transitions1[i].y), q, transform.GetChild(1));
        }

        transform.GetChild(1).localPosition = new Vector2(-370, -370);
        transform.GetChild(1).localScale = new Vector2(35, 35);
    }
}
