using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryController : MonoBehaviour
{
    public void LoadC1Scene()
    {
        SceneManager.LoadScene("C1 GameScene");
    }
    public void LoadC2Scene()
    {
        SceneManager.LoadScene("C2 GameScene");
    }
    public void LoadC3Scene()
    {
        SceneManager.LoadScene("C3 GameScene");
    }

    public void LoadC1CScene()
    {
        SceneManager.LoadScene("C1C GameScene");
    }

    public void LoadC2CScene()
    {
        SceneManager.LoadScene("C2C GameScene");
    }

    public void LoadC3CScene()
    {
        SceneManager.LoadScene("C3C GameScene");
    }

}
