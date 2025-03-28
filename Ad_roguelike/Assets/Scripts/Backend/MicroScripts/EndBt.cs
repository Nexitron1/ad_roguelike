using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void End(int a)
    {
        if (a == 0)
        {
            Application.Quit();
        }

        if (a == 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
