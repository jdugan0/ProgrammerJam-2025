using Godot;
using System;
using System.Threading.Tasks;

public partial class MainMenu : Control
{
    public void Levels()
    {
        SceneSwitcher.instance.SwitchScene("level_manager");
    }
    public void Quit()
    {
        GetTree().Quit();
    }
    public void Options()
    {
        
    }
}
