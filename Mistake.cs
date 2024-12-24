using System.Collections.Generic; // List 사용을 위해 필요
using UnityEngine;
//github에 등록
public class Mistake : MonoBehaviour
{
    public GameObject circlePrefab;
    private bool isClicked = false;

    public AudioClip correctSound;
    private AudioSource audioSource;
    private GameManager gameManager;

    // 생성된 Circle 오브젝트를 추적하기 위한 리스트
    private List<GameObject> instantiatedCircles = new List<GameObject>();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (isClicked) return;
        if (gameManager != null && gameManager.IsGameFinished()) return;

        Vector2 spawnPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            FindObjectOfType<Canvas>().transform as RectTransform,
            Input.mousePosition, Camera.main, out spawnPosition);

        GameObject instantiatedCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        instantiatedCircle.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        instantiatedCircle.SetActive(true);

        // 생성된 Circle을 리스트에 추가
        instantiatedCircles.Add(instantiatedCircle);

        if (audioSource != null && correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }

        isClicked = true;
        gameManager.RegisterClick(); //정답 카운트 증가
    }

    // 생성된 Circle 오브젝트들을 비활성화하는 메서드
    public void HideAllCircles()
    {
        foreach (GameObject circle in instantiatedCircles)
        {
            if (circle != null)
            {
                circle.SetActive(false);
            }
        }
    }
}
