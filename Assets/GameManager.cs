using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    
    
    public static int pontuacao = 0;
    public static int pontAnterior = 0;
    public static int pontMaior = 0;
    public static int vida = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vida = 3;
        pontuacao = 0;
    }

    void OnGUI () {
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(Screen.width - 200, 20, 200, 100), "Vida: " + vida, style);
        GUI.Label(new Rect(50, 20, 200, 100), "Pontuação: " + pontuacao, style);
    }

    public void perde(){
        if(pontuacao > pontMaior){
            pontMaior = pontuacao;
        }
        pontAnterior = pontuacao;
        //SceneManager.LoadScene("Fim");
    }

    public void ganha(){
        if(pontuacao > pontMaior){
            pontMaior = pontuacao;
        }
        pontAnterior = pontuacao;
        //SceneManager.LoadScene("Fim");
    }

    // Update is called once per frame
    void Update()
    {

    }
}