using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    private bool _isLockCursor;
    public bool IsLockCursor => _isLockCursor;

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        if (GameManager.Instance.AutoMode) return;
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            ToggleCursor();
        }
    }

    private void ToggleCursor()
    {
        if (_isLockCursor)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _isLockCursor = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isLockCursor = false;
    }
}