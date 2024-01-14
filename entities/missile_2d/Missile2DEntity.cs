using Godot;

public partial class Missile2DEntity : Area2D
{

    [Export] public float Acceleration { get; set; } = 1000.0f;
    [Export] public float MinimumSpeed { get; set; } = 100f;
    [Export] public float MaximumTurnRate { get; set; } = Mathf.Pi;
    [Export] public Vector2 StartVelocity;
    [Export] public Line2D Line;

    private Missile2DMovementController _movementController;

    public override void _Ready()
    {
        _movementController = new BezierMissileMovementController(this);
    }

    public override void _Process(double delta)
    {
        _movementController.NextPosition(delta);
    }
}
