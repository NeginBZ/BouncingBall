using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BouncingBallGame
{
    public class Bat : DrawableGameComponent
    {
        private Game1 parent;
        private Vector2 stage;
        private Texture2D batImage;
        private Vector2 pos;
        private int speed;
        public Bat(Game game, Texture2D image, Vector2 stage, int speed) : base(game)
        {
            parent = (Game1)game;
            batImage = image;
            this.stage = stage;
            pos = new Vector2(
                stage.X / 2 - batImage.Width / 2,
                stage.Y - batImage.Height);
            this.speed = speed;
        }

        public override void Draw(GameTime gameTime)
        {
            parent.SpriteBatch.Begin();
            parent.SpriteBatch.Draw(
                batImage,  // image to show
                pos,  // position
                Color.White);  // fill color
            parent.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right))
            {
                pos.X += speed;
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                pos.X -= speed;
            }
            pos.X = MathHelper.Clamp(pos.X, 0, stage.X - batImage.Width);
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public Rectangle GetBound()
        {
            return new Rectangle((int)pos.X, (int)pos.Y,
                batImage.Width, batImage.Height);
        }
    }
}
