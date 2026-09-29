using SFML.Graphics;
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


    public override void Update(Scene scene, float deltaTime)
    { }
}