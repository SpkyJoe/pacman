using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class Candy : Entity
{
    public Candy() : base("pacman")
    {
        
    }
    
    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(72, 54, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }
}