using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBallGame
{
    public class Explosion : DrawableGameComponent
    {
        private Game1 parent;
        private Texture2D tex;
        private int rows;
        private int cols;
        private int width;
        private int height;
        private int currentX;
        private int currentY;
        private Vector2 pos;
        public Explosion(Game game, 
            Texture2D tex, int rows, int cols) : base(game)
        {
            parent = (Game1)game;
            this.tex = tex;
            this.rows = rows;
            this.cols = cols;
            width = tex.Width / cols;
            height = tex.Height / rows;
            currentX = 0;
            currentY = 0;
            pos = new Vector2(0, 0);
            this.Enabled = false;
            this.Visible = false;
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle srcRect = new Rectangle(currentX, currentY, width, height);
            parent.SpriteBatch.Begin();
            parent.SpriteBatch.Draw(tex, pos, srcRect, Color.White);
            parent.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            currentX += width;
            if (currentX >= tex.Width)
            {
                currentX = 0;
                currentY += height;
                if (currentY >= tex.Height)
                {
                    this.Enabled = false;
                    this.Visible = false;
                }
            }
            base.Update(gameTime);
        }
        public void StartAnimation(Vector2 pos)
        {
            currentX = 0;
            currentY = 0;
            this.pos = pos;
            this.Enabled = true;
            this.Visible = true;
        }
    }
}
