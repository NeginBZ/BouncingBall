using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace BouncingBallGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GraphicsDeviceManager Graphics { get => _graphics; }
        public SpriteBatch SpriteBatch { get => _spriteBatch; }
        public Vector2 stage;

        private List<string> menuItems = new List<string> { 
            "Start Game",
            "Help",
            "About us",
            "Credits",
            "Exit"
        };

        private List<string> GameHelp = new List<string> {
            "* Please use left and right keyboard buttons to move board",
            "* If you want to pause the game or come back to menu please use Escape button",
            "* To restart the game you can just Enter",
        };
        private Help help;
        private List<string> AboutUs = new List<string> {
            "Negin Beheshti Zavareh",
            
        };
        private About about;
        private MenuScene menuScene;
        private ActionScene actionScene;
        private GameScene currentScene;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            menuScene = new MenuScene(this, "Bouncing Ball", menuItems);
            Components.Add(menuScene);

            help = new Help(this, "Bouncing Ball", GameHelp);
            Components.Add(help);

            about = new About(this, "Author Name", AboutUs);
            Components.Add(about);
            
            actionScene = new ActionScene(this);
            Components.Add(actionScene);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            currentScene = menuScene;
            currentScene.show();
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void Notify(GameScene sender, string action)
        {
            currentScene.hide();
            if (sender is MenuScene)
            {
                switch (action) {
                    case "Start Game":
                        currentScene = actionScene;
                        break;
                    case "Help":
                        currentScene = help;
                        break;
                    case "About us":
                        currentScene = about;
                        break;
                    case "Credits":
                        break;
                    case "Exit":
                        Exit();
                        break;
                }
            }
            else if (sender is ActionScene)
            {
                currentScene = menuScene;
            }
            else if (sender is About)
            {
                currentScene = menuScene;
            }
            else if (sender is Help)
            {
                currentScene = menuScene;
            }
            currentScene.show();
        }
    }
}
