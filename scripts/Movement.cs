using Godot;
using System;

public partial class Movement : CharacterBody2D
{
    public enum MovementState
    {
        SIDE,
        TOP
    }
    [Export] public MovementState movementState = MovementState.SIDE;
    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
        switch (movementState)
        {
            case MovementState.SIDE:
                MovementStateSide(delta);
                break;
            case MovementState.TOP:
                MovementStateTop(delta);
                break;
            default:
                break;
        }
    }

    public void MovementStateSide(double delta)
    {

    }
    public void MovementStateTop(double delta)
    {
        
    }
}
