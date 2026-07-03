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
        public Ally(int health, int strength, Vector2 location, Texture2D sprite, Texture2D buttonImage)
            : base(health, strength, location, sprite)
        {
            buttons = new Button[4];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    buttons[i * 2 + j] = new Button(buttonImage, new Rectangle(i * 201 + 350, 360 + 51 * j,
                        200, 50), new Rectangle(0, 0, buttonImage.Width, buttonImage.Height));
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
                GenericAttack(target);
                return true;
            }
            else
            {
                return false;
            }
        }

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
