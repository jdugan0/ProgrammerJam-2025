using Godot;
using System;

public partial class Movement : CharacterBody2D
{
    public enum MovementState
    {
        SIDE,
        TOP
    }
    private MovementState movementState = MovementState.SIDE;

    [Export] AnimatedSprite2D sprite;

    // top down constants:
    [Export] private float topDownSpeed = 200f;


    // side constants:
    [Export] private float sideSpeed = 300f * 1.6f;
    [Export] private float sideAirSpeed = 200f * 1.6f;
    [Export] private float jumpHeight = 72f;
    [Export] private float accel = 30f * 1.6f;
    [Export] private float airAccel = 15f * 1.6f;
    [Export] private float drag = 5f * 1.6f;
    [Export] private float gravity = 1400f;
    [Export] private float jumpCutMultiplier = 0.4f;
    [Export] private float maxFallSpeed = 2000f;
    [Export] private float friction = 10f * 1.6f;
    [Export] private float coyoteTime = 1f;
    [Export] private float jumpBuffer = 1f;

    [Export] private float pushForce = 15;

    [Export] private float maxImpulse = 100f;

    [Export] Shape2D topDownShape;
    [Export] Shape2D sideShape;

    [Export] CollisionShape2D colObject;

    // Timers & Flags
    float coyoteTimer = 0;
    float jumpTimer = 0;
    bool jumpFlag = false;

    public override void _Ready()
    {

    }

    public MovementState GetMovementState()
    {
        return movementState;
    }

    private void PushRigidBodies(float dt, Vector2 preVel)
    {
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            var col = GetSlideCollision(i);
            if (col.GetCollider() is RigidBody2D rb && rb.IsInGroup("pushable"))
            {
                Vector2 n = col.GetNormal();         // collider → you
                float intoBox = -preVel.Dot(n);      // positive if moving into collider

                if (intoBox > 0f)
                {
                    Vector2 pushDir = -n;            // you → collider
                    Vector2 impulse = pushDir * intoBox * pushForce * dt;
                    if (impulse.Length() > maxImpulse)
                    {
                        impulse = impulse.Normalized() * maxImpulse;
                    }

                    if (!rb.TestMove(rb.GlobalTransform, impulse * dt))
                    {
                        rb.ApplyImpulse(impulse);
                    }
                    else
                    {
                        // Box can’t move → stop us
                        Velocity = Velocity.Slide(n); // or Vector2.Zero
                    }
                }
            }
        }
    }




    public override void _PhysicsProcess(double delta)
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
                MotionMode = MotionModeEnum.Grounded;
                sprite.Play("SIDE");
                MovementStateSide(delta);
                colObject.Shape = topDownShape;
                break;
            case MovementState.TOP:
                MotionMode = MotionModeEnum.Floating;
                colObject.Shape = sideShape;
                sprite.Play("TOP");
                MovementStateTop(delta);
                Vector2 preVel = Velocity;
                MoveAndSlide();
                PushRigidBodies((float)delta, preVel);
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
        Rotation = 0;
        // cut velocity for variable jump height
        if (!Input.IsActionPressed("UP") && v.Y < 0)
        {
            v.Y *= jumpCutMultiplier;
        }
        if (direction != 0)
        {
            if (direction == 1)
            {
                sprite.FlipH = false;
            }
            else
            {
                sprite.FlipH = true;
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
            float angle = inputDirection.Angle();
            Rotation = angle;
        }
    }
}
