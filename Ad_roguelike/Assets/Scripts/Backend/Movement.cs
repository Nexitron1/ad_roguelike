using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2Int MyPos;
    MapGenerator generator;
    Animator Cosmonaut;
    Character character;
    
    private void Start()
    {
        generator = Camera.main.GetComponent<MapGenerator>();
        Cosmonaut = transform.parent.GetChild(2).GetComponent<Animator>();
        character = Camera.main.GetComponent<Character>();

        character.SetMovement(this);
        MyPos = character.CosmonautPos;
        transform.localPosition = new Vector2(transform.localPosition.x + 245 * MyPos.x, transform.localPosition.y + 245 * MyPos.y);

    }
    public void RefreshPos()
    {
        MyPos = character.CosmonautPos;
    }
    void Update()
    {
        if (character.CanMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (generator.rooms.ContainsKey(new Vector2Int(MyPos.x + 1, MyPos.y)))
                {
                    if (SearchTransitions(new Vector2Int(MyPos.x, MyPos.y), new Vector2Int(MyPos.x + 1, MyPos.y)))
                    {
                        StartCoroutine(MoveTo(new Vector2(transform.localPosition.x + 245, transform.localPosition.y)));
                        MyPos = new Vector2Int(MyPos.x + 1, MyPos.y);
                        transform.localScale = new Vector3(35, 15, 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (generator.rooms.ContainsKey(new Vector2Int(MyPos.x - 1, MyPos.y)))
                {
                    if (SearchTransitions(new Vector2Int(MyPos.x, MyPos.y), new Vector2Int(MyPos.x - 1, MyPos.y)))
                    {
                        StartCoroutine(MoveTo(new Vector2(transform.localPosition.x - 245, transform.localPosition.y)));
                        MyPos = new Vector2Int(MyPos.x - 1, MyPos.y);
                        transform.localScale = new Vector3(35, 15, 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftControl))
            {
                if (generator.rooms.ContainsKey(new Vector2Int(MyPos.x, MyPos.y + 1)))
                {
                    if (SearchTransitions(new Vector2Int(MyPos.x, MyPos.y), new Vector2Int(MyPos.x, MyPos.y + 1)))
                    {
                        StartCoroutine(MoveTo(new Vector3(transform.localPosition.x, transform.localPosition.y + 245)));
                        MyPos = new Vector2Int(MyPos.x, MyPos.y + 1);
                        transform.localScale = new Vector3(15, 35, 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftControl))
            {
                if (generator.rooms.ContainsKey(new Vector2Int(MyPos.x, MyPos.y - 1)))
                {
                    if (SearchTransitions(new Vector2Int(MyPos.x, MyPos.y), new Vector2Int(MyPos.x, MyPos.y - 1)))
                    {
                        StartCoroutine(MoveTo(new Vector3(transform.localPosition.x, transform.localPosition.y - 245)));
                        MyPos = new Vector2Int(MyPos.x, MyPos.y - 1);
                        transform.localScale = new Vector3(15, 35, 1);
                    }
                }
            }

            character.CosmonautPos = MyPos;
        }

    }

    bool SearchTransitions(Vector2Int start, Vector2Int end)
    {
        for (int i = 0; i < generator.transitions1.Count; i++)
        {
            if(generator.transitions1[i] == start && generator.transitions2[i] == end)
            {
                return true;
            }
        }

        for (int i = 0; i < generator.transitions2.Count; i++)
        {
            if (generator.transitions2[i] == start && generator.transitions1[i] == end)
            {
                return true;
            }
        }

        return false;
    }


    IEnumerator MoveTo(Vector2 endPos)
    {
        character.CanMove = false;
        Cosmonaut.SetBool("isRun", true);
        
        float MovementTime = 0.4f;
        Vector2 startPos = transform.localPosition;

        for (float i = 0; i < 1; i += MovementTime / (40 * MovementTime))
        {
            transform.localPosition = Vector2.Lerp(startPos, endPos, i);
            yield return new WaitForSeconds(MovementTime / 40);
        }
        transform.localPosition = endPos;
        character.CanMove = true;
        Cosmonaut.SetBool("isRun", false);
        transform.localScale = new Vector3(35, 35, 1);

        character.RoomEntry(generator.rooms[MyPos]);
    }
}
