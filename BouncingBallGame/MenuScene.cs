using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace BouncingBallGame
{
    public class MenuScene : GameScene
    {
        private Game1 parent;
        private List<string> menuItems;
        private string title;

        private SpriteFont regularFont;
        private SpriteFont hilightFont;
        private SpriteFont titleFont;

        private Vector2 pos;
        private int selectedItem;

        private KeyboardState oldState;

        public MenuScene(Game game, string title, List<string> menuItems) : base(game)
        {
            parent = (Game1)game;
            this.menuItems = menuItems;
            this.title = title;
        }
        protected override void LoadContent()
        {
            regularFont = parent.Content.Load<SpriteFont>("Fonts/regularFont");
            hilightFont = parent.Content.Load<SpriteFont>("Fonts/hilightFont");
            titleFont = parent.Content.Load<SpriteFont>("Fonts/titleFont");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);
            Vector2 tpos = pos;
            parent.SpriteBatch.Begin();
            parent.SpriteBatch.DrawString(titleFont, title, tpos, Color.Red);
            tpos.Y += titleFont.LineSpacing + 10;
            tpos.X += 20;
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == selectedItem)
                {
                    parent.SpriteBatch.DrawString(hilightFont, menuItems[i], tpos, Color.Green);
                    tpos.Y += hilightFont.LineSpacing + 10;
                }
                else
                {
                    parent.SpriteBatch.DrawString(regularFont, menuItems[i],  tpos, Color.Blue);
                    tpos.Y += regularFont.LineSpacing + 10;
                }
            }

            parent.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            selectedItem = 2;
            pos = new Vector2(parent.stage.X / 3, parent.stage.Y / 3);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (oldState.IsKeyUp(Keys.Down) && ks.IsKeyDown(Keys.Down))
            {
                selectedItem = MathHelper.Clamp(selectedItem + 1, 0, menuItems.Count - 1);
            }
            if (oldState.IsKeyUp(Keys.Up) &&  ks.IsKeyDown(Keys.Up))
            {
                selectedItem = MathHelper.Clamp(selectedItem - 1, 0, menuItems.Count - 1);
            }
            if (oldState.IsKeyUp(Keys.Enter) && ks.IsKeyDown(Keys.Enter))
            {
                parent.Notify(this, menuItems[selectedItem]);
            }
            oldState = ks;
            base.Update(gameTime);
        }

    }
}
