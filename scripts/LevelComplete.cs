using Godot;
using System;

public partial class LevelComplete : Area2D
{
    public void OnCol(Node2D body)
    {
        // GD.Print("hi");
        if (body is Movement)
        {
            ((CanvasLayer)GetTree().GetNodesInGroup("WinMenu")[0]).Visible = true;
            GetTree().Paused = true;
            if (GameManager.instance.levelUnlocked == GameManager.instance.currentLevelID)
            {
                GameManager.instance.levelUnlocked += 1;
            }
        }
    }
}
