using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class Paddle
    {
        private Texture2D texture2D;
        /*private Vector2 position;*/
        public Rectangle body;

        private int speed;
        private bool isHitted;
        private int hitTime;
        private int hitTimeElapsed;

        private char side;

        private Vector2 startPosition;

        private Rectangle topOuterFragment, topInnerFragment, innerFragment, botInnerFragment, botOuterFragment;
        private int fragmentXOffset;


        public Paddle(Texture2D texture2D, Vector2 position, char side)
        {
            this.texture2D = texture2D;
            startPosition = position;
            body = new Rectangle((int)startPosition.X, (int)startPosition.Y, texture2D.Width, texture2D.Height);
            this.side = side;

            isHitted = false;
            speed = 4;
            hitTime = 300;
            hitTimeElapsed = hitTime;
            fragmentXOffset = 400;

        }

        public void SetStartPosition()
        {
            body.X = (int)startPosition.X;
            body.Y = (int)startPosition.Y;
        }

        private void SetFragments()
        {
            if (side == 'l')
            {
                topOuterFragment = new Rectangle(body.X - fragmentXOffset, body.Y, body.Width + fragmentXOffset, body.Height / 5);
                topInnerFragment = new Rectangle(body.X - fragmentXOffset, body.Y + 1 * body.Height / 5, body.Width + fragmentXOffset, body.Height / 5);
                innerFragment = new Rectangle(body.X - fragmentXOffset, body.Y + 2 * body.Height / 5, body.Width + fragmentXOffset, body.Height / 5);
                botInnerFragment = new Rectangle(body.X - fragmentXOffset, body.Y + 3 * body.Height / 5, body.Width + fragmentXOffset, body.Height / 5);
                botOuterFragment = new Rectangle(body.X - fragmentXOffset, body.Y + 4 * body.Height / 5, body.Width + fragmentXOffset, body.Height / 5);
            }
            if (side == 'r')
            {
                topOuterFragment = new Rectangle(body.X, body.Y, body.Width + fragmentXOffset, body.Height / 5);
                topInnerFragment = new Rectangle(body.X, body.Y + 1 * body.Height / 5, body.Width + fragmentXOffset, body.Height / 5);
                innerFragment = new Rectangle(body.X, body.Y + 2 * body.Height / 5, body.Width + fragmentXOffset, body.Height / 5);
                botInnerFragment = new Rectangle(body.X, body.Y + 3 * body.Height / 5, body.Width + fragmentXOffset, body.Height / 5);
                botOuterFragment = new Rectangle(body.X, body.Y + 4 * body.Height / 5, body.Width + fragmentXOffset, body.Height / 5);
            }
        }
        /*void UpdateBody()
        {
            body.X = (int)position.X;
            body.Y = (int)position.Y;
        }*/

        public bool CheckCollisionWithBall(Ball ball)
        {

            if (ball.CollisonBody.Intersects(topOuterFragment))
            {
                ball.BounceFromPaddle(1);
                return true;
            }
            if (ball.CollisonBody.Intersects(topInnerFragment))
            {
                ball.BounceFromPaddle(2);
                return true;
            }
            if (ball.CollisonBody.Intersects(innerFragment))
            {
                ball.BounceFromPaddle(3);
                return true;
            }
            if (ball.CollisonBody.Intersects(botInnerFragment))
            {
                ball.BounceFromPaddle(4);
                return true;
            }
            if (ball.CollisonBody.Intersects(botOuterFragment))
            {
                ball.BounceFromPaddle(5);
                return true;
            }

            return false;
        }

        public void MoveUP()
        {
            if (body.Y > 0)
            {
                body.Y -= speed;
            }
            SetFragments();
        }

        public void MoveDown(int bound)
        {
            if (body.Y + body.Height < bound)
            {
                body.Y += speed;
            }
            SetFragments();
        }

        /// <summary>
        /// Wywoływać razem z metodą Hitted()
        /// </summary>
        public void Hit()
        {
            isHitted = true;
        }

        public void CheckIfHitted(GameTime time)
        {
            if (isHitted)
            {
                Highlight(time);
            }
        }

        private void Highlight(GameTime time)
        {
            hitTimeElapsed -= time.ElapsedGameTime.Milliseconds;
            if (hitTimeElapsed < 0)
            {
                isHitted = false;
                hitTimeElapsed = hitTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isHitted)
                spriteBatch.Draw(texture2D, new Vector2(body.X, body.Y), Color.White);
            else
            {
                spriteBatch.Draw(texture2D, new Vector2(body.X, body.Y), Color.Bisque);
            }
        }

        public void DrawFragments(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, topOuterFragment, Color.Red);
            spriteBatch.Draw(texture2D, topInnerFragment, Color.Blue);
            spriteBatch.Draw(texture2D, innerFragment, Color.Black);
            spriteBatch.Draw(texture2D, botInnerFragment, Color.Yellow);
            spriteBatch.Draw(texture2D, botOuterFragment, Color.Green);

        }
    }
}
