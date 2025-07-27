using Godot;
using System;

public partial class LevelButton : TextureButton
{
	[Export] public int levelID = 1;
	[Export] public Level_UI level_UI;
	public override void _Ready()
	{
		if (GameManager.instance.levelUnlocked < levelID)
		{
			Disabled = true;
		}
	}

	public void OnPress()
	{
		level_UI.ChooseLevel(levelID);
	}

}
