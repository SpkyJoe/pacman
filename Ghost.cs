using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Ghost : Entity
{
 public Ghost() : base("pacman"){}


 public override void Create(Scene scene)
 {
  base.Create(scene);
  sprite.TextureRect = new IntRect(36, 0, 18, 18);
  sprite.Origin = new Vector2f(9, 9);
 }

 public override void Update(Scene scene, float deltaTime)
 { }
}