using Platformer;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace platformer;
 
public class BreakablePlat : Platform
{

    public override bool Solid => true;
    public override bool Breakable => true;
    static private SoundBuffer sound = new SoundBuffer("assets/random.wav");
    static private Sound trasig = new Sound(sound);

    public BreakablePlat()
    {
        sprite.TextureRect = new IntRect(72, 90, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }

    public override void BreakCheck(Scene scene)
    {
        if (scene.FindByType<Hero>(out Hero H))
        {
            if (H.Position.Y - H.Bounds.Height / 2 <= Position.Y + Bounds.Height / 2
                && H.Position.X + 1 - H.Bounds.Width / 2 < Position.X + Bounds.Width / 2
                && H.Position.X - 1 + H.Bounds.Width / 2 > Position.X - Bounds.Width / 2
                && H.Position.Y + H.Bounds.Height / 2 > Position.Y - Bounds.Height / 2)
            {
                trasig.Play();
                Dead = true;
            }
            
        }
    }
    
    public override void Update(Scene scene, float deltatime)
    {
        
    }
}