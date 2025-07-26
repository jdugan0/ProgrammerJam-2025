using Godot;
using System;

public partial class Hazard : Node
{
    [Export] public bool topDown = false;
    public void OnCol(Node2D node)
    {
        GD.Print("HI");
        if (GameManager.instance.GetMovementState() == Movement.MovementState.TOP && topDown && node is Movement)
        {
            node.QueueFree();
        }
        if (GameManager.instance.GetMovementState() == Movement.MovementState.SIDE && !topDown && node is Movement)
        {
            node.QueueFree();
        }
    }
}
