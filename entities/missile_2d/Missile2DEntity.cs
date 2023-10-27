using Godot;

public partial class Missile2DEntity : Area2D
{

    [Export] public float Acceleration { get; set; } = 1000.0f;
    [Export] public float MinimumSpeed { get; set; } = 100f;
    [Export] public float MaximumTurnRate { get; set; } = Mathf.Pi/1.2f;

    public Vector2 Velocity = Vector2.Zero;

    public override void _Process(double delta)
    {
        Position += Velocity * (float)delta;
        
        Rotation = Velocity.Angle() + Mathf.Pi/2;
    }
}
