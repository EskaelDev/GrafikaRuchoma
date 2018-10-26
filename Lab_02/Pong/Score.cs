using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class Score
    {
        private SpriteFont font;
        private int score;
        private Vector2 position;
        private Color color;

        public Score(SpriteFont font,  Vector2 position)
        {
            this.font = font;
            this.score = 0;
            this.position = position;
            this.color = Color.GreenYellow;
        }

        public void AddPoint()
        {
            score++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, score.ToString("00"), position, color);
        }
    }
}
