using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public QuizManager quizManager;
    private bool isGamePaused;
    public int score;  // Biến điểm số

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
        PauseGame();
        SceneManager.sceneLoaded += OnSceneLoaded; // Đăng ký sự kiện sceneLoaded
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Hủy đăng ký sự kiện khi GameManager bị hủy
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            // Tìm QuizManager trong SampleScene
            quizManager = FindObjectOfType<QuizManager>();

            if (quizManager == null)
            {
                Debug.LogError("QuizManager not found in SampleScene!");
            }

            ResumeGame(); // Tiếp tục game khi SampleScene đã được load
        }
    }
    public void StartGame()
    {
        
        SceneManager.LoadScene("SampleScene"); // Đặt tên cảnh trò chơi của bạn ở đây
    }

    public void Question(Player player, Enemies enemy)
    {
        if (quizManager == null)
        {
            Debug.LogError("QuizManager is not assigned in GameManager!");
            return;
        }
        quizManager.ShowQuestion(player, enemy);
        PauseGame();
    }

    public void PauseGame()
    {
        Debug.Log("Pausing game...");
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Endgame()
    {
        Debug.Log("End game");
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game...");
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    public void RestartGame()
    {
        // Lấy tên của scene hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Tải lại scene hiện tại
        SceneManager.LoadScene(currentSceneName);
    }
    public void AddScore(int points)
    {
        score += points;
    }
}