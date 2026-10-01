using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Pacman;

public sealed class Pacman : Actor
{

    private IntRect moveRight = new IntRect(0, 0, 18, 18);
    private IntRect moveUp = new IntRect(0, 18, 18, 18);
    private IntRect moveLeft = new IntRect(0, 36, 18, 18);
    private IntRect moveDown = new IntRect(0, 54, 18, 18);
    private IntRect moveStill = new IntRect(36, 54, 18, 18);
    private IntRect moveRight2 = new IntRect(18, 0, 18, 18);
    private IntRect moveUp2 = new IntRect(18, 18, 18, 18);
    private IntRect moveLeft2 = new IntRect(18, 36, 18, 18);
    private IntRect moveDown2 = new IntRect(18, 54, 18, 18);
    
    
    public override void Create(Scene scene)
    {
        speed = 100.0f;
        base.Create(scene);
        sprite.TextureRect = moveStill;
        scene.LoseHealth += OnLoseHealth;

    }

    protected override int PickDirection(Scene scene)
    {
        int dir = direction;
        if (Keyboard.IsKeyPressed(Right))
        {
            dir = 0;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Up))
        {
            dir = 1;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Left))
        {
            dir = 2;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Down))
        {
            dir = 3;
            moving = true;
        }

        if (IsFree(scene, dir)) return dir;
        if (!IsFree(scene, direction)) moving = false;
        return direction;
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        Reset();
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.LoseHealth -= OnLoseHealth;
    }

    public override void Render(RenderTarget target)
    { 
        switch (direction)
        {
            case 0:
                if (sprite.TextureRect != moveRight && animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.5f)
                {
                    animateClock.Restart();
                    animationTimer = animateClock.ElapsedTime.AsSeconds();
                    sprite.TextureRect = moveRight;
                }

                else if (animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.25f &&
                         sprite.TextureRect != moveRight2)
                {
                    sprite.TextureRect = moveRight2;
                }

                break;
            case 1:
                if (sprite.TextureRect != moveUp && animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.5f)
                {
                    animateClock.Restart();
                    animationTimer = animateClock.ElapsedTime.AsSeconds();
                    sprite.TextureRect = moveUp;
                }
                else if (animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.25f && sprite.TextureRect != moveUp2)
                {
                    sprite.TextureRect = moveUp2;
                }

                break;
            case 2:
                if (sprite.TextureRect != moveLeft && animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.5f)
                {
                    animateClock.Restart();
                    animationTimer = animateClock.ElapsedTime.AsSeconds();
                    sprite.TextureRect = moveLeft;
                }
                else if (animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.25f &&
                         sprite.TextureRect != moveLeft2)
                {
                    sprite.TextureRect = moveLeft2;
                }

                break;
            case 3:
                if (sprite.TextureRect != moveDown && animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.5f)
                {
                    animateClock.Restart();
                    animationTimer = animateClock.ElapsedTime.AsSeconds();
                    sprite.TextureRect = moveDown;
                }
                else if (animateClock.ElapsedTime.AsSeconds() > animationTimer + 0.25f &&
                         sprite.TextureRect != moveLeft2)
                {
                    sprite.TextureRect = moveDown2;
                }

                break;
        }
        base.Render(target);
    }
}