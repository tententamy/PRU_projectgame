using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    private static QuizManager _instance;

    public static QuizManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<QuizManager>();

                if (_instance == null)
                {
                    Debug.LogError("QuizManager is not found in the scene.");
                }
            }
            return _instance;
        }
    }

    public GameObject questionPanel; // Panel hiển thị câu hỏi và đáp án
    public TMP_Text questionText; // Text hiển thị câu hỏi
    public Button answerButton1; // Button hiển thị đáp án 1
    public Button answerButton2; // Button hiển thị đáp án 2
    public TMP_Text scoreText; // Text hiển thị điểm số
    public GameObject losePanel;
    public TMP_Text loseText;

    public List<QuizQuestion> questions; // Danh sách các câu hỏi

    private Player playerScript;
    private Enemies enemyScript;

    private int score = 0;

    private void Start()
    {
        // Kiểm tra xem tất cả các thành phần UI đã được gán chưa
        if (questionPanel == null || questionText == null || answerButton1 == null || answerButton2 == null || scoreText == null)
        {
            Debug.LogError("Some UI components are not assigned in QuizManager.");
            return;
        }
        if (losePanel != null)
            losePanel.SetActive(false);

        questionPanel.SetActive(false); // Ẩn panel câu hỏi khi bắt đầu
        //scoreText.gameObject.SetActive(true);

        // Hiển thị điểm số ban đầu
        UpdateScore();

        // Khởi tạo danh sách câu hỏi
        questions = new List<QuizQuestion>
 {
     new QuizQuestion("What is the capital of France?", new string[] { "Paris", "London" }, 0),
     new QuizQuestion("What is 2 + 2?", new string[] { "3", "4" }, 1),
     new QuizQuestion("What color is the sky?", new string[] { "Blue", "Green" }, 0),
     new QuizQuestion("What is the largest planet in our solar system?", new string[] { "Earth", "Jupiter" }, 1),
     new QuizQuestion("Who wrote 'Hamlet'?", new string[] { "Shakespeare", "Tolkien" }, 0),
     new QuizQuestion("What is the boiling point of water?", new string[] { "100°C", "50°C" }, 0),
     new QuizQuestion("What is the chemical symbol for water?", new string[] { "H2O", "O2" }, 0),
     new QuizQuestion("How many continents are there?", new string[] { "5", "7" }, 1),
     new QuizQuestion("What is the currency of Japan?", new string[] { "Yen", "Dollar" }, 0),
     new QuizQuestion("Who painted the Mona Lisa?", new string[] { "Da Vinci", "Van Gogh" }, 0),
     new QuizQuestion("Which element is represented by the symbol 'O'?", new string[] { "Oxygen", "Gold" }, 0),
     new QuizQuestion("In which year did the Titanic sink?", new string[] { "1912", "1905" }, 0),
     new QuizQuestion("Who discovered penicillin?", new string[] { "Alexander Fleming", "Marie Curie" }, 0),
     new QuizQuestion("What is the speed of light?", new string[] { "300,000 km/s", "150,000 km/s" }, 0),
     new QuizQuestion("What is the capital of Australia?", new string[] { "Sydney", "Canberra" }, 1),
     new QuizQuestion("What is the smallest country in the world?", new string[] { "Vatican City", "Monaco" }, 0),
     new QuizQuestion("Who wrote '1984'?", new string[] { "George Orwell", "Aldous Huxley" }, 0),
     new QuizQuestion("What is the hardest natural substance on Earth?", new string[] { "Diamond", "Gold" }, 0),
     new QuizQuestion("Which planet is known as the Red Planet?", new string[] { "Mars", "Venus" }, 0),
     new QuizQuestion("What is the main ingredient in sushi?", new string[] { "Rice", "Noodles" }, 0),
     new QuizQuestion("What is 7 + 5?", new string[] { "12", "11" }, 0),
     new QuizQuestion("What is 12 + 8?", new string[] { "20", "19" }, 0),
     new QuizQuestion("What is 9 + 6?", new string[] { "15", "16" }, 0),
     new QuizQuestion("What is 15 + 4?", new string[] { "19", "18" }, 0),
     new QuizQuestion("What is 23 + 7?", new string[] { "30", "31" }, 0),
     new QuizQuestion("What is 18 + 5?", new string[] { "23", "22" }, 0),
     new QuizQuestion("What is 11 + 9?", new string[] { "20", "21" }, 0),
     new QuizQuestion("What is 14 + 13?", new string[] { "27", "26" }, 0),
     new QuizQuestion("What is 29 + 10?", new string[] { "39", "38" }, 0),
     new QuizQuestion("What is 6 + 7?", new string[] { "13", "14" }, 0),
     new QuizQuestion("What is 15 - 7?", new string[] { "8", "9" }, 0),
     new QuizQuestion("What is 20 - 8?", new string[] { "12", "13" }, 0),
     new QuizQuestion("What is 14 - 5?", new string[] { "9", "8" }, 0),
     new QuizQuestion("What is 18 - 9?", new string[] { "9", "8" }, 0),
     new QuizQuestion("What is 25 - 12?", new string[] { "13", "14" }, 0),
     new QuizQuestion("What is 30 - 15?", new string[] { "15", "14" }, 0),
     new QuizQuestion("What is 22 - 7?", new string[] { "15", "14" }, 0),
     new QuizQuestion("What is 19 - 10?", new string[] { "9", "8" }, 0),
     new QuizQuestion("What is 17 - 6?", new string[] { "11", "10" }, 0),
     new QuizQuestion("What is 9 - 4?", new string[] { "5", "4" }, 0),
     new QuizQuestion("What is 3 x 4?", new string[] { "12", "11" }, 0),
     new QuizQuestion("What is 5 x 6?", new string[] { "30", "31" }, 0),
     new QuizQuestion("What is 7 x 8?", new string[] { "56", "55" }, 0),
     new QuizQuestion("What is 2 x 9?", new string[] { "18", "19" }, 0),
     new QuizQuestion("What is 4 x 5?", new string[] { "20", "21" }, 0),
     new QuizQuestion("What is 6 x 7?", new string[] { "42", "41" }, 0),
     new QuizQuestion("What is 8 x 3?", new string[] { "24", "23" }, 0),
     new QuizQuestion("What is 9 x 2?", new string[] { "18", "17" }, 0),
     new QuizQuestion("What is 5 x 9?", new string[] { "45", "46" }, 0),
     new QuizQuestion("What is 7 x 6?", new string[] { "42", "43" }, 0),
     new QuizQuestion("What is 16 ÷ 4?", new string[] { "4", "5" }, 0),
     new QuizQuestion("What is 18 ÷ 6?", new string[] { "3", "2" }, 0),
     new QuizQuestion("What is 20 ÷ 5?", new string[] { "4", "3" }, 0),
     new QuizQuestion("What is 24 ÷ 8?", new string[] { "3", "4" }, 0),
     new QuizQuestion("What is 15 ÷ 3?", new string[] { "5", "6" }, 0),
     new QuizQuestion("What is 12 ÷ 4?", new string[] { "3", "4" }, 0),
     new QuizQuestion("What is 28 ÷ 7?", new string[] { "4", "5" }, 0),
     new QuizQuestion("What is 30 ÷ 6?", new string[] { "5", "6" }, 0),
 };
    }

    public void ShowQuestion(Player player, Enemies enemy)
    {
        if (questionPanel == null || questionText == null || answerButton1 == null || answerButton2 == null || scoreText == null)
        {
            Debug.LogError("Some UI components are not assigned in QuizManager.");
            return;
        }

        playerScript = player;
        enemyScript = enemy;

        playerScript.StopMovement();
        enemyScript.StopMovement();

        // Chọn một câu hỏi ngẫu nhiên
        int questionIndex = Random.Range(0, questions.Count);
        QuizQuestion currentQuestion = questions[questionIndex];

        questionText.text = currentQuestion.question;

        TMP_Text answerText1 = answerButton1.GetComponentInChildren<TMP_Text>();
        TMP_Text answerText2 = answerButton2.GetComponentInChildren<TMP_Text>();

        if (answerText1 == null || answerText2 == null)
        {
            Debug.LogError("Answer buttons do not have TMP_Text components.");
            return;
        }

        answerText1.text = currentQuestion.answers[0];
        answerText2.text = currentQuestion.answers[1];

        answerButton1.onClick.RemoveAllListeners();
        answerButton2.onClick.RemoveAllListeners();

        answerButton1.onClick.AddListener(() => OnAnswerSelected(0, currentQuestion.correctAnswerIndex));
        answerButton2.onClick.AddListener(() => OnAnswerSelected(1, currentQuestion.correctAnswerIndex));

        questionPanel.SetActive(true);
    }

    private void OnAnswerSelected(int index, int correctAnswerIndex)
    {
        Debug.Log("Answer selected: " + index);

        if (index == correctAnswerIndex)
        {
            Debug.Log("Correct answer!");
            score += 1;
            UpdateScore();
        }
        else
        {
            Debug.Log("Wrong answer.");
            ShowLoseMessage();
        }

        questionPanel.SetActive(false);

        playerScript.ResumeMovement();
        enemyScript.ResumeMovement();

        GameManager.Instance.ResumeGame();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void ShowLoseMessage()
    {
        if (losePanel != null && loseText != null)
        {
            losePanel.SetActive(true);
            loseText.text = "You Lose!";
        }
        Time.timeScale = 0f;
    }
}