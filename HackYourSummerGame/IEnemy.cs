using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackYourSummerGame
{
    interface IEnemy
    {
        /// <summary>
        /// Get next move for enemy
        /// </summary>
        /// <param name="target">target for enemy</param>
        public void GetNextMove(Character target);
    }
}
