using SFML.Graphics;
namespace Pacman;

public sealed class Ghost : Entity
{
 public Ghost() : base("packman"){}


 public override void Create(Scene scene)
 {
  base.Create(scene);
  sprite.TextureRect = new IntRect(32, 0, 18, 18);
 }

 public override void Update(Scene scene, float deltaTime)
 { }
}