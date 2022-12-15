using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RHangmanManager : MonoBehaviour
{
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject keyboardContainer;
    [SerializeField] GameObject keyboardCover;
    [SerializeField] GameObject letterContainer;
    [SerializeField] GameObject[] hangmanStages;
    [SerializeField] GameObject letterButton;
    [SerializeField] TextAsset wordFile;
    [SerializeField] TextAsset hintFile;
    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject endgamePanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] TextMeshProUGUI categoryText;


    private string word;
    private string hint;
    private string[] wordList;
    private string[] hintList;
    private int correctGuesses;
    private int incorrectGuesses;
    [SerializeField] int stage = 0;
    private int score = 0;
 
    void Awake()
    {
        GenerateWord();
    }

    void Start()
    {
        InitializeButtons();
        InitializeGame();
    }

    private void InitializeButtons()
    {
        for(int i = 65; i <= 90; i++)
        {
            CreateButton(i);
        }
    }

    private void InitializeGame()
    {
        incorrectGuesses = 0;
        correctGuesses = 0;
        UpdateScoreText();
        keyboardCover.SetActive(false);
        scorePanel.SetActive(false);
        foreach (Button child in keyboardContainer.GetComponentsInChildren<Button>())
        {
            child.interactable = true;
        }
        foreach (Transform child in wordContainer.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach (GameObject stageH in hangmanStages)
        {
            stageH.SetActive(true);
        }
        stage = Random.Range(0, 59);
        UpdateCateText();
        word = wordList[stage].ToUpper();
        hint = hintList[stage];
        foreach (char letter in word)
        {
            var temp = Instantiate(letterContainer, wordContainer.transform);
        }
        UpdateHintText();
    }

    private void UpdateCateText()
    {
        if (stage <= 19)
        {
            categoryText.text = "Size & Amount";
        }
        else if (stage >= 20 && stage <= 39)
        {
            categoryText.text = "Politics & Traditions";
        }
        else
        {
            categoryText.text = "Time";
        }
    }

    private void GenerateWord()
    {
        wordList = wordFile.text.Split("\n");
        hintList = hintFile.text.Split("\n");
        for(int i = 0; i < wordList.Length; i++)
        {
            wordList[i] = wordList[i].Substring(0, wordList[i].Length - 1);
            hintList[i] = hintList[i].Substring(0, hintList[i].Length - 1);
            Debug.Log("Word: "+wordList[i]);
        }
    }

    private void CreateButton(int i)
    {
        GameObject temp = Instantiate(letterButton, keyboardContainer.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate {CheckLetter(((char)i).ToString());});
    }

    private void CheckLetter(string inputLetter)
    {
        bool letterInWord = false;
        for(int i = 0; i < word.Length; i++)
        {
            if(inputLetter == word[i].ToString())
            {
                letterInWord = true;
                correctGuesses++;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = inputLetter;
            }
        }
        if(letterInWord == false)
        {
            incorrectGuesses++; 
            hangmanStages[incorrectGuesses - 1].SetActive(false);
        }
        CheckOutcome();
    }

    private void CheckOutcome()
    {
        if(correctGuesses == word.Length)
        {
            for(int i = 0; i < word.Length; i++) 
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
            }
            stage++;
            keyboardCover.SetActive(true);
            ConcludeScore();
            Invoke("InitializeGame",2f);
        }

        if(incorrectGuesses == hangmanStages.Length)
        {
            for(int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = word[i].ToString();
            }
            keyboardCover.SetActive(true);
            ConcludeScore();
            Invoke("InitializeGame",2f);
        }
    }

    private void UpdateScoreText()
    {
        totalScoreText.text = "Score: " + score;
    }

    private void UpdateHintText()
    {
        hintText.text = hint;
    }

    private void ConcludeScore()
    {
        if(incorrectGuesses <= 2)
        {
            scoreText.text = "3";
            score +=3;
        }
        else if(incorrectGuesses > 2 && incorrectGuesses <= 4)
        {
            scoreText.text = "2";
            score +=2;
        }
        else if(incorrectGuesses > 4 && incorrectGuesses <= 5)
        {
            scoreText.text = "1";
            score +=1;
        }
        else
        {
            scoreText.text ="0";
        }
        scorePanel.SetActive(true);
    }
}
