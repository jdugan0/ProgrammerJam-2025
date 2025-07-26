using Godot;
using System;

public partial class NoSwap : Node
{
    public void OnEnter(Node2D body)
    {
        if (body is Movement m)
        {
            m.noSwap = true;
        }
    }
    public void OnExit(Node2D body)
    {
        if (body is Movement m)
        {
            m.noSwap = false;
        }
    }
}
