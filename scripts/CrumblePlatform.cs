using Godot;
using System;

public partial class CrumblePlatform : Area2D
{
	[Export] public AnimationPlayer p;
	public void do_something(Node2D node)
	{
		if (node is Movement)
		{
			p.Play("crumble");
		}

	}
}
