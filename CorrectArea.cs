using System.Collections.Generic; // List 사용을 위해 필요
using UnityEngine;
using UnityEngine.UI;
//github에 등록
public class CorrectArea : MonoBehaviour
{
    public GameObject xPrefab;
    public Mistake[] mistakeObjects;
    public Image[] heartImages;
    public Sprite heartSprite;
    public Sprite breakHeartSprite;
    public int maxXCount = 5;

    private int currentXCount = 0;
    private int currentHeartIndex = 0;
    public AudioClip wrongSound;
    private AudioSource audioSource;
    private GameManager gameManager;

    private List<GameObject> instantiatedXObjects = new List<GameObject>(); // 생성된 X 오브젝트 리스트

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (gameManager != null && gameManager.IsGameFinished()) return;

        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool isMistake = IsMistakeArea(clickPosition);

        if (!isMistake)
        {
            if (currentXCount >= maxXCount) return;

            Vector2 spawnPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                FindObjectOfType<Canvas>().transform as RectTransform,
                Input.mousePosition, Camera.main, out spawnPosition);

            GameObject instantiatedX = Instantiate(xPrefab, spawnPosition, Quaternion.identity);
            instantiatedX.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
            instantiatedX.SetActive(true);

            instantiatedXObjects.Add(instantiatedX); // 리스트에 추가

            Destroy(instantiatedX, 0.8f);

            UpdateHeartStatus();
            currentXCount++;

            if (audioSource && wrongSound)
            {
                audioSource.PlayOneShot(wrongSound);
            }

            if (currentXCount >= maxXCount)
            {
                gameManager.EndGame(false);
            }
        }
    }

    private bool IsMistakeArea(Vector2 clickPosition)
    {
        foreach (Mistake mistake in mistakeObjects)
        {
            Collider2D collider = mistake.GetComponent<Collider2D>();
            if (collider != null && collider.OverlapPoint(clickPosition))
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateHeartStatus()
    {
        if (currentHeartIndex < heartImages.Length)
        {
            heartImages[currentHeartIndex].sprite = breakHeartSprite;
            currentHeartIndex++;
        }
    }

    public void HideAllXMarks() // X 마크 비활성화 메서드
    {
        foreach (GameObject xMark in instantiatedXObjects)
        {
            if (xMark != null)
            {
                xMark.SetActive(false);
            }
        }
    }
}


