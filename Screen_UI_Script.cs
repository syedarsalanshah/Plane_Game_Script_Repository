using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screen_UI_Script : MonoBehaviour
{
    public GameObject Pause_Menu;
    // Start is called before the first frame update
    void Start()
    {
        Pause_Menu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause_menu();
        }
    }
    public void Pause_menu()
    {
        Pause_Menu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Resume_menu()
    {
        Time.timeScale = 1.0f;
        Pause_Menu.gameObject.SetActive(false);

    }
    public void GotoMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
