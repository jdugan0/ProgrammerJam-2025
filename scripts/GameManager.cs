using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager instance;
    public Movement player;
    [Export] public int levelUnlocked = 1;
    public string currentLevel;
    public string[] levels;
    public int currentLevelID;
    public override void _Ready()
    {
        instance = this;
    }

    public Movement.MovementState GetMovementState()
    {
        if (player == null)
        {
            return Movement.MovementState.SIDE;
        }
        return player.GetMovementState();
    }
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("PAUSE"))
        {
            Pause();
        }
    }

    public void RestartLevel()
    {
        SceneSwitcher.instance.SwitchScene(currentLevel);
    }

    public void ChooseLevel(int level)
    {
        SceneSwitcher.instance.SwitchScene(levels[level - 1]);
		currentLevel = levels[level - 1];
		currentLevelID = level;
    }

    public void NextLevel()
    {
        ChooseLevel(currentLevelID + 1);
    }

    public void Pause()
    {
        GetTree().Paused = !GetTree().Paused;
        ((CanvasLayer)GetTree().GetNodesInGroup("PauseMenu")[0]).Visible = !((CanvasLayer)GetTree().GetNodesInGroup("PauseMenu")[0]).Visible;
    }
}
