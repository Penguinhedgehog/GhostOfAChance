using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject controlScheme;
    [SerializeField] GameObject menu;
    
    public void DisplayControls() {
        controlScheme.SetActive(true);
    }

    public void CloseControls() {
        controlScheme.SetActive(false);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
