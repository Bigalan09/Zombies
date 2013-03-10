using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;
using Zombies.states.player;

namespace Zombies.strategy
{
    class PlayerKeyboardStrategy : Strategy
    {
        private Vector2 direction = new Vector2(-500, 500);
        private KeyboardState lastKeyboardState;
        private MouseState lastMouseState;

        public override void Act(GameTime gameTime)
        {
            direction.X += Mouse.GetState().X - 500;
            direction.Y += Mouse.GetState().Y - 500;

            ((PlayerState)Owner.CurrentState).Turn(direction);

            Vector2 walkDir = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                walkDir += direction;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                walkDir -= direction;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Vector2 t2 = new Vector2();
                t2.X = direction.Y;
                t2.Y = -direction.X;
                walkDir += t2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Vector2 t2 = new Vector2();
                t2.X = -direction.Y;
                t2.Y = direction.X;
                walkDir += t2;
            }

            if (walkDir.Length() != 0)
            {
                ((PlayerState)Owner.CurrentState).Walk(walkDir);
            }
            else
            {
                ((PlayerState)Owner.CurrentState).StopWalk();
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                ((PlayerState)Owner.CurrentState).Attack();
            else if (Mouse.GetState().RightButton == ButtonState.Pressed)
                ((PlayerState)Owner.CurrentState).SecondaryAttack();
            else
                ((PlayerState)Owner.CurrentState).StopFire();
            if (Keyboard.GetState().IsKeyUp(Keys.E) && lastKeyboardState.IsKeyDown(Keys.E))
                ((PlayerState)Owner.CurrentState).Use();

            /*
            //TEMPORARY
            if (Mouse.GetState().ScrollWheelValue > lastMouseState.ScrollWheelValue)
                this.Owner.EntityManager.Camera.zoom += 0.01f;
            else if(Mouse.GetState().ScrollWheelValue < lastMouseState.ScrollWheelValue)
                 this.Owner.EntityManager.Camera.zoom -= 0.01f;

            //TEMPORARY
            if (Keyboard.GetState().IsKeyDown(Keys.Add))
                this.Owner.EntityManager.Time += 0.01f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
                this.Owner.EntityManager.Time -= 0.01f;
            */

            Vector2 t = ((Player)Owner).Aim.Position - ((Player)Owner).Position;
            t.Normalize();
            lastMouseState = Mouse.GetState();
            lastKeyboardState = Keyboard.GetState();
            Mouse.SetPosition(500, 500);
        }
    }
}
