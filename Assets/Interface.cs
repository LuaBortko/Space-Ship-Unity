using UnityEngine;
using UnityEngine.SceneManagement;
public class Interface : MonoBehaviour
{
    void OnGUI()
    {
        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 100;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.white;

        GUIStyle textStyle = new GUIStyle();
        textStyle.fontSize = 50;
        textStyle.alignment = TextAnchor.MiddleCenter;
        textStyle.normal.textColor = Color.white;

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 30;

        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        // Texto principal
        string texto;
        if(SceneManager.GetActiveScene().name == "Vitoria")
        {
            texto = "Parabuens\n Yupiiiiiii";
        }
        else
        {
            texto = "Pura decepção :(";
        }

        GUI.Label(new Rect(centerX - 200, centerY - 100, 400, 60), texto, titleStyle);

        // Texto secundário
        GUI.Label(new Rect(centerX - 200, centerY + 100, 400, 40), "Pontuação: "+ GameManager.pontuacao, textStyle);
        GUI.Label(new Rect(centerX - 200, centerY + 200, 400, 40), "Maior Pontuação: "+ GameManager.pontMaior, textStyle);

        // Botão
        if (GUI.Button(new Rect(centerX - 150, centerY + 300, 300, 80),"Menu", buttonStyle))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}