using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 48;
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Bold;

        string activeScene = SceneManager.GetActiveScene().name;

        if (activeScene == "MenuScene")
        {
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "SPACESHIP\n\nPressione espaço para começar!", style);
        }
        else if (activeScene == "VictoryScene")
        {
            style.normal.textColor = Color.green;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Você Ganhou!\n\nPressione espaço para reiniciar", style);
        }
        else if (activeScene == "DefeatScene")
        {
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "GAME OVER\n\nPressione espaço para reiniciar", style);
        }
    }
}
