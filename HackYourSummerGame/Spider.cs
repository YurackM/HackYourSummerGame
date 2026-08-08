using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackYourSummerGame
{
    internal class Spider : Enemy, IEnemy
    {
        public Spider(int health, int strength, int speed, Vector2 location, Texture2D sprite, Texture2D healthBar, Texture2D healthContainer)
            : base(health, strength, speed, location, sprite, healthBar, healthContainer)
        {
        }

        public void GetNextMove(Character target)
        {
            if(moveTracker > 2)
            {
                moveTracker = 0;
            }

            if(moveTracker == 0)
            {
                GenericAttack(target);
            }
            else if(moveTracker == 1)
            {
                DoubleBite(target);
            }
            else if(moveTracker == 2)
            {
                WebUp(target);
            }

            moveTracker++;
        }

        //
        public void DoubleBite(Character target)
        {
            target.Health -= strength / 7 * 5;
            attackOffset = new Vector2(-30, 30);
            target.Health -= strength / 7 * 4;
        }

        public void WebUp(Character target)
        {
            target.TempSpeed -= strength / 5;
            attackOffset = new Vector2(-10, 10);
        }
    }
}
