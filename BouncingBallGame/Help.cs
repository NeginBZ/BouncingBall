using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace BouncingBallGame
{
    class Help : GameScene
    {
        private Game1 parent;
        private List<string> help;

        private SpriteFont regularFont;
        private SpriteFont hilightFont;
        private string title;

        private Vector2 pos;
        private int selectedItem;

        private KeyboardState oldState;

        public Help(Game1 game, string title, List<string> Help) : base(game)
        {
            parent = (Game1)game;
            this.help = Help;
            this.title = title;
        }
        protected override void LoadContent()
        {
            regularFont = parent.Content.Load<SpriteFont>("Fonts/regularFont");
            hilightFont = parent.Content.Load<SpriteFont>("Fonts/hilightFont");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);
            Vector2 tpos = pos;
            parent.SpriteBatch.Begin();
            parent.SpriteBatch.DrawString(hilightFont, title, tpos, Color.Green);
            tpos.Y += hilightFont.LineSpacing + 10;
            tpos.X += 2;
            for (int i = 0; i < help.Count; i++)
            {
                parent.SpriteBatch.DrawString(regularFont, help[i], tpos, Color.Blue);
                tpos.Y += hilightFont.LineSpacing + 10;
            }

            parent.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            //selectedItem = 2;
            //pos = new Vector2(parent.stage.X / 3, parent.stage.Y / 3);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                parent.Notify(this, "Pause");
            }
            base.Update(gameTime);
        }
    }
}
