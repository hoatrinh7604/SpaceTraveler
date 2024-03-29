using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject confirm;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI highscore;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        confirm.SetActive(false);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void Confirm(bool isShow)
    {
        Time.timeScale = (isShow) ? 0 : 1;
        confirm.SetActive(isShow);
    }

    public void BackToMenu()
    {
        GetComponent<SceneController>().BackToMenu();
    }

    public void Restart()
    {
        GetComponent<SceneController>().Restart();
    }

    public void Quit()
    {
        GetComponent<SceneController>().Quit();
    }

    public void UpdateScore(float score)
    {
        this.score.text = score.ToString();
    }

    public void UpdateHighScore(float score)
    {
        this.highscore.text = score.ToString();
    }

    public string FloatToMin(float value)
    {
        int min = (int)value / 60;
        return min + "m" + ((int)value - min * 60) + "s";
    }
}
