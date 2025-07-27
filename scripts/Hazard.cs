using Godot;
using System;
using System.Threading.Tasks;

public partial class Hazard : Area2D
{
    [Export] public bool topDown = false;

    public async void OnCol(Node2D node)
    {
        if (!(node is Movement)) return;
        if (((Movement)node).killed) return;
        bool kill = GameManager.instance.GetMovementState() == Movement.MovementState.TOP && topDown && node is Movement
             || GameManager.instance.GetMovementState() == Movement.MovementState.SIDE && !topDown && node is Movement;
        if (!kill) return;
        ((Movement)node).killed = true;
        var clipName = topDown ? "scream" : "stab";
        AudioStreamPlayer p = AudioManager.instance.PlaySFX(clipName);
        if (topDown)
        {
            float dur = (float)p.Stream.GetLength();
            Tween tween = node.CreateTween();
            tween.SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.Out);
            tween.TweenProperty(node, "scale", Vector2.Zero, dur);
        }
        await ToSignal(p, AudioStreamPlayer.SignalName.Finished);
        GameManager.instance.RestartLevel();
    }

    public override void _PhysicsProcess(double delta)
    {
        foreach (var node in GetOverlappingBodies())
        {
            OnCol(node);
        }
    }
}
