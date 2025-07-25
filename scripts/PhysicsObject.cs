using Godot;
using System;

public partial class PhysicsObject : RigidBody2D
{
    [Export] bool gravity;
    public override void _Process(double delta)
    {
        if (gravity)
        {
            if (((GameManager)GetTree().GetNodesInGroup("GameManager")[0]).GetMovementState() == Movement.MovementState.SIDE)
            {
                GravityScale = 5.0f;
            }
            else
            {
                GravityScale = 0.0f;
            }
        }
    }

}
