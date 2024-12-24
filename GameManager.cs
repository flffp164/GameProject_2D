using UnityEngine;
using UnityEngine.SceneManagement; // Scene 관리에 필요
using UnityEngine.UI; // UI를 관리하기 위해 필요
//github에 등록ㅇ
public class GameManager : MonoBehaviour
{
    public int totalMistakes = 5;
    private int correctClicks = 0;

    public GameObject nextStageButton;
    public GameObject gameOverPanel;
    public Button restartButton;

    public Text correctClicksText;
    public Text timerText;

    private bool isGameFinished = false; // 게임이 끝났는지를 확인하는 변수
    private float timer = 60.0f;

    private void Start()
    {
        nextStageButton.SetActive(false);
        gameOverPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);
        UpdateCorrectClicksText();
        UpdateTimerText();
    }

    private void Update()
    {
        if (isGameFinished) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            EndGame(false); // 실패 처리 (시간 초과)
        }
        UpdateTimerText();
    }

    private void UpdateCorrectClicksText()
    {
        correctClicksText.text = correctClicks + " / " + totalMistakes;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RegisterClick()
    {
        if (isGameFinished) return;

        correctClicks++;
        UpdateCorrectClicksText();

        if (correctClicks >= totalMistakes)
        {
            EndGame(true); // 성공 처리
        }
    }

   public void EndGame(bool isSuccess)
{
    isGameFinished = true;

    // 실패 시에만 모든 AudioSource의 오디오 멈추기
    if (!isSuccess)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    // CorrectArea의 X 마크 비활성화 호출
    CorrectArea correctArea = FindObjectOfType<CorrectArea>();
    if (correctArea != null)
    {
        correctArea.HideAllXMarks();
    }

    // 모든 Mistake 오브젝트에서 Circle 비활성화 호출
    Mistake[] mistakeObjects = FindObjectsOfType<Mistake>();
    foreach (Mistake mistake in mistakeObjects)
    {
        mistake.HideAllCircles();
    }

    if (isSuccess)
    {
        nextStageButton.SetActive(true); // 성공 시 다음 스테이지 버튼 활성화
    }
    else
    {
        gameOverPanel.SetActive(true); // 실패 시 게임 오버 패널 활성화
        restartButton.gameObject.SetActive(true); // Restart 버튼 활성화
    }
}



    public bool IsGameFinished() // 게임이 종료되었는지 확인하는 메서드 추가
    {
        return isGameFinished;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }
}