using SFML.Graphics;
using SFML.System;
using System.Linq;

namespace Pacman;

public sealed class Ghost : Actor
{
    private float frozenTimer;
    private float respawnTimer;
    private IntRect frame1 = new IntRect(36, 0, 18, 18);
    private IntRect frame2 = new IntRect(54, 0, 18, 18);
    
    public override void Create(Scene scene)
 {
  direction = -1;
  speed = 100.0f;
  moving = true;
  base.Create(scene);
  sprite.TextureRect = frame1;
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
            if (frozenTimer > 0.0f)
            {
                Position = originalPosition;
                moving = false;
                respawnTimer = 1.5f;
            }
            else
            {
                scene.PublishLostHealth(1);
                Reset();
            }

            

        }
    }

    private void OnCandyEaten(Scene scene, int candyEaten)
    {
        frozenTimer = 5.0f;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
        respawnTimer = MathF.Max(respawnTimer - deltaTime, 0.0f);
        if(respawnTimer <= 0.0f && !moving) moving = true;
    }

    public override void Render(RenderTarget target)
    {
        if (sprite.TextureRect != frame1 && animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.4f)
        {
            animateClock.Restart();
            animationTimer = animateClock.ElapsedTime.AsSeconds();
            sprite.TextureRect = frame1;
        }
        else if (animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.2f &&
                 sprite.TextureRect != frame2)
        {
            sprite.TextureRect = frame2;
        }
        
        if (frozenTimer > 0.0f)
        {
            sprite.Color = Color.Blue;
            
        }
        else sprite.Color = Color.White;
        base.Render(target);
    }
}