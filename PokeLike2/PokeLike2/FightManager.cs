// Copyright (c) 2016 Mischa Ahi
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    if (Game1.DebugMode == true)
                        Debug.WriteLine("Deine Angriffskraft ist zu niedrig um Schaden zu verursachen.");
            }
        }

        public static void EnemyAttacksPlayer(Pokemon pokemon)
        {
            if (pokemon.Health > 0)
            {
                if (Player.Defense - pokemon.AttackPower * 2 < 0)
                {
                    if (Player.Health + Player.Defense - pokemon.AttackPower * 2 > 0)
                        Player.Health += Player.Defense - pokemon.AttackPower * 2;
                    else
                        Player.Health = 0;
                }
                else
                    if(Game1.DebugMode == true)
                        Debug.WriteLine("Die Angriffskraft des Gegners ist zu niedrig um Schaden zu verursachen.");
            }
        }

        public static void Fight(Pokemon pokemon)
        { 
            // Fight between Player and any Enemy
            if (Player.Init >= pokemon.Init)
            {
                while (Player.Health > 0 && pokemon.Health > 0)
                {
                    PlayerAttacksEnemy(pokemon);
                    EnemyAttacksPlayer(pokemon);
                }
                if (Player.Health < 1)
                {
                    Player.Death();
                }
                else
                {
                    Player.Xp += pokemon.AttackPower * pokemon.Defense;
                    GameManager.Destroy(pokemon);
                    CollisionManager.Destroy(pokemon.collider);
                }
            }
            else if (Player.Init < pokemon.Init)
            {
                while (Player.Health > 0 && pokemon.Health > 0)
                {
                    EnemyAttacksPlayer(pokemon);
                    PlayerAttacksEnemy(pokemon);
                }
                if (Player.Health < 1)
                {
                    Player.Death();
                }
                else
                {
                    Player.Xp += pokemon.AttackPower * pokemon.Defense;
                    GameManager.Destroy(pokemon);
                    CollisionManager.Destroy(pokemon.collider);
                }
            }
            Player.CheckAndDoLvlUp();
        }
    }
}
