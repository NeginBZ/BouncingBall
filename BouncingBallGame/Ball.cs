using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace BouncingBallGame
{
    public class Ball : DrawableGameComponent
    {
        private Game1 parent;
        private Vector2 stage;
        private Texture2D ballImage;
        private Vector2 pos;
        private Vector2 speed;
        private Vector2 initialSpeed;
        private SoundEffect hitSound;
        private SoundEffect missSound;

        public Ball(Game game, 
            Texture2D image, 
            Vector2 stage, 
            Vector2 speed,
            SoundEffect hitSound,
            SoundEffect missSound) : base(game)
        {
            parent = (Game1)game;
            ballImage = image;
            this.stage = stage;
            this.initialSpeed = this.speed = speed;
            pos = new Vector2(
                stage.X / 2 - ballImage.Width / 2,
                stage.Y / 2 - ballImage.Height / 2);
            this.hitSound = hitSound;
            this.missSound = missSound;
        }

        public override void Draw(GameTime gameTime)
        {
            parent.SpriteBatch.Begin();
            parent.SpriteBatch.Draw(
                ballImage,  // image to show
                pos,  // position
                Color.White);  // fill color
            parent.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        private void BounceRight()
        {
            speed.X = -Math.Abs(speed.X);
            hitSound.Play();
        }
        private void BounceLeft()
        {
            speed.X = Math.Abs(speed.X);
            hitSound.Play();
        }
        private void BounceTop()
        {
            speed.Y = Math.Abs(speed.Y);
            hitSound.Play();
        }
        private void BounceBottom()
        {
            speed.Y = -Math.Abs(speed.Y);
            hitSound.Play();
        }
        public void BounceBat()
        {
            BounceBottom();
        }
        public override void Update(GameTime gameTime)
        {
            pos += speed;  // pos.X += speed.X;  pos.Y += speed.Y;
            // right wall
            if (pos.X + ballImage.Width >= stage.X)
            {
                BounceRight();
            }
            // left wall
            if (pos.X <= 0)
            {
                BounceLeft();
            }
            // top wall
            if (pos.Y <= 0)
            {
                BounceTop();
            }
            // bottom wall
            if (pos.Y + ballImage.Height >= stage.Y)
            {
                missSound.Play();
                this.Enabled = false;
                this.Visible = false;
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public Rectangle GetBound()
        {
            return new Rectangle( (int)pos.X, (int)pos.Y,
                ballImage.Width, ballImage.Height);
        }

        public void Restart()
        {
            pos = new Vector2(
                stage.X / 2 - ballImage.Width / 2,
                stage.Y / 2 - ballImage.Height / 2);
            speed = initialSpeed;
            this.Enabled = true;
            this.Visible = true;
        }
        public void BounceBrick(Brick b)
        {
            float cx = pos.X + ballImage.Width / 2;
            float cy = pos.Y + ballImage.Height / 2;
            if (cy > b.Bottom)
            {
                BounceTop();
            }
            else if (cy < b.Top)
            {
                BounceBottom();
            }
            else if (cx < b.Left)
            {
                BounceRight();
            } 
            else if (cx > b.Right)
            {
                BounceLeft();
            }
        }
    }
}
