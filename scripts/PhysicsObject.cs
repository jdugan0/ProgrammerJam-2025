using Godot;
using System;

public partial class PhysicsObject : RigidBody2D
{
    [Export] public bool gravity;
    [Export] public float maxV = 100f;
    public override void _Process(double delta)
    {
        if (gravity)
        {
            if (LinearVelocity.Length() > maxV)
            {
                LinearVelocity = LinearVelocity.Normalized() * maxV;
            }
        }
    }


}
