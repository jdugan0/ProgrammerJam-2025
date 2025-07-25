using Godot;
using System;

public partial class Movement : CharacterBody2D
{
    public enum MovementState
    {
        SIDE,
        TOP
    }
    public MovementState movementState = MovementState.TOP;

    // top down constants:
    [Export] public float topDownSpeed = 200f;
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
        Vector2 inputDirection = Input.GetVector("LEFT", "RIGHT", "UP", "DOWN");
        Velocity = inputDirection * topDownSpeed;
        if (inputDirection.Length() > 0)
        {
            float angle = inputDirection.Angle() + Mathf.Pi / 2;
            Rotation = angle;
        }
        MoveAndSlide();
    }
}
