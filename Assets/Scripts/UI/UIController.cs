using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;

    private void Awake()
    {
        restartButton.SetActive(false);
    }

    public void EnableRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void DisableRestartButton()
    {
        restartButton.SetActive(false);
    }
}
