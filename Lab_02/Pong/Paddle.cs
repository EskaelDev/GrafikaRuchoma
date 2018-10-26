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

        private Rectangle topOuterFragment, topInnerFragment, innerFragment, botInnerFragment, botOuterFragment;



        public Paddle(Texture2D texture2D, Vector2 position)
        {
            this.texture2D = texture2D;
            body = new Rectangle((int)position.X, (int)position.Y, texture2D.Width, texture2D.Height);

            isHitted = false;
            speed = 4;
            hitTime = 300;
            hitTimeElapsed = hitTime;

        }


        private void SetFragments()
        {
            topOuterFragment = new Rectangle(body.X, body.Y, body.Width, body.Height / 5);
            topInnerFragment = new Rectangle(body.X, body.Y + 1 * body.Height / 5, body.Width, body.Height / 5);
            innerFragment = new Rectangle(body.X, body.Y + 2 * body.Height / 5, body.Width, body.Height / 5);
            botInnerFragment = new Rectangle(body.X, body.Y + 3 * body.Height / 5, body.Width, body.Height / 5);
            botOuterFragment = new Rectangle(body.X, body.Y + 4 * body.Height / 5, body.Width, body.Height / 5);
        }
        /*void UpdateBody()
        {
            body.X = (int)position.X;
            body.Y = (int)position.Y;
        }*/

        public bool CheckCollisionWithBall( Ball ball)
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
    }
}
