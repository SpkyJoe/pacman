using SFML.Graphics;
using SFML.System;
using System.Linq;

namespace Pacman;

public class Actor : Entity
{
    private bool wasAligned;
    protected float speed;
    protected int direction;
    protected bool moving;
    protected Vector2f originalPosition ;
    protected float originalSpeed;
    protected float animationTimer;
    protected Clock animateClock = new Clock();

    protected Actor() : base("pacman")
    {
        moving = false;
        Position  = new Vector2f();
    }

    protected void Reset()
    {
        wasAligned = false;
        speed = originalSpeed;
        Position = originalPosition;
    }
    protected bool IsAligned =>
        (int)MathF.Floor(Position.X) % 18 == 0 &&
        (int)MathF.Floor(Position.Y) % 18 == 0;
    
    protected static Vector2f ToVector(int dir)
    {
        if (dir==0) return new Vector2f(1, 0); //Höger
        if (dir==1) return new Vector2f(0, -1); //Upp
        if (dir==2) return new Vector2f(-1, 0); //Vänster
        if (dir == 3) return new Vector2f(0,1); //Ner
        return new Vector2f(0, 0); //Stilla, om nåt går fel.
    }

    protected bool IsFree(Scene scene, int dir)
    {
        Vector2f at = Position + new Vector2f(9, 9);
        at += 18 * ToVector(dir);
        FloatRect rect = new FloatRect(at.X, at.Y, 1, 1);
        return !scene.FindIntersects(rect).Any(e => e.Solid);
    }

    protected virtual int PickDirection(Scene scene) { return 0; }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        originalPosition = Position;
        originalSpeed = speed;
        Reset();
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (IsAligned)
        {
            if (!wasAligned)
            {
                direction = PickDirection(scene);
            }

            if (moving)
            {
                wasAligned = true;
            }
            
        }
        else
        {
            wasAligned = false;
        }

        if (!moving) return;
        Position += ToVector(direction) * (speed * deltaTime);
        Position = MathF.Floor(Position.X) switch
        {
            < 0 => new Vector2f(432, Position.Y),
            > 432 => new Vector2f(0, Position.Y),
            _ => Position
        };
    }
    
}