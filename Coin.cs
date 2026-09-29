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
        sprite.Origin = new Vector2f(9, 9);
    }

    public override void Update(Scene scene, float deltaTime)
    {

    }
}