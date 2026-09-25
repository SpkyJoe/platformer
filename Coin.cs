using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using Platformer;
using System.IO;
using SFML.Audio;

namespace platformer;

public class Coin : Entity
{
    public Coin() : base("tileset")
    {
        sprite.TextureRect = new IntRect(198, 128, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }

    public override void Update(Scene scene, float DeltaTime)
    {
        if (scene.FindByType<Hero>(out Hero foundHero))
        {
            if (Collision.RectangleRectangle(Bounds, foundHero.Bounds, out _))
            {
                Dead = true;
                foundHero.coinsCollected++;
            }
        }
    }
}