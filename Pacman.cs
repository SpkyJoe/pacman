using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Pacman : Entity
{
    public Pacman() : base("pacman")
    { }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
      
    }


   
}