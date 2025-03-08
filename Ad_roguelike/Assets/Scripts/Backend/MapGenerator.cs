using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public Dictionary<Vector2Int, RoomTypes> rooms = new Dictionary<Vector2Int, RoomTypes>();
    public List<Vector2Int> transitions1, transitions2;
    public List<Vector2Int> roomPoses = new List<Vector2Int>();
    public int LastX = 0, LastY = 0;
    public List<int> ShopChance, TreasureChance;
    MapTemplate mapTemplate;

    public enum RoomTypes
    {
        None,
        Empty,
        Fight,
        Start,
        Shop,
        Treasure,
        Boss
    }

    private void Start()
    {
        ClearArrays();
        GenerateMap();

    }

    public void ClearArrays()
    {
        rooms.Clear();
        transitions1.Clear();
        transitions2.Clear();
        roomPoses.Clear();
        LastX = 0;
        LastY = 0;
    }
    public void ReConnect()
    {
        if(mapTemplate != null)
        {
            mapTemplate.transform.parent.parent.GetComponent<Window>().CloseWindow();
        }
    }
    public void ConnectMap(MapTemplate template)
    {
        mapTemplate = template;
    }

    public void RefreshIcons()
    {
        if (mapTemplate != null)
            mapTemplate.RefreshIcons();
    }
    public void GenerateMap()
    {
        int attempt = 0, att2 = 0, att3 = 0;
        rooms.Add(new Vector2Int(0, 0), RoomTypes.Start);
        roomPoses.Add(new Vector2Int(0, 0));

        

        while (roomPoses.Count < 13 && att2 < 5000)
        {
            att2++;
            att3++;

            int x = LastX;
            int y = LastY;

            int o = Random.Range(0, 4);
            switch (o)
            {
                case 0:
                    x += 1;
                    y = LastY;
                    break;
                case 1:
                    x -= 1;
                    y = LastY;
                    break;
                case 2:
                    x = LastX;
                    y += 1;
                    break;
                case 3:
                    x = LastX;
                    y -= 1;
                    break;
            }

            if (attempt >= 5)
            {
                attempt = 0;
                int a = Random.Range(0, roomPoses.Count);
                LastX = roomPoses[a].x;
                LastY = roomPoses[a].y;
                continue;
            }

            if ((x < 0 || y < 0 || x > 3 || y > 3) || (x == 3 && y == 3))
            {
                attempt++;
                continue;
            }


            if (!rooms.ContainsKey(new Vector2Int(x, y)))
            {
                rooms.Add(new Vector2Int(x, y), RoomTypes.Fight);
                transitions1.Add(new Vector2Int(LastX, LastY));
                transitions2.Add(new Vector2Int(x, y));
                roomPoses.Add(new Vector2Int(x, y));
                int b = Random.Range(0, roomPoses.Count);
                LastX = roomPoses[b].x;
                LastY = roomPoses[b].y;

            }

            if(att3 > 10)
            {
                att3 = 0;

                LastX = roomPoses[roomPoses.Count - 1].x;
                LastY = roomPoses[roomPoses.Count - 1].y;
                x = LastX;
                y = LastY;
            }

            

        }



        //расстановка комнат
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (!rooms.ContainsKey(new Vector2Int(x, y))) continue;

                if(x == 0 && y == 0)
                {
                    rooms[new Vector2Int(0, 0)] = RoomTypes.Start;
                    continue;
                }


            }
        }
        //босс
        rooms[roomPoses[roomPoses.Count - 1]] = RoomTypes.Boss;

        //магаз
        for (int i = 0; i < ShopChance.Count; i++)
        {
            bool a = true;
            while (a)
            {
                Vector2Int p = new Vector2Int(Random.Range(0, 4), Random.Range(0, 4));
                if ((p.x == 0 && p.y == 0) || (!rooms.ContainsKey(new Vector2Int(p.x, p.y))))
                    continue;

                if (!(rooms[new Vector2Int(p.x, p.y)] == RoomTypes.Fight))
                    continue;

                if ((Random.Range(0, 100) >= ShopChance[i]))
                    break;

                rooms[new Vector2Int(p.x, p.y)] = RoomTypes.Shop;
                a = false;
            }
        }

        //сокровище
        for (int i = 0; i < TreasureChance.Count; i++)
        {
            bool a = true;
            while (a)
            {
                Vector2Int p = new Vector2Int(Random.Range(0, 4), Random.Range(0, 4));
                if ((p.x == 0 && p.y == 0) || (!rooms.ContainsKey(new Vector2Int(p.x, p.y))))
                    continue;

                if (!(rooms[new Vector2Int(p.x, p.y)] == RoomTypes.Fight))
                    continue;

                if ((Random.Range(0, 100) >= TreasureChance[i]))
                    break;

                rooms[new Vector2Int(p.x, p.y)] = RoomTypes.Treasure;
                a = false;
            }
        }



    }

}