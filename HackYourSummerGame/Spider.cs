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
    internal class Spider : Enemy
    {
        public Spider(int health, int strength, Vector2 location, Texture2D sprite, Texture2D healthBar, Texture2D healthContainer)
            : base(health, strength, location, sprite, healthBar, healthContainer)
        {
        }

        public override void MoveChoice(Character target)
        {
            if(moveTracker > 1)
            {
                moveTracker = 0;
            }

            if(moveTracker == 0)
            {
                GenericAttack(target);
            }
            else if(moveTracker == 1)
            {
                target.Health -= strength / 7 * 4;
                target.Health -= strength / 7 * 4;
            }

            moveTracker++;
        }
    }
}
