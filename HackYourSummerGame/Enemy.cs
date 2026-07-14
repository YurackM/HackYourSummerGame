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
    /// <summary>
    /// Class represeting an enemy, mostly a placeholder for now
    /// </summary>
    internal class Enemy : Character
    {
        public Enemy(int health, int strength, Vector2 location, Texture2D sprite, Texture2D healthBar, Texture2D healthContainer)
            : base(health, strength, location, sprite, healthBar, healthContainer)
        {

        }

        // Generic attack
        public override void GenericAttack(Character target)
        {
            base.GenericAttack(target);
            attackOffset = new Vector2(-20, 20);
        }

        // Update enemy info
        public override void Update()
        {
            if (attackOffset.X < 0)
            {
                attackOffset += new Vector2(1, -1);
            }
        }
    }
}
