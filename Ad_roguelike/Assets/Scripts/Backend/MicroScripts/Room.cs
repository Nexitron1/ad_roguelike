using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapGenerator;

public class Room : MonoBehaviour
{
    public Vector2Int myPosition;
    public Sprite[] roomSprites;
    public GameObject roomSpritePrefab;
    public SpriteRenderer roomIcon;
    RoomTypes myType;
    void Start()
    {

    }
    public void SetRoomIcon()
    {
        roomIcon = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    public void SetType(RoomTypes type)
    {
        if (roomIcon == null) SetRoomIcon();

        myType = type;
        RedactRoomIcon(myType);
    }

    void RedactRoomIcon(RoomTypes n)
    {
        if (roomIcon == null) SetRoomIcon();

        roomIcon.sprite = roomSprites[(int)n];

        if (n == RoomTypes.Boss)
            MakeIconRed();
    }
        
    public void MakeIconRed()
    {
        if (roomIcon == null) SetRoomIcon();

        roomIcon.color = Color.red;
    }

}
