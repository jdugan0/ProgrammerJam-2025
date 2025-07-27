using Godot;
using System;
using System.Threading.Tasks;

public partial class Level_UI : Control
{
	[Export] public string[] levels;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.instance.levels = levels;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ChooseLevel(int level)
	{
		GameManager.instance.ChooseLevel(level);
	}
	public async void Back()
	{
		await SceneSwitcher.instance.SwitchSceneAsyncSlide("MainMenu");
	}
}
