using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using Platformer;
using System.IO;

namespace platformer;

public class Door : Entity
{
   public string NextRoom;
   public bool Unlocked;
   public Door() : base("tileset")
   {
      sprite.TextureRect = new IntRect(180, 103, 18, 23);
      sprite.Origin = new Vector2f(9, 11.5f);
      Unlocked = false;
   }

   public override void Update(Scene scene, float deltaTime)
   {
      if (scene.FindByType<Hero>(out Hero hero))
      {
         if (Collision.RectangleRectangle(Bounds, hero.Bounds, out _))
         {
            if(Unlocked == true)
            {
               scene.Load(NextRoom);
               Unlocked = false;
            }
         }
      }
      switch (Unlocked)
      {
         case true:
            sprite.Color = Color.Black;
            break;
         case false:
            break;
      }
      
   }

   public void Render(RenderTarget target)
   {
      target.Draw(sprite);
   }
}