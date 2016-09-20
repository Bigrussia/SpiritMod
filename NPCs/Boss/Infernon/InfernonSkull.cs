﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.Boss.Infernon
{
    public class InfernonSkull : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Infernon Skull";
            npc.width = npc.height = 80;

            npc.damage = 0;
            npc.lifeMax = 10;

            npc.alpha = 255;

            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.dontTakeDamage = true;

            Main.npcFrameCount[npc.type] = 4;
        }

        public override bool PreAI()
        {
            if (!Main.npc[(int)npc.ai[3]].active || Main.npc[(int)npc.ai[3]].type != mod.NPCType("Infernon"))
            {
                npc.ai[0] = -1;
            }

            if (npc.ai[0] == -1)
            {
                npc.alpha += 3;
                if (npc.alpha > 255)
                    npc.active = false;
            }
            if (npc.ai[0] == 0)
            {
                npc.ai[1]++;
                if (npc.ai[1] >= 60)
                {
                    npc.ai[0] = 1;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                }
            }
            if (npc.ai[0] == 1)
            {
                if (npc.ai[1] == 0)
                {
                    npc.TargetClosest(false);
                    Vector2 spinningpoint = Main.player[npc.target].Center - npc.Center;
                    spinningpoint.Normalize();
                    float dir = -1f;
                    if ((double)spinningpoint.X < 0.0)
                        dir = 1f;
                    Vector2 pos = Utils.RotatedBy(spinningpoint, -dir * 6.28318548202515 / 6.0, new Vector2());
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, pos.X, pos.Y, mod.ProjectileType("InfernonSkull_Laser"), 5, 0.0f, Main.myPlayer, (dir * 6.28F / 540), npc.whoAmI);
                    npc.netUpdate = true;
                }
                npc.ai[1]++;
                if (npc.ai[1] >= 120)
                {
                    npc.ai[0] = 2;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                }
            }
            if (npc.ai[0] == 2)
            {
                if (npc.ai[1] == 0)
                {
                    npc.alpha += 3;
                    if(npc.alpha > 255)
                    {
                        // Teleport.
                        NPC target = Main.npc[(int)npc.ai[3]];
                        Vector2 newPos = target.Center + new Vector2(Main.rand.Next(-200, 201), Main.rand.Next(-200, 201));
                        npc.Center = newPos;

                        npc.ai[1] = 1;
                    }
                }
                else
                {
                    npc.alpha -= 3;

                    if (npc.alpha <= 0)
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                    }
                }
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 0)
            {
                npc.frame.Y = 0;
            }
            else if (npc.ai[0] == 1)
            {
                npc.frame.Y = frameHeight;
            }
            else if (npc.ai[0] == 2)
            {
                if (npc.alpha >= 0 && npc.alpha < 100)
                    npc.frame.Y = 0;
                else if (npc.alpha >= 100 && npc.alpha < 175)
                    npc.frame.Y = frameHeight * 2;
                else if (npc.alpha >= 175 && npc.alpha < 255)
                    npc.frame.Y = frameHeight * 3;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[0] == 1)
            {
                Texture2D glowmask = mod.GetTexture("Effects/Glowmasks/InfernonSkull_Glowmask");
                Vector2 origin = new Vector2(glowmask.Width * 0.5F, glowmask.Height * 0.5F);
                spriteBatch.Draw(glowmask, (npc.Center - origin) - Main.screenPosition, Color.White);
            }
            return true;
        }
    }
}