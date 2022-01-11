using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace BouncingBallGame
{
    public class Brick : DrawableGameComponent
    {
        public const int W = 60;
        public const int H = 20;

        private Game1 parent;
        private int level;
        private Texture2D tex;
        private Vector2 pos;
        private Rectangle srcRect;
        public Brick(Game game, int level, Texture2D tex, Vector2 pos) : base(game)
        {
            parent = (Game1)game;
            this.level = level;
            this.tex = tex;
            this.pos = pos;
            this.srcRect = new Rectangle(level * W, 0, W, H);
        }

        public override void Draw(GameTime gameTime)
        {
            parent.SpriteBatch.Begin();
            parent.SpriteBatch.Draw(tex, pos, srcRect, Color.White);
            parent.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public Rectangle GetBound()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, W, H);
        }

        public float Top { get => pos.Y;  }
        public float Bottom { get => pos.Y + H;  }
        public float Left { get => pos.X;  }
        public float Right { get => pos.X + W;  }
        
        public void Hit()
        {
            if (level == 0)
            {
                Enabled = false;
                Visible = false;
            } 
            else
            {
                level--;
                srcRect.X -= W;
            }
        }
    }
}
