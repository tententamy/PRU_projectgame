using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public QuizManager quizManager;
    private bool isGamePaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ GameManager khi load scene mới
        }
    }

    public void Question(Player player, Enemies enemy)
    {
        if (quizManager == null)
        {
            Debug.LogError("QuizManager is not assigned in GameManager!");
            return;
        }
        quizManager.ShowQuestion(player, enemy);
        PauseGame(); // Tạm dừng trò chơi khi hiển thị câu hỏi
    }

    public void PauseGame()
    {
        Debug.Log("Pausing game...");
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game...");
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}