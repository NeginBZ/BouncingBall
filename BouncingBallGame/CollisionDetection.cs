using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BouncingBallGame
{
    public class CollisionDetection : GameComponent
    {
        private Game1 parent;
        private Bat bat;
        private Ball ball;
        private Explosion exp;
        public CollisionDetection(Game game, 
            Bat bat, Ball ball, Explosion exp) : base(game)
        {
            parent = (Game1)game;
            this.bat = bat;
            this.ball = ball;
            this.exp = exp;
        }

        public override void Update(GameTime gameTime)
        {
            if (ball.Enabled)
            {
                Rectangle ballRect = ball.GetBound();
                Rectangle batRect = bat.GetBound();
                if (batRect.Intersects(ballRect))
                {
                    ball.BounceBat();
                    //exp.StartAnimation(ballRect.Center.ToVector2());
                    exp.StartAnimation(new Vector2(ballRect.X, ballRect.Y));
                }               
            }
            base.Update(gameTime);
        }
    }
}
