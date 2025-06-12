using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 *   Autor/Author: Pedro Lucas de Oliveira Cabral 
 *   GitHub: https://github.com/DoisLucas
 *   Data/Date: 01/02/2018
 */

public class Fade : MonoBehaviour
{

    #region Variaveis

    public Texture TelaFade;

    public float tempo;
    public float tempoFade = 2;
    public float alphaColor = 0f;

    public bool FadeIn;
    public bool FadeOut;

    int indexDaCena;

    #endregion
   
    public void Start()
    {
        InicioDeCena();
    }

    public void Update()
    {
        if (FadeOut || FadeIn)
        {
            tempo += Time.deltaTime;
        }
    }

    public void InicioDeCena()
    {
        FadeIn = true;
        FadeOut = false;
    }

    public void SetTempoFade(float n)
    {
        tempoFade = n;
    }

    public void ProximoNivel()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        FadeOutMudarDeNivel(index);
    }

    public void FadeOutMudarDeNivel(int index)
    {
        FadeOut = true;
        FadeIn = false;
        indexDaCena = index;
    }

    public void SairDoPrograma()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_IOS
            // On iOS, it's generally bad practice to quit programmatically.
            // Consider removing this or disabling the button.
            Debug.Log("Application.Quit() called on iOS. This is generally discouraged.");
        #else
            Application.Quit();
        #endif
    }

    public void OnGUI()
    {
        GUI.color = new Color()
        {
            a = alphaColor
        };

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TelaFade);

        if (FadeIn == true)
        {
            alphaColor = 1 - tempo / tempoFade;
        }
        else if (FadeOut == true)
        {
            alphaColor = tempo / tempoFade;
        }

        if (tempo >= tempoFade)
        {
            FalseTodosFades();
            tempo = 0;
        }

        if (alphaColor >= 1)
        {
            FalseTodosFades();
            tempo = 0;
            TransicaoDeCenario();
        }
    }

    public void FalseTodosFades()
    {
        FadeIn = false;
        FadeOut = false;
    }

    public void TransicaoDeCenario()
    {
        SceneManager.LoadScene(indexDaCena);
    }

}