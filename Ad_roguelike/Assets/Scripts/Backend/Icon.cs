using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    //����������
    bool ActiveWindow = false;
    [SerializeField] IconProperties iconProperties;
    [SerializeField] GameObject WindowPrefab;
    Window myWindow;
    bool closedWindow;


    void Start()
    {
        //��������� ������ �� �������� SpriteRenderer
        GetComponent<SpriteRenderer>().sprite = iconProperties.iconSprite;
    }
    private void OnMouseDown() //�����, ��������������� ������� � ����������� ��������� ������� �������� �� ����������
    {
        StartApp();
    }

    void StartApp()
    {
        if (!ActiveWindow)
        {
            //��� ����������� ������ ���� ��� ��� �������� ���������� (�� ������� ����� ����� ����� ��������� ������� ����������)
            ActiveWindow = true;
            GameObject a = Instantiate(WindowPrefab, Vector2.zero, Quaternion.identity);
            myWindow = a.GetComponent<Window>();
            myWindow.windowProperties = iconProperties.windowProperties;
            var b = Instantiate(myWindow.windowProperties.Template, new Vector2(0, 0), Quaternion.identity);
            b.transform.parent = myWindow.transform.GetChild(3);
            WindowManager.Up(a.GetComponent<Window>()); 
            
        }
        else
        {
            //��� ����������� ����� �� �������� ����������
            if (closedWindow)
            {
                WindowManager.OpenWindow(myWindow);
                closedWindow = !closedWindow;
            }
            else if (!closedWindow)
            {
                WindowManager.CloseWindow(myWindow);
                closedWindow = !closedWindow;
            }
        }

    }
}
