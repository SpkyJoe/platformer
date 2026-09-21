using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace platformer;

public class Hero : Entity
{
    private bool faceRight = false;

    
    public Hero() : base("characters")
    {
        sprite.TextureRect = new IntRect(0, 0, 24, 24);
        sprite.Origin = new Vector2f(12, 12);
    }
    
    
}