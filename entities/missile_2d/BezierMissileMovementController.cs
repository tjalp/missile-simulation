using Godot;

public class BezierMissileMovementController : Missile2DMovementController
{
    
    private Missile2DEntity _missile;
    public Vector2 Velocity;

    public BezierMissileMovementController(Missile2DEntity missile)
    {
        _missile = missile;
        Velocity = missile.StartVelocity;
    }

    public void NextPosition(double delta)
    {
        GD.Print("Der" + GetNextDerivative(_missile.Position, Velocity, 0f));
        GD.Print("Pos" + GetNextPosition(_missile.Position, Velocity, delta));
        // Velocity = GetNextDerivative(_missile.Position, Velocity * (float)delta, 0f);
        // _missile.Position = GetNextPosition(_missile.Position, Velocity, delta);
        _missile.Rotation = Velocity.Angle() + Mathf.Pi/2;
        
        _missile.Line.ClearPoints();
        _missile.Line.AddPoint(_missile.Position);
        
        for (var t = 0f; t <= 1f; t += .01f)
        {
            _missile.Line.AddPoint(GetNext(_missile.Position, Velocity, t));
        }
    }

    public Vector2 GetNextDerivative(Vector2 currentPosition, Vector2 currentVelocity, float t)
    {
        var mousePosition = _missile.GetViewport().GetMousePosition();
        var control1 = GetNextPosition(currentPosition, currentVelocity, 1f);
        // var control2 = new Vector2(mousePosition.X, control1.Y);
        var control2 = mousePosition - control1.Inverse();

        return QuadraticBezierDerivative(currentPosition, control1, mousePosition, t);
        // return currentPosition.BezierDerivative(control1, control2, mousePosition, t);
    }

    public Vector2 GetNext(Vector2 currentPosition, Vector2 currentVelocity, float t)
    {
        var mousePosition = _missile.GetViewport().GetMousePosition();
        var control1 = GetNextPosition(currentPosition, currentVelocity, 1f);
        // var control2 = new Vector2(mousePosition.X, control1.Y);
        var control2 = mousePosition - control1.Inverse();

        return QuadraticBezier(currentPosition, control1, mousePosition, t);
        return currentPosition.BezierInterpolate(control1, control2, mousePosition, t);
        // return QuadraticBezier(currentPosition, control1, mousePosition, t);
    }
    
    public Vector2 GetNextVelocity(Vector2 currentPosition, Vector2 currentVelocity, double delta, float t)
    {
        var mousePosition = _missile.GetViewport().GetMousePosition();
        var control1 = GetNextPosition(currentPosition, currentVelocity, 1/2);
        var control2 = mousePosition - control1;

        return currentPosition.BezierDerivative(control1, control2, mousePosition, t);
    }
    
    public Vector2 GetNextPosition(Vector2 currentPosition, Vector2 currentVelocity, double delta)
    {
        return currentPosition + currentVelocity * (float)delta;
    }
    
    // private Vector2 QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    // {
    //     Vector2 q0 = p0.Lerp(p1, t);
    //     Vector2 q1 = p1.Lerp(p2, t);
    //     Vector2 r = q0.Lerp(q1, t);
    //     return r;
    // }
    
    private Vector2 QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        Vector2 q0 = p0.Lerp(p1, t);
        Vector2 q1 = p1.Lerp(p2, t);
        Vector2 r = q0.Lerp(q1, t);
        return r;
    }

    private Vector2 QuadraticBezierDerivative(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        // Compute the derivative of each component separately
        Vector2 q0 = (p1 - p0) * 2.0f;
        Vector2 q1 = (p2 - p1) * 2.0f;
        
        // Linearly interpolate the derivatives
        Vector2 r = q0.Lerp(q1, t);
        return r;
    }
}