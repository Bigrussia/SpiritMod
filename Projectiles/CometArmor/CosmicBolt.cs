﻿using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles.CometArmor
{
	class CosmicBolt : ModProjectile
	{

		public override void SetDefaults()
        { 

            projectile.name = "Cosmic Bolt";
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 7;
			projectile.timeLeft = 800;
			projectile.height = 6;
			projectile.width = 6;
            projectile.alpha = 255;
			aiType = ProjectileID.Bullet;
			projectile.extraUpdates = 1;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("StarFracture"), 240, false);
            }
        }

        public override void AI(){

			Vector2 targetPos = projectile.Center;
			float targetDist = 450f;
			bool targetAcquired = false;

			//loop through first 200 NPCs in Main.npc
			//this loop finds the closest valid target NPC within the range of targetDist pixels
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].CanBeChasedBy(projectile) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
				{
					float dist = projectile.Distance(Main.npc[i].Center);
					if (dist < targetDist)
					{
						targetDist = dist;
						targetPos = Main.npc[i].Center;
						targetAcquired = true;
					}
				}
			}
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 133, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                int dust2 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 133, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 0f;
                Main.dust[dust2].velocity *= 0f;
                Main.dust[dust2].scale = 0.9f;
                Main.dust[dust].scale = 0.9f;
            }

            //change trajectory to home in on target
            if (targetAcquired)
			{
				float homingSpeedFactor = 6f;
				Vector2 homingVect = targetPos - projectile.Center;
				float dist = projectile.Distance(targetPos);
				dist = homingSpeedFactor / dist;
				homingVect *= dist;

				projectile.velocity = (projectile.velocity * 20 + homingVect) / 21f;
			}
		}

	}
}
