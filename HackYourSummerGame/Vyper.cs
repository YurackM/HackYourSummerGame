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
    internal class Vyper : Enemy, IEnemy
    {
        public Vyper (int health, int strength, int speed, Vector2 position, Texture2D sprite, Texture2D healthBar, Texture2D healthContainer)
            : base(health, strength, speed, position, sprite, healthBar, healthContainer)
        {
        }

        // Get next move
        public void GetNextMove(Character target)
        {
            if (moveTracker > 1)
            {
                moveTracker = 0;
            }

            if (moveTracker == 0)
            {
                GenericAttack(target);
            }
            else if (moveTracker == 1)
            {
                Poison(target);
            }

            moveTracker++;
            if (dot > 0)
            {
                health -= (int)(dot-- * maxHealth * 0.05);
            }
        }

        // poison attack
        public void Poison(Character target)
        {
            target.DOT += 2;
            attackOffset = new Vector2(-10, 10);
        }
    }
}
