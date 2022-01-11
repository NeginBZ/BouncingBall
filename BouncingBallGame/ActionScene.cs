using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace BouncingBallGame
{
    public class ActionScene : GameScene
    {
        private Game1 parent;

        private SoundEffect hitSound;
        private SoundEffect missSound;

        private CollisionDetection cd;
        private Explosion explosion;
        private Bat bat;
        private Ball ball;

        private bool gameOver = false;
        Texture2D brickImage;

        private int[,,] boards = new int[2, 6, 12]
        {
            {
                {  0, -1,  1, -1,  0, -1,  2, -1,  1, -1,  0, -1 },
                {  -1, 1, -1,  0, -1,  2, -1,  0, -1,  1, -1,  0 },
                {  1, -1,  0, -1,  2, -1,  3, -1,  4, -1,  1, -1 },
                {  -1, 0, -1,  2, -1,  0, -1,  0, -1,  4, -1,  1 },
                {  0, -1,  2, -1,  3, -1,  4, -1,  0, -1,  0, -1 },
                {  -1, 2, -1,  5, -1,  5, -1,  4, -1,  2, -1,  0 },
            },
            {
                {  0, -1,  1, -1,  0, -1,  2, -1,  1, -1,  0, -1 },
                {  -1, 1, -1,  0, -1,  2, -1,  0, -1,  1, -1,  0 },
                {  1, -1,  0, -1,  2, -1,  3, -1,  4, -1,  1, -1 },
                {  -1, 0, -1,  2, -1,  0, -1,  0, -1,  4, -1,  1 },
                {  0, -1,  2, -1,  3, -1,  4, -1,  0, -1,  0, -1 },
                {  -1, 2, -1,  5, -1,  5, -1,  4, -1,  2, -1,  0 },
            }
        };

        public ActionScene(Game game) : base(game)
        {
            parent = (Game1)game;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void show()
        {
            if (gameOver)
            {
                gameOver = false;
                ball.Restart();
            }

            base.show();
        }

        public override void Update(GameTime gameTime)
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && ball.Enabled == false)
            //{
            //    ball.Restart();
            //}
            if (ball.Enabled == false)
            {
                gameOver = true;
                parent.Notify(this, "GameOver");
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameOver = false;
                parent.Notify(this, "Pause");
            }
            base.Update(gameTime);
        }

        private void DrawBricks(int bi)
        {
            int leftGap = 50;
            int topGap = 20;
            Vector2 pos = new Vector2(leftGap, topGap);
            for (int i = 0; i < boards.GetLength(1); i++)
            {
                for (int j = 0; j < boards.GetLength(2); j++)
                {
                    if (boards[bi, i, j] != -1)
                    {
                        Brick brick = new Brick(parent, boards[bi, i, j], brickImage, pos);
                        Components.Add(brick);
                    }
                    pos.X += Brick.W;
                }
                pos.X = leftGap;
                pos.Y += Brick.H;
            }
        }

        protected override void LoadContent()
        {
            hitSound = parent.Content.Load<SoundEffect>("Music/click");
            missSound = parent.Content.Load<SoundEffect>("Music/applause1");

            Texture2D batImage = parent.Content.Load<Texture2D>("Images/Bat");
            bat = new Bat(parent, batImage, parent.stage, 8);
            Components.Add(bat);

            Texture2D ballImage = parent.Content.Load<Texture2D>("Images/Ball");
            Vector2 speed = new Vector2(2f, -2f);
            ball = new Ball(parent, ballImage, parent.stage, speed,
                hitSound, missSound);
            Components.Add(ball);

            brickImage = parent.Content.Load<Texture2D>("Images/bricks");
            //Brick b = new Brick(parent, 3, brickImage, new Vector2(100, 100));
            //Components.Add(b);
            DrawBricks(0);

            Texture2D expImage = parent.Content.Load<Texture2D>("Images/explosion");
            explosion = new Explosion(parent, expImage, 5, 5);
            Components.Add(explosion);

            cd = new CollisionDetection(parent, bat, ball, explosion);
            Components.Add(cd);

            CollisionBrickBall cbb = new CollisionBrickBall(parent, Components, ball);
            Components.Add(cbb);
            base.LoadContent();
        }
    }
}
