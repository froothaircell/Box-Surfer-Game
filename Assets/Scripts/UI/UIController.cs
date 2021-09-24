using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;

    public void EnableRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void DisableRestartButton()
    {
        restartButton.SetActive(false);
    }
}
