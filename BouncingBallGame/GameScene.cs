using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBallGame
{
    public class GameScene : DrawableGameComponent
    {
        private Game1 parent;
        public List<GameComponent> Components { get; set; }
        public GameScene(Game game) : base(game)
        {
            parent = (Game1)game;
            Components = new List<GameComponent>();
            hide();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        private void SetState(bool state)
        {
            this.Enabled = state;
            this.Visible = state;
            foreach (GameComponent item in Components)
            {
                item.Enabled = state;
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    comp.Visible = state;
                }
            }
        }
        public virtual void hide()
        {
            SetState(false);
        }
        public virtual void show()
        {
            SetState(true);
        }
    }
}
