using Godot;

public partial class Missile2D : Node
{

    [Export] public Missile2DEntity Missile;
    [Export] public Line2D Line;
    [Export] public Label FpsCounter;
    
    public override void _Ready()
    {
        // SetProcess(false);
    }

    public override void _Process(double delta)
    {
        FpsCounter.Text = Performance.GetMonitor(Performance.Monitor.TimeFps) + " fps";
        // VectorMethod1(delta, true);
        // VectorMethod2(delta, true);
        // LerpMethod(delta, true);
    }
    
    // public override void _Input(InputEvent @event)
    // {
    //     if (@event.IsActionPressed("ui_accept"))
    //     {
    //         SetProcess(!IsProcessing());
    //     }
    // }

    // /*
    //  * Uses simple linear interpolation to hit the target.
    //  * Not optimal and misses.
    //  */
    // private void LerpMethod(double delta, bool showPath)
    // {
    //     var mousePosition = GetViewport().GetMousePosition();
    //     var vector = mousePosition - Missile.Position;
    //     var lerp = .75f * (float)delta;
    //
    //     Missile.Velocity = Missile.Velocity.Lerp(vector, lerp);
    //
    //     if (!showPath) return;
    //     
    //     Line.ClearPoints();
    //     
    //     var position = Missile.Position;
    //     var velocity = Missile.Velocity;
    //
    //     for (int i = 0; i < 1000; i++)
    //     {
    //         Line.AddPoint(position);
    //         
    //         var vectorPrediction = mousePosition - position;
    //         
    //         position += velocity * (float)delta;
    //         velocity = velocity.Lerp(vectorPrediction, lerp);
    //     }
    // }
    //
    // /**
    //  * Uses simple velocity and acceleration vectors.
    //  * The acceleration vector is determined by subtracting
    //  * the missile position from the mouse position (both
    //  * vectors), normalizing it and multiplying it by the
    //  * fixed acceleration (set in the editor).
    //  * The acceleration is then added onto the velocity.
    //  *
    //  * Pros: Very simple.
    //  * Cons: Almost always misses and it's possible to
    //  * accidentally orbit the target.
    //  */
    // private void VectorMethod1(double delta, bool showPath)
    // {
    //     var mousePosition = GetViewport().GetMousePosition();
    //     var accelerationVector = (mousePosition - Missile.Position).Normalized() * (Missile.Acceleration * (float)delta);
    //     
    //     Missile.Velocity += accelerationVector;
    //
    //     if (!showPath) return;
    //     
    //     Line.ClearPoints();
    //
    //     var position = Missile.Position;
    //     var velocity = Missile.Velocity;
    //
    //     for (int i = 0; i < 1000; i++)
    //     {
    //         Line.AddPoint(position);
    //         
    //         var accelerationVectorPrediction = (mousePosition - position).Normalized() * (Missile.Acceleration * (float)delta);
    //         
    //         position += velocity * (float)delta;
    //         velocity += accelerationVectorPrediction;
    //     }
    // }
    //
    // /*
    //  * https://stackoverflow.com/questions/18733036/why-isnt-my-homing-missile-algorithm-working
    //  *
    //  * 
    //  */
    // private void VectorMethod2(double delta, bool showPath)
    // {
    //     var mousePosition = GetViewport().GetMousePosition();
    //     var normalizedDirection = (mousePosition - Missile.Position).Normalized();
    //     
    //     // GD.Print(Missile.Position);
    //
    //     if (Missile.Velocity == Vector2.Zero) Missile.Velocity = normalizedDirection * Missile.MinimumSpeed;
    //     
    //     var normalizedVelocity = Missile.Velocity.Normalized();
    //     var angle = Mathf.Acos(normalizedDirection.Dot(normalizedVelocity));
    //     var crossProduct = normalizedDirection.Cross(normalizedVelocity);
    //     var sign = crossProduct > 0 ? -1 : 1;
    //     var maxAngle = sign * Mathf.Clamp(angle, -Missile.MaximumTurnRate, Missile.MaximumTurnRate);
    //     var newVelocity = Missile.Velocity.Rotated(maxAngle * (float)delta);
    //     
    //     GD.Print(angle);
    //     GD.Print(maxAngle);
    //
    //     Missile.Velocity = newVelocity;
    // }
}
