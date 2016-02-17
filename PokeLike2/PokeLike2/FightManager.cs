using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    static class FightManager
    {
        public static void PlayerAttacksEnemy(Pokemon pokemon)
        {
            if (Player.Health > 0)
            {
                if (pokemon.Defense - Player.AttackPower * 2 < 0)
                    pokemon.Health += pokemon.Defense - Player.AttackPower * 2;
                else
                    Console.WriteLine("Deine Angriffskraft ist zu niedrig um Schaden zu verursachen.");// unvollständig
            }
            else
                return; // Todesevent fehlt
        }

        public static void EnemyAttacksPlayer(Pokemon pokemon)
        {
            if (pokemon.Health > 0)
            {
                if (Player.Defense - pokemon.AttackPower * 2 < 0)
                    Player.Health += Player.Defense - pokemon.AttackPower * 2;
                else
                {
                    Console.WriteLine("Die Angriffskraft des Gegners ist zu niedrig um Schaden zu verursachen.");// unvollständig
                }
            }
            else
                return; // Todesevent fehlt
        }

        public static void Fight(Pokemon pokemon)
        {
            if (Player.Init >= pokemon.Init)
            {
                while (Player.Health > 0 && pokemon.Health > 0)
                {
                    PlayerAttacksEnemy(pokemon);
                    EnemyAttacksPlayer(pokemon);
                }
            }
            else if (Player.Init < pokemon.Init)
            {
                while (Player.Health > 0 && pokemon.Health > 0)
                {
                    EnemyAttacksPlayer(pokemon);
                    PlayerAttacksEnemy(pokemon);
                }
            }
                GameManager.DestroyGameObject(pokemon);
        }
    }
}
