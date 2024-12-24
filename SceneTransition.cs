using UnityEngine;
using UnityEngine.SceneManagement;
//github에 등록
public class SceneTransition : MonoBehaviour
{
    public void OnStartSceneButtonClick()
    {
        SceneManager.LoadScene("StartScene");  
    }
    
    public void OnRound1ButtonClick()
    {
        SceneManager.LoadScene("1RoundScene");  
    }

    public void OnRound2ButtonClick()
    {
        // Round2 씬으로 전환
        SceneManager.LoadScene("2RoundScene");
    }

    public void OnEndGameButtonClick()
    {
        // EndGame 씬으로 전환
        SceneManager.LoadScene("EndScene");
    }

    public void OnExitButtonClick()
    {
        // 게임 종료
        Application.Quit();

        // 에디터에서 테스트할 때는 Unity를 종료하지 않도록 아래 코드 추가
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
