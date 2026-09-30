using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Coin : Entity
{
    public Coin() : base("pacman") {}

    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(36, 36, 18, 18);
        
    }
    protected override void CollideWith(Scene scene, Entity e)
    {
        if (e is Pacman)
        {
            Dead = true;
            scene.PublishGainedScore(100);
        }
    }
}