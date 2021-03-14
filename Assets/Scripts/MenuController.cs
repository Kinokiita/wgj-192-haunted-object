using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private GameObject optionsMenu;

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ExitGame();
        }
    }

    /**
     * Tries to load the given level scene. Not failsafe
     */
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }

    public void LoadMainMenu()
    {
        LoadMenu(MenuType.MAIN_MENU);
    }

    public void LoadOptionsMenu()
    {
        LoadMenu(MenuType.OPTIONS);
    }

    public void LoadLevelSelection()
    {
        LoadMenu(MenuType.LEVEL_SELECTION);
    }

    private void LoadMenu(MenuType menuType)
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelSelection.SetActive(false);
        switch (menuType)
        {
            case MenuType.MAIN_MENU:
                mainMenu.SetActive(true);
                break;
            case MenuType.OPTIONS:
                optionsMenu.SetActive(true);
                break;
            case MenuType.LEVEL_SELECTION:
                levelSelection.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(menuType), menuType, null);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private enum MenuType
    {
        MAIN_MENU,
        OPTIONS,
        LEVEL_SELECTION
    }
}