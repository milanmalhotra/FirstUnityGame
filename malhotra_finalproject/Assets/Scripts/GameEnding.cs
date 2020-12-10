using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject player;
    public CanvasGroup hostageSavedImage;
    public AudioSource hostageSavedAudio;
    public CanvasGroup playerDeadImage;
    public AudioSource playerDeadAudio;
    public Canvas mainCanvas;
    public Text enemyWarningText;
    public Text finalScore;
    public Timer time;

    public static bool atHostage;
    static bool isPlayerDead;
    float timer;
    bool hasAudioPlayed;
    static float displayImageDuration = 1f;

    private void Start()
    {
        enemyWarningText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            //if all enemies are dead and the player is at the hostage, set atHostage to true
            if (Target.totalEnemies <= 0)
            {
                atHostage = true;
            }
            //if not display warning message
            else
            {
                mainCanvas.enabled = false;
                enemyWarningText.enabled = true;
                enemyWarningText.text = "You must kill all the enemies before saving the hostage. There's still " + Target.totalEnemies.ToString() + " enemies left.";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mainCanvas.enabled = true;
            enemyWarningText.enabled = false;
        }
    }

    public static void KilledPlayer()
    {
        isPlayerDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (atHostage)
        {
            string minutes = ((time.finalTime % 3600) / 60).ToString("00");
            string seconds = (time.finalTime % 60).ToString("00");
            finalScore.text = "Final Score: " + Target.score.ToString() + "           " + "Time: " + minutes + ":" + seconds;
            displayImageDuration = 7f;
            mainCanvas.enabled = false;
            EndLevel(hostageSavedImage, false, hostageSavedAudio);
        }
        else if (isPlayerDead)
        {
            displayImageDuration = 3f;
            mainCanvas.enabled = false;
            EndLevel(playerDeadImage, true, playerDeadAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

        if(timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(1);
                isPlayerDead = false;
                Target.score = 0;
            }
            else
            {
                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}
