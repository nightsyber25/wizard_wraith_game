using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamecanvasController : MonoBehaviour
{
    [SerializeField] GameObject hintPanel;
    public void OpenHintPanel()
    {
        hintPanel.SetActive(true);
    }

    public void CloseHintPanel()
    {
        hintPanel.SetActive(false);
    }

    public void PlayAgainAction()
    {
        SceneManager.LoadScene("C1 GameScene");
    }

    public void BackToMenuAction()
    {
        SceneManager.LoadScene(0);
    }
}
