using Godot;
using System;

public partial class ScrollTexture : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] float parallax;
	[Export] CharacterBody2D player;

	public override void _Ready()
	{
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RegionRect = new Rect2(player.Position.X * parallax, player.Position.Y * parallax, 1920, 1080);
		Position = new Vector2(player.Position.X, player.Position.Y);
	}
}