using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using Platformer;
using System.IO;

namespace platformer;

public class Platform : Entity
{
    public override bool Solid => true;
    
    public Platform() : base("tileset") // var i tilesetet "platform" finns och vart mitten på spriten finns mellan de pixlarna
    {
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }
}