using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    //����������
    bool ActiveWindow = false;
    [SerializeField] IconProperties iconProperties;
    [SerializeField] GameObject WindowPrefab, MiniIconPrefab;
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

    public void StartApp()
    {
        if (!ActiveWindow)
        {
            //��� ����������� ������ ���� ��� ��� �������� ���������� (�� ������� ����� ����� ����� ��������� ������� ����������)
            ActiveWindow = true;
            GameObject a = Instantiate(WindowPrefab, Vector2.zero, Quaternion.identity);
            //a.transform.parent = Camera.main.transform.GetChild(0);
            myWindow = a.GetComponent<Window>();
            myWindow.windowProperties = iconProperties.windowProperties;
            var b = Instantiate(myWindow.windowProperties.Template, new Vector2(0, 0), Quaternion.identity);
            b.transform.parent = myWindow.transform.GetChild(3);
            WindowManager.Up(a.GetComponent<Window>());

            GameObject c = Instantiate(MiniIconPrefab);
            c.transform.parent = transform.parent.GetChild(1).GetChild(0);
            c.GetComponent<MiniIcon>().myIcon = this;
            c.GetComponent<SpriteRenderer>().sprite = iconProperties.iconSprite;
            WindowManager.miniIcons.Add(c.GetComponent<MiniIcon>());
            WindowManager.ReorganizeMiniIcons();

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
