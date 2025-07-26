using Godot;
using System;

public partial class LevelComplete : Area2D
{
    public void OnCol(Node2D body)
    {
        GD.Print("hi");
        if (body is Movement)
        {
            SceneSwitcher.instance.SwitchScene("level_manager");
            GameManager.instance.levelUnlocked += 1;
        }
    }
}
