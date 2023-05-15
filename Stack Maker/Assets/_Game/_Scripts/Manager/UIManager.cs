using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainMenuUI;
    public GameObject finishUI;

    public void OpenMainMenuUI()
    {
        mainMenuUI.SetActive(true);
        finishUI.SetActive(false);
    }
    public void OpenFinishUI()
    {
        mainMenuUI.SetActive(false);
        finishUI.SetActive(true);  
    }

    public void PlayButton()
    {
        mainMenuUI.SetActive(false);
        LevelManager.Instance.OnStart();
        //GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    public void RetryButton() 
    {
        LevelManager.Instance.LoadLevelX();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OpenMainMenuUI();
    }

    public void NextButton()
    {
        LevelManager.Instance.NextLevel();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OpenMainMenuUI();
    }
}
