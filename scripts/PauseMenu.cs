using Godot;
using System;
using System.Threading.Tasks;

public partial class PauseMenu : CanvasLayer
{
    public void Resume()
    {
        GameManager.instance.Pause();
    }
    public async void MainMenu()
    {
        GameManager.instance.Pause();
        await SceneSwitcher.instance.SwitchSceneAsyncSlide("MainMenu");
    }
    public void NextLevel()
    {
        GetTree().Paused = false;
        GameManager.instance.NextLevel();
    }
}
