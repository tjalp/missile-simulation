using Godot;

/**
 * Uses simple velocity and acceleration vectors.
 * The acceleration vector is determined by subtracting
 * the missile position from the mouse position (both
 * vectors), normalizing it and multiplying it by the
 * fixed acceleration (set in the editor).
 * The acceleration is then added onto the velocity.
 *
 * Pros: Very simple.
 * Cons: Almost always misses and it's possible to
 * accidentally orbit the target.
 */
public class VectorMissileMovementController : Missile2DMovementController
{

    private Missile2DEntity _missile;
    public Vector2 Velocity = Vector2.Zero;

    public VectorMissileMovementController(Missile2DEntity missile)
    {
        _missile = missile;
    }

    public void NextPosition(double delta)
    {
        Velocity = GetNextVelocity(_missile.Position, Velocity, delta);
        _missile.Position = GetNextPosition(_missile.Position, Velocity, delta);
        _missile.Rotation = Velocity.Angle() + Mathf.Pi/2;
        
        _missile.Line.ClearPoints();
        _missile.Line.AddPoint(_missile.Position);

        var velocity = Velocity;
        var position = _missile.Position;
        
        for (int i = 0; i * delta < 3; i++)
        {
            velocity = GetNextVelocity(position, velocity, delta);
            position = GetNextPosition(position, velocity, delta);
            
            _missile.Line.AddPoint(position);
        }
    }

    public Vector2 GetNextVelocity(Vector2 currentPosition, Vector2 currentVelocity, double delta)
    {
        var mousePosition = _missile.GetViewport().GetMousePosition();
        var accelerationVector = (mousePosition - currentPosition).Normalized() * (_missile.Acceleration * (float)delta);

        return currentVelocity + accelerationVector;
    }
    
    public Vector2 GetNextPosition(Vector2 currentPosition, Vector2 currentVelocity, double delta)
    {
        return currentPosition + currentVelocity * (float)delta;
    }
}