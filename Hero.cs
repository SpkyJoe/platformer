using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace platformer;

public class Hero : Entity
{
    private bool faceRight = false;
    public const float WalkSpeed = 100.0f;
    public const float JumpForce = 250.0f;
    public const float GravityForce = 400.0f;
    private float VerticalSpeed;
    private bool isGrounded;
    private bool isUpPressed;

    
    public Hero() : base("characters")
    {
        sprite.TextureRect = new IntRect(0, 0, 24, 24);
        sprite.Origin = new Vector2f(12, 12);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            scene.TryMove(this, new Vector2f(-100 * deltaTime, 0));
            faceRight = false;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
             scene.TryMove(this, new Vector2f(100 * deltaTime, 0));
            faceRight = true;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
        {
            if (isGrounded && !isUpPressed)
            {
                VerticalSpeed -= JumpForce;
                isUpPressed = true;
            }
        }
        else
        {
            isUpPressed = false;
        }
        VerticalSpeed += GravityForce * deltaTime;
        if (VerticalSpeed > 500.0f) VerticalSpeed = 500.0f;
        isGrounded = false;
        Vector2f velocity = new Vector2f(0, VerticalSpeed * deltaTime);
        if (scene.TryMove(this, velocity))
        {
            if (VerticalSpeed > 0.0f)
            {
                isGrounded = true;
            }
            VerticalSpeed = 0.0f;
        }
    }
    
    public override void Render(RenderTarget target)
    {
        sprite.Scale = new Vector2f(faceRight ? -1 : 1, 1);
        base.Render(target);
    }
}