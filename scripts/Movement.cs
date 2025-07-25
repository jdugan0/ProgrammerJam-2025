using Godot;
using System;
using System.Data;

public partial class Movement : CharacterBody2D
{
    public enum MovementState
    {
        SIDE,
        TOP
    }
    public MovementState movementState = MovementState.SIDE;

    // top down constants:
    [Export] public float topDownSpeed = 200f;


    // side constants:
    [Export] public float sideSpeed = 300f * 1.6f;
    [Export] public float sideAirSpeed = 200f * 1.6f;
    [Export] public float jumpHeight = 72f;
    [Export] public float accel = 30f * 1.6f;
    [Export] public float airAccel = 15f * 1.6f;
    [Export] public float drag = 5f * 1.6f;
    [Export] public float gravity = 1400f;
    [Export] public float jumpCutMultiplier = 0.4f;
    [Export] public float maxFallSpeed = 2000f;
    [Export] public float friction = 10f * 1.6f;
    [Export] public float coyoteTime = 1f;
    [Export] public float jumpBuffer = 1f;

    // Timers & Flags
    float coyoteTimer = 0;
    float jumpTimer = 0;
    bool jumpFlag = false;

    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("SWITCH"))
        {
            if (movementState == MovementState.SIDE)
            {
                movementState = MovementState.TOP;
            }
            else
            {
                movementState = MovementState.SIDE;
            }
        }
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
        Vector2 v = Velocity;

        if (IsOnFloor())
        {
            coyoteTimer = 0f;
        }

        float accelActual = IsOnFloor() ? accel : airAccel;

        int direction = 0;

        if (Input.IsActionPressed("LEFT"))
        {
            direction = -1;
        }
        if (Input.IsActionPressed("RIGHT"))
        {
            direction += 1;
        }

        float maxSpeed = IsOnFloor() ? sideSpeed : sideAirSpeed;
        v.X += accelActual * (float)delta * direction;
        if (Math.Abs(v.X) >= maxSpeed) v.X = Mathf.Sign(v.X) * maxSpeed;
        if (direction == 0)
        {
            float dv;
            if (IsOnFloor())
            {
                dv = Mathf.Sign(v.X) * friction * (float)delta;
            }
            else
            {
                dv = Mathf.Abs(v.X) * v.X * drag * (float)delta;
            }
            if (Mathf.Abs(dv) > Mathf.Abs(v.X))
                v.X = 0;
            else
                v.X -= dv;
        }

        if (Input.IsActionJustPressed("UP"))
        {
            if (coyoteTimer < coyoteTime)
                coyoteTimer += 100000;
            if (!IsOnFloor())
            {
                jumpFlag = true;
                jumpTimer = 0;
            }
            else
            {
                v.Y = -Mathf.Sqrt(2f * gravity * jumpHeight);
            }
        }

        if (IsOnFloor() && jumpFlag)
        {
            v.Y = -Mathf.Sqrt(2f * gravity * jumpHeight);
            jumpFlag = false;
        }

        if (!IsOnFloor())
        {
            coyoteTimer += (float)delta;
            jumpTimer += (float)delta;
            if (jumpTimer > jumpBuffer) jumpFlag = false;
            v.Y += gravity * (float)delta;
            if (v.Y > maxFallSpeed) v.Y = maxFallSpeed;
        }
        // cut velocity for variable jump height
        if (!Input.IsActionPressed("UP") && v.Y < 0)
        {
            v.Y *= jumpCutMultiplier;
        }
        if (direction != 0)
        {
            if (direction == 1)
            {
                Rotation = Mathf.Pi / 2;
            }
            else
            {
                Rotation = -Mathf.Pi / 2;
            }
        }
        Velocity = v;
        MoveAndSlide();
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
