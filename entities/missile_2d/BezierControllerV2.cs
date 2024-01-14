using System;
using Godot;

namespace RocketModel.entities.missile_2d;

public class BezierControllerV2 : Missile2DMovementController
{
    private Missile2DEntity _missile;
    public Vector2 Velocity = Vector2.Up * 500f;

    public BezierControllerV2(Missile2DEntity missile)
    {
        _missile = missile;
    }

    public void NextPosition(double delta)
    {
        _missile.Rotation = Velocity.Angle() + Mathf.Pi/2;
        
        _missile.Line.ClearPoints();
        _missile.Line.AddPoint(_missile.Position);
        
        for (var t = 0f; t <= 1f; t += .01f)
        {
            _missile.Line.AddPoint(GetNext(_missile.Position, Velocity, t));
        }
    }

    public Vector2 GetNext(Vector2 currentPosition, Vector2 currentVelocity, float t)
    {
        throw new NotImplementedException();
    }
}