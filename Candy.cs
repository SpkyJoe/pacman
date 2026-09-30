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
       
    }
    protected override void CollideWith(Scene scene, Entity e)
    {
        if (e is Pacman)
        {
            Dead = true;
            scene.PublishCandyEaten(1);
           
        }
    }
}
