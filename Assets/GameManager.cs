using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    
    
    public static int pontuacao = 0;
    public static int pontAnterior = 0;
    public static int pontMaior = 0;
    public static int vida = 3;
    public GameObject Star;
    public GameObject Clock;
    float timer;
    float delay = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vida = 3;
        pontuacao = 0;
        timer = 0f;
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
        timer += Time.deltaTime;
        if(timer >= delay)
        {
            float y = Random.Range(-2.5f, 2.5f);
            float rand = Random.Range(0f, 1f);
            if(rand > 0.2)
            {
                Instantiate(Star, new Vector3(5.2f, y, 0f), Quaternion.identity);
            }
            else
            {
                Instantiate(Clock, new Vector3(5.2f, y, 0f), Quaternion.identity);
            }
            timer = 0f;
        }
    }
}