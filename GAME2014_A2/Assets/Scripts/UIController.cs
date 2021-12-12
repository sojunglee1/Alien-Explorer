using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("On Screen Controls")]
    public GameObject onScreenControls;

    [Header("Button Control Events")]
    public static bool jumpButtonDown;
    public static bool shootButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        CheckPlatform();
    }

    private void CheckPlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.WindowsEditor:
                onScreenControls.SetActive(true);
                break;
            default:
                onScreenControls.SetActive(false);
                break;
        }
    }

    public void OnJumpButton_Down()
    {
        jumpButtonDown = true;
    }

    public void OnJumpButton_Up()
    {
        jumpButtonDown = false;
    }
    public void OnShootButton_Down()
    {
        shootButtonDown = true;
    }
    public void OnShootButton_Up()
    {
        shootButtonDown = false;
    }
}
