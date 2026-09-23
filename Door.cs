using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Door : Entity
{
   public string NextRoom;
<<<<<<< Updated upstream
   public bool Unlocked;
=======
   
>>>>>>> Stashed changes
   public Door() : base("tileset")
   {
      sprite.TextureRect = new IntRect(180, 103, 18, 23);
      sprite.Origin = new Vector2f(9, 11.5f);
      
   }

   public void Update(Scene scene, float deltaTime)
   {
      
   }

   public void Render(RenderTarget target)
   {
      target.Draw(sprite);
   }
}