using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public static StartScreenManager Instance { get; private set; }

    public GameObject startPanel;
    public Button startButton;
    public TMP_Text welcomeText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startPanel == null || startButton == null || welcomeText == null)
        {
            Debug.LogError("Some UI components are not assigned in StartScreenManager.");
            return;
        }
        ShowStartScreen();
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void ShowStartScreen()
    {
        startPanel.SetActive(true);
    }

    private void OnStartButtonClicked()
    {
        startPanel.SetActive(false); // Ẩn panel bắt đầu khi nút bắt đầu được nhấn
        GameManager.Instance.StartGame();
    }
}
