using Godot;

public class MaximumAngleController : Missile2DMovementController
{
    
    private Missile2DEntity _missile;
    private Vector2 Velocity;

    public MaximumAngleController(Missile2DEntity missile)
    {
        _missile = missile;
        Velocity = missile.StartVelocity;
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
            // velocity = GetNextVelocity(position, velocity, delta);
            position = GetNextPosition(position, velocity, delta);
            
            _missile.Line.AddPoint(position);
        }
    }

    public Vector2 GetNextVelocity(Vector2 currentPosition, Vector2 currentVelocity, double delta)
    {
        var mousePosition = _missile.GetViewport().GetMousePosition();
        var targetVector = mousePosition - currentPosition;
        var accelerationVector = (mousePosition - currentPosition).Normalized() * (_missile.Acceleration * (float)delta);
        var angle = currentVelocity.AngleTo(targetVector);
        var toChangeAngle = Mathf.Clamp(angle, _missile.MaximumTurnRate * -1, _missile.MaximumTurnRate);
        var turnTime = toChangeAngle / angle;
        
        GD.Print("Angle: " + angle + "\nToChange: " + toChangeAngle);

        return currentVelocity.Rotated(toChangeAngle);
        // return currentVelocity + accelerationVector;
    }
    
    public Vector2 GetNextPosition(Vector2 currentPosition, Vector2 currentVelocity, double delta)
    {
        return currentPosition + currentVelocity * (float)delta;
    }
}