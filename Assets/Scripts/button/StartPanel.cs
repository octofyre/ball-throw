using UnityEngine;

public class StartPanel : MonoBehaviour
{
    public GameObject panel;

    public void StartGame()
    {
        panel.SetActive(false);
    }
}