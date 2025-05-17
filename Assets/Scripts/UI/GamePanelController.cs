using UnityEngine;

//Button functions for the upper HUD in the game view
public class GamePanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject debugWindow;
    [SerializeField]
    private GameObject debugBtn;

    public void ToggleDebugger()
    {
        debugWindow.SetActive(!debugWindow.activeSelf);
        debugBtn.SetActive(!debugBtn.activeSelf);
    }

    public void OpenMenu()
    {
        GameManager.Instance.TogglePause();
    }
}
