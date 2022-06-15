using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] float score;
    [SerializeField] float highscore;

    [SerializeField] GameObject currentPlanet;
    [SerializeField] GameObject prePlanet;
    [SerializeField] GameObject nextPlanet;
    [SerializeField] GameObject nextPlanet2;
    [SerializeField] GameObject character;

    public Transform planetPos0;
    public Transform planetPos1;
    public Transform planetPos2;
    public Transform planetPos3;

    public Color[] template = { new Color32(255, 81, 81, 255), new Color32(255, 129, 82, 255), new Color32(255, 233, 82, 255), new Color32(163, 255, 82, 255), new Color32(82, 207, 255, 255), new Color32(170, 82, 255, 255) };

    private UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlanet.GetComponent<PlanetController>().HandleTarget();
            currentPlanet.GetComponent<PlanetController>().SetPlanet(planetPos0);
            Destroy(currentPlanet, 2f);
            character.GetComponent<CharacterHandle>().HandleMoving(transform);
        }
    }

    public void SpawFirst()
    {
        nextPlanet = GetComponent<SpawObject>().Spaw(planetPos2);
        nextPlanet2 = GetComponent<SpawObject>().Spaw(planetPos3);
    }

    public void HandlePlanet()
    {
        nextPlanet = nextPlanet2;
        nextPlanet2 = GetComponent<SpawObject>().Spaw(planetPos3);
        nextPlanet.GetComponent<PlanetController>().SetPlanet(planetPos2);
    }

    public void UpdateCurrentTarget(GameObject value)
    {
        currentPlanet = value;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiController.GameOver();
    }

    public void UpdateScore()
    {
        score++;
        uiController.UpdateScore(score);
        if (score > highscore)
        {
            highscore = score;
            uiController.UpdateHighScore(highscore);
            PlayerPrefs.SetFloat("highscore", highscore);
        }
    }

    public void Reset()
    {
        Time.timeScale = 1;
        score = 0;
        highscore = PlayerPrefs.GetFloat("highscore");
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(highscore);
        SpawFirst();
    }

}
