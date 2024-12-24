using UnityEngine;

//github에 등록
public class ButtonSound : MonoBehaviour
{
    // AudioSource와 AudioClip을 변수로 선언
    public AudioSource audioSource;
    public AudioClip buttonSound;

    // 버튼 클릭 시 호출할 메서드
    public void PlaySound()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound); // 소리 재생
        }
    }
}
