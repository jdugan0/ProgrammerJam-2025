using Godot;
using System;

public partial class Level_UI : Control
{
	[Export] public string[] levels;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ChooseLevel(int level)
	{
		SceneSwitcher.instance.SwitchScene(levels[level - 1]);
		GameManager.instance.currentLevel = levels[level - 1];
	}
	public void Back()
	{
		SceneSwitcher.instance.SwitchScene("MainMenu");
	}
}
