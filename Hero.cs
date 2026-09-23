using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using Platformer;
using System.IO;
using System.Reflection.Metadata.Ecma335;

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
    private bool outOfBounds;
    private bool isMoving;
    private Clock animationClock = new Clock();
    private float animationTimer;
    private bool firstFrame;
    

    
    public Hero() : base("characters")
    {
        sprite.TextureRect = new IntRect(0, 0, 24, 24);
        sprite.Origin = new Vector2f(12, 12);
        sprite.Position = new Vector2f();
        outOfBounds = false;
    }

    public override FloatRect Bounds
    {
        get
        {
            var bounds = base.Bounds;
            bounds.Left += 3;
            bounds.Width -= 6;
            bounds.Top += 3;
            bounds.Height -= 3;
            return bounds;
        }
    }


    public override void Update(Scene scene, float deltaTime) //Keyboard Input Left/Right/Up(Jump)
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            scene.TryMove(this, new Vector2f(-100 * deltaTime, 0));
            faceRight = false;
            isMoving = true;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
             scene.TryMove(this, new Vector2f(100 * deltaTime, 0));
            faceRight = true;
            isMoving = true;
            
        }

        if (!Keyboard.IsKeyPressed(Keyboard.Key.Right)&&!Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            isMoving = false;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
        {
            if (isGrounded && !isUpPressed) //Triggar Jump en gång trots att man trycker fler ggr på Up.
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
        if (VerticalSpeed > 500.0f) VerticalSpeed = 500.0f; //Limit på hur snabb VerticalSpeed kan bli
        isGrounded = false;
        Vector2f velocity = new Vector2f(0, VerticalSpeed * deltaTime);
        if (scene.TryMove(this, velocity))
        {
            if (VerticalSpeed > 0.0f) //Känner av om momentum vertikalt = 0 == är på marken
            {
                isGrounded = true;
                VerticalSpeed = 0.0f;
            }
            else
            {
                VerticalSpeed = -0.5f * VerticalSpeed;
            }
        }

        switch (sprite.Position.Y > 18.0f && sprite.Position.Y < 288.0f && sprite.Position.X > 18.0f && sprite.Position.X < 378.0f)
        {
            case true:
                outOfBounds = false;
                break;
            case false:
                outOfBounds = true;
                break;
        }

        if (outOfBounds)
        {
            scene.Reload();
            Dead=true;
        }

        
       
            
    }
    
    public override void Render(RenderTarget target)
    {
        if (isMoving) // Walkanimation, växlar mellan 2 states beroende hur lång tid som har gått.
        {
            if (!firstFrame && animationClock.ElapsedTime.AsSeconds() > animationTimer + 0.5f)
            { 
                animationClock.Restart();
                animationTimer = animationClock.ElapsedTime.AsSeconds();
                sprite.TextureRect = new IntRect(24, 0, 24, 24);
                firstFrame = true;
            }

            if (animationClock.ElapsedTime.AsSeconds() > animationTimer + 0.25f)
            {
                sprite.TextureRect = new IntRect(0, 0, 24, 24);
                firstFrame = false;
            }
            
        }
        
        sprite.Scale = new Vector2f(faceRight ? -1 : 1, 1); // <Condition(faceright)> ? <case true(-1)>:<case false(1)>
        base.Render(target);
    }
}