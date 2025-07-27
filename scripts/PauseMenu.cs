using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
    public void Resume()
    {
        GameManager.instance.Pause();
    }
    public void MainMenu()
    {
        GameManager.instance.Pause();
        SceneSwitcher.instance.SwitchScene("MainMenu");
    }
    public void NextLevel()
    {
        GameManager.instance.NextLevel();
    }
}
