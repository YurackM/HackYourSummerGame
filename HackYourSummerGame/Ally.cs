using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackYourSummerGame
{
    internal class Ally : Character
    {
        // Fields
        private Button[] buttons;
        private ContentManager contentManager;
        private bool currentTurn;
        private List<SingleTargetAttack> attacks;
        private int[] statIncreases;

        // Constructor
        public Ally(int health, int strength, int speed, Vector2 position, Texture2D sprite, Texture2D healthBar, Texture2D healthContainer, ContentManager contentManager)
            : base(health, strength, speed, position, sprite, healthBar, healthContainer)
        {
            this.contentManager = contentManager;
            Texture2D buttonImage = contentManager.Load<Texture2D>("AttackButton");

            buttons = new Button[4];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    buttons[i * 2 + j] = new Button(buttonImage, new Rectangle(i * 401 + 660, 750 + 101 * j,
                        400, 100), new Rectangle(0, 0, buttonImage.Width, buttonImage.Height));
                }
            }

            statIncreases = new int[3] { 5, 3, 3 };
        }

        //
        public bool GetPlayerChoice(Character target)
        {
            bool buttonClicked = false;

            if (buttons[0].Clicked())
            {
                GenericAttack(target);
                buttonClicked = true;
            }
            else if (buttons[1].Clicked())
            {
                GenericAttack(target);
                buttonClicked = true;
            }
            else if (buttons[2].Clicked())
            {
                GenericAttack(target);
                buttonClicked = true;
            }
            else if (buttons[3].Clicked())
            {
                DoubleSlam(target);
                buttonClicked = true;
            }

            if(DOT > 0 && buttonClicked)
            {
                health -= (int)(DOT * (0.05 * maxHealth));
                DOT--;
            }
            
            return buttonClicked;
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
                ongoingAnimation = true;
            }
            else
            {
                ongoingAnimation = false;
            }

            base.Update();
        }

        // Draw player
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        // Draw buttons
        public void DrawButtons(SpriteBatch sb)
        {
            foreach (Button button in buttons)
            {
                button.Draw(sb);
            }
        }

        public bool UpgradePlayer()
        {
            bool buttonClicked = false;

            if (buttons[0].Clicked())
            {
                strength += statIncreases[0];
                buttonClicked = true;
            }
            else if (buttons[1].Clicked())
            {
                strength += statIncreases[1];
                buttonClicked = true;
            }
            else if (buttons[2].Clicked())
            {
                strength += statIncreases[2];
                buttonClicked = true;
            }

            return buttonClicked;
        }

        public void UpgradePlayerChoice(SpriteBatch sb)
        {
            base.Draw(sb);
            for(int i = 0; i < 3; i++)
            {
                buttons[i].Draw(sb);
            }
        }
    }
}
