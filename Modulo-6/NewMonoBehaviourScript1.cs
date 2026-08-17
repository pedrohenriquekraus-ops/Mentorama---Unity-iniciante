using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] int Conta = 0;
    [SerializeField] List<GameObject> Bolas;
    List<string> SceneNames;

    void Start()
    {



        SceneNames = new List<string>();
        string CenaRaiz = gameObject.scene.name;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string nome = Path.GetFileNameWithoutExtension(path);

            if (CenaRaiz != nome)
            {
                SceneNames.Add(nome);
            }

        }


    }


    void PintaBolas()
    {
        Bolas.Clear();
        foreach (GameObject bolas in GameObject.FindGameObjectsWithTag("Bolas"))
        {
            Bolas.Add(bolas);
        }

        foreach (GameObject go in Bolas)
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color corAleatoria = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                renderer.material.color = corAleatoria;
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // só carrega se ainda tiver cena pra carregar
            if (Conta < SceneNames.Count)
            {
                SceneManager.LoadSceneAsync(SceneNames[Conta], LoadSceneMode.Additive);

                Conta++;
                PintaBolas();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // só descarrega se tiver algo carregado além da inicial
            if (Conta > 0)
            {
                Conta--;
                SceneManager.UnloadSceneAsync(SceneNames[Conta]);
                PintaBolas();
            }
        }


    }
}