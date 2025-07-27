using Godot;
using System;

public partial class Hazard : Area2D
{
    [Export] public bool topDown = false;
    public void OnCol(Node2D node)
    {
        // GD.Print("HI");
        if (GameManager.instance.GetMovementState() == Movement.MovementState.TOP && topDown && node is Movement)
        {
            node.QueueFree();
            GameManager.instance.RestartLevel();
        }
        if (GameManager.instance.GetMovementState() == Movement.MovementState.SIDE && !topDown && node is Movement)
        {
            node.QueueFree();
            GameManager.instance.RestartLevel();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        foreach (var node in GetOverlappingBodies())
        {
            OnCol(node);
        }
    }
}
