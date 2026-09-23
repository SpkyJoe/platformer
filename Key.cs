using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using Platformer;
using System.IO;

namespace platformer;

public class Key : Entity
{
    
    public Key() : base("tileset")
    {
        sprite.TextureRect = new IntRect(126, 18, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }

    public override void Update(Scene scene, float DeltaTime)
    {
        if (scene.FindByType<Hero>(out Hero foundHero))
        {
            if (Collision.RectangleRectangle(Bounds, foundHero.Bounds, out _))
            {
                Dead = true;   
                scene.FindByType<Door>(out Door foundDoor);
                do
                {
                    if (foundDoor != null)
                    {
                        foundDoor.Unlocked = true;
                    }
                } while (!foundDoor.Unlocked);
            }
        }
    }
}