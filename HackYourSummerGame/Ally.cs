using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackYourSummerGame
{
    internal class Ally : Character
    {
        // Fields
        private Button[] buttons;

        // Constructor
        public Ally(int health, int strength, int speed, Vector2 location, Texture2D sprite, Texture2D buttonImage, Texture2D healthBar, Texture2D healthContainer)
            : base(health, strength, speed, location, sprite, healthBar, healthContainer)
        {
            buttons = new Button[4];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    buttons[i * 2 + j] = new Button(buttonImage, new Rectangle(i * 401 + 660, 750 + 101 * j,
                        400, 100), new Rectangle(0, 0, buttonImage.Width, buttonImage.Height));
                }
            }
        }

        //
        public bool GetPlayerChoice(Character target)
        {
            if (buttons[0].Clicked())
            {
                GenericAttack(target);
                return true;
            }
            else if (buttons[1].Clicked())
            {
                GenericAttack(target);
                return true;
            }
            else if (buttons[2].Clicked())
            {
                GenericAttack(target);
                return true;
            }
            else if (buttons[3].Clicked())
            {
                DoubleSlam(target);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Basic attack move
        public override void GenericAttack(Character target)
        {
            base.GenericAttack(target);
            attackOffset = new Vector2(20, -20);
        }

        public void DoubleSlam(Character target)
        {
            target.Health -= strength;
            target.Health -= strength;
            attackOffset = new Vector2(20, -20);
        }

        // Update player info
        public override void Update()
        {
            if (attackOffset.X > 0)
            {
                attackOffset -= new Vector2(1, -1);
            }
        }

        // Draw player
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            foreach(Button button in buttons)
            {
                button.Draw(sb);
            }
        }
    }
}
