using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace BouncingBallGame
{
    public class CollisionBrickBall : GameComponent
    {
        private Game1 parent;
        private List<GameComponent> comp;
        private Ball ball;
        public CollisionBrickBall(Game game, List<GameComponent> comp, Ball ball) : base(game)
        {
            parent = (Game1)game;
            this.comp = comp;
            this.ball = ball;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle ballRect = ball.GetBound();
            foreach (GameComponent item in comp)
            {
                if (item is Brick)
                {
                    Brick b = (Brick)item;
                    if (b.Visible && ballRect.Intersects(b.GetBound()))
                    {
                        ball.BounceBrick(b);
                        b.Hit();
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
