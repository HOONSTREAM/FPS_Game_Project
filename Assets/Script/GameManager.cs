using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver

    }
    PlayerMove player;
    public GameObject gamelabel;
    Text gametext;
    public static GameManager gm;
    public GameState gstate;
    public GameObject gameOption;
    
    private void Awake()
    {
        if (gm == null)
            gm = this;
    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        gstate = GameState.Ready;
        gametext = gamelabel.GetComponent<Text>();
        gametext.text = "Ready ...";
        gametext.color = new Color32(255, 185, 0, 255);

        StartCoroutine(ReadyToStart());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenOptionWindow();
        }
        if(player.Hp <= 0)
        {
            gamelabel.SetActive(true);
            gametext.text = "Game Over";
            gametext.color = new Color32(255, 0, 0, 255);
            gstate = GameState.GameOver;

            Transform buttons = gametext.transform.GetChild(0);

            buttons.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }
    }

    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f);
        gametext.text = "Go!";

        yield return new WaitForSeconds(0.5f);

        gamelabel.SetActive(false);

        gstate = GameState.Run;

    }

    public void OpenOptionWindow()
    {

        gameOption.gameObject.SetActive(true);
        Time.timeScale = 0f; // 게임 속도를 0배속으로 진행한다.
        gstate = GameState.Pause;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CloseOptionWindow()
    {
        gameOption.gameObject.SetActive(false);
        Time.timeScale = 1f;
        gstate = GameState.Run;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
