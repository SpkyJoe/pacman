using SFML.Graphics;
using SFML.System;
using System.Linq;

namespace Pacman;

public sealed class Ghost : Actor
{
    private float frozenTimer;
    
    public override void Create(Scene scene)
 {
  direction = -1;
  speed = 100.0f;
  moving = true;
  base.Create(scene);
  sprite.TextureRect = new IntRect(36, 0, 18, 18);
  scene.CandyEaten += OnCandyEaten;

 }

protected override int PickDirection(Scene scene)
 {
    List<int> validMoves = new List<int>();
    for (int i = 0; i < 4; i++)
    {
     if((i + 2) % 4 == direction) continue;
     if (IsFree(scene, i)) validMoves.Add(i);
         
    }
    int r = new Random().Next(0, validMoves.Count);
    return validMoves[r];
 }

    protected override void CollideWith(Scene scene, Entity e)
    {
        if (e is Pacman)
        {
            scene.PublishLostHealth(1);
            Reset();
        }
    }

    private void OnCandyEaten(Scene scene, int candyEaten)
    {
        
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
    }

    public override void Render(RenderTarget target)
    {
        if (frozenTimer > 0.0f)
        {
            sprite.Color = Color.Blue;
            base.Render(target);
        }
        else 
        base.Render(target);
    }
}