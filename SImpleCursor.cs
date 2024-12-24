using UnityEngine;
//github에 등록
public class SimpleCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // 마우스 커서 이미지

    void Start()
    {
        // 커서 이미지 변경
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnDisable()
    {
        // 게임 종료 시 기본 커서로 복원
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
