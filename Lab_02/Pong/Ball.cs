using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class Ball
    {
        private Texture2D ballTexture2D;
        private Texture2D ExplosionTexture2D;

        public Rectangle body;
        private Rectangle BallCurrentFrame;
        private Rectangle ExplosionCurrentFrame;

        public Rectangle CollisonBody => new Rectangle(body.X + BallBodyOffset, body.Y + BallBodyOffset,
              body.Width - 2 * BallBodyOffset, body.Height - 2 * BallBodyOffset);

        private Vector2 directionVector2;

        private int speed;

        private int BallAnimationSpeed, BallAnimationTimeElapsed;

        private int ExplosionAnimationSpeed, ExplosionAnimationTimeElapsed;

        private int BallFrameXOffset, BallFrameYOffset;

        private int ExplosionFrameXOffset, ExplosionFrameYOffset;

        private int BallBodyOffset;

        private int GameStartX, GameStartY;

        private Vector2 explosionVector2;

        public bool isDestroyed;

        public Ball(Texture2D ballTexture2D, Texture2D explosionTexture2D, int x, int y)
        {
            this.ballTexture2D = ballTexture2D;
            BallFrameXOffset = ballTexture2D.Width / 16;
            BallFrameYOffset = ballTexture2D.Height / 5;

            GameStartX = x;
            GameStartY = y;
            speed = 5;
            isDestroyed = false;

            body = new Rectangle(GameStartX, GameStartY, BallFrameXOffset, BallFrameYOffset);
            BallCurrentFrame = new Rectangle(0, 0, ballTexture2D.Width / 16, ballTexture2D.Height / 5);
            BallAnimationSpeed = 16;
            BallAnimationTimeElapsed = BallAnimationSpeed;
            directionVector2 = new Vector2(speed, speed);

            BallBodyOffset = 10;

            ExplosionTexture2D = explosionTexture2D;
            ExplosionCurrentFrame = new Rectangle(0, 0, explosionTexture2D.Width / 5, explosionTexture2D.Height / 5);
            ExplosionAnimationSpeed = 16;
            ExplosionAnimationTimeElapsed = ExplosionAnimationSpeed;
            ExplosionFrameXOffset = explosionTexture2D.Width / 5;
            ExplosionFrameYOffset = explosionTexture2D.Height / 5;
        }


        public void Move()
        {
            body.X += (int)directionVector2.X;
            body.Y += (int)directionVector2.Y;
        }

        void Accelerate()
        {
            if (directionVector2.X >= 0)
                directionVector2.X += speed * 0.3f;
            else
            {
                directionVector2.X -= speed * 0.3f;
            }

            /*            if (directionVector2.Y >= 0)
                            directionVector2.Y += speed * 0.1f;
                        else
                        {
                            directionVector2.Y -= speed * 0.1f;
                        }*/

        }

        public void BounceFromPaddle(int p)
        {
            directionVector2.X *= -1;
            if (p == 1)
            {
                if (directionVector2.Y > 0)
                {
                    directionVector2.Y *= -1;
                }

                if (directionVector2.Y == 0.0f)
                    directionVector2.Y = -speed;
                Accelerate();
                return;

            }

            if (p == 3)
            {
                directionVector2.Y = 0.00f;
                Accelerate();

                return;
            }

            if (p == 5)
            {
                if (directionVector2.Y < 0)
                {
                    directionVector2.Y *= -1;
                }
                if (directionVector2.Y == 0.0f)
                    directionVector2.Y = speed;
                Accelerate();
                return;
            }
            Accelerate();
            return;
        }

        public void BounceFromWall(int bound)
        {
            if (CollisonBody.Y <= 0 || CollisonBody.Y >= bound - CollisonBody.Height)
                directionVector2.Y *= -1;
        }

        /*public bool CheckCollisonWithPaddle(Rectangle paddle)
        {
            if (CollisonBody.Intersects(paddle))
            {
                BounceFromPaddle();
                Accelerate();
                return true;
            }
            else
            {
                return false;
            }
        }*/

        public void DestroyBall(GameTime time, int bound)
        {
            isDestroyed = true;
            if (body.X > bound)
                explosionVector2 = new Vector2(bound - body.X, body.Y);
            else
            if (body.X < 0)
                explosionVector2 = new Vector2(0, body.Y);
            else
                explosionVector2 = new Vector2(body.X, body.Y);

            ResetBall();
        }

        public int CheckCollisionWithWall(int leftBound, int rightBound)
        {
            if (CollisonBody.X <= leftBound)
            {
                return 'l';
            }
            else if (CollisonBody.X + CollisonBody.Width >= rightBound)
            {
                return 'r';
            }

            return 0;
        }


        public void AnimateBall(GameTime time)
        {
            BallAnimationTimeElapsed -= time.ElapsedGameTime.Milliseconds;
            if (BallAnimationTimeElapsed <= 0)
            {
                NextFrameBallAnimation();
                BallAnimationTimeElapsed = BallAnimationSpeed;
            }
        }

        public void AnimateExplosion(GameTime time)
        {
            ExplosionAnimationTimeElapsed -= time.ElapsedGameTime.Milliseconds;
            if (ExplosionAnimationTimeElapsed <= 0)
            {
                NextFrameExplosionAnimation();
                ExplosionAnimationTimeElapsed = ExplosionAnimationSpeed;
            }
        }
        public void NextFrameBallAnimation()
        {
            if (BallCurrentFrame.X + BallFrameXOffset >= ballTexture2D.Width)
            {
                BallCurrentFrame.X = 0;

                if (BallCurrentFrame.Y + BallFrameYOffset >= ballTexture2D.Height)
                {
                    BallCurrentFrame.Y = 0;
                }
                else
                {
                    BallCurrentFrame.Y += BallFrameYOffset;
                }
            }
            else
            {
                BallCurrentFrame.X += BallFrameXOffset;
            }

            if (BallCurrentFrame.X == 7 * BallFrameXOffset && BallCurrentFrame.Y == 4 * BallFrameYOffset)
            {
                ResetFramesCoords();
            }
        }

        public void NextFrameExplosionAnimation()
        {
            if (ExplosionCurrentFrame.X + ExplosionFrameXOffset >= ExplosionTexture2D.Width)
            {
                ExplosionCurrentFrame.X = 0;

                if (ExplosionCurrentFrame.Y + ExplosionFrameYOffset >= ExplosionTexture2D.Height)
                {
                    ExplosionCurrentFrame.Y = 0;
                }
                else
                {
                    ExplosionCurrentFrame.Y += ExplosionFrameYOffset;
                }
            }
            else
            {
                ExplosionCurrentFrame.X += ExplosionFrameXOffset;
            }
            if (ExplosionCurrentFrame.X == 4 * ExplosionFrameXOffset && ExplosionCurrentFrame.Y == 4 * ExplosionFrameYOffset)
            {
                isDestroyed = false;
            }

        }




        void ResetBall()
        {
            body.X = GameStartX;
            body.Y = GameStartY;
            directionVector2 = new Vector2(speed, speed);
        }

        void ResetFramesCoords()
        {
            BallCurrentFrame.X = 0;
            BallCurrentFrame.Y = 0;
        }

        public void DrawBall(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballTexture2D, new Vector2(body.X, body.Y), BallCurrentFrame, Color.White);
        }

        public void DrawExplosion(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ExplosionTexture2D, explosionVector2, ExplosionCurrentFrame, Color.White);
        }
    }
}
