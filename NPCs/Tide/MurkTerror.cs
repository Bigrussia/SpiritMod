﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.Tide
{
    public class MurkTerror : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Murky Terror";
            npc.displayName = "Murky Terror";
            npc.damage = 30;
            npc.width = 66; //324
            npc.height = 54; //216
            npc.defense = 11;
            npc.lifeMax = 130;
            npc.knockBackResist = 0.45f;
            npc.noGravity = true;
            Main.npcFrameCount[npc.type] = 6;
            npc.HitSound = SoundID.NPCHit25;
            npc.DeathSound = SoundID.NPCDeath5;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            float num1164 = 4f;
            float num1165 = 0.75f;
            Vector2 vector133 = new Vector2(npc.Center.X, npc.Center.Y);
            float num1166 = Main.player[npc.target].Center.X - vector133.X;
            float num1167 = Main.player[npc.target].Center.Y - vector133.Y - 200f;
            float num1168 = (float)Math.Sqrt((double)(num1166 * num1166 + num1167 * num1167));
            if (num1168 < 20f)
            {
                num1166 = npc.velocity.X;
                num1167 = npc.velocity.Y;
            }
            else
            {
                num1168 = num1164 / num1168;
                num1166 *= num1168;
                num1167 *= num1168;
            }
            if (npc.velocity.X < num1166)
            {
                npc.velocity.X = npc.velocity.X + num1165;
                if (npc.velocity.X < 0f && num1166 > 0f)
                {
                    npc.velocity.X = npc.velocity.X + num1165 * 2f;
                }
            }
            else if (npc.velocity.X > num1166)
            {
                npc.velocity.X = npc.velocity.X - num1165;
                if (npc.velocity.X > 0f && num1166 < 0f)
                {
                    npc.velocity.X = npc.velocity.X - num1165 * 2f;
                }
            }
            if (npc.velocity.Y < num1167)
            {
                npc.velocity.Y = npc.velocity.Y + num1165;
                if (npc.velocity.Y < 0f && num1167 > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + num1165 * 2f;
                }
            }
            else if (npc.velocity.Y > num1167)
            {
                npc.velocity.Y = npc.velocity.Y - num1165;
                if (npc.velocity.Y > 0f && num1167 < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - num1165 * 2f;
                }
            }
            if (npc.position.X + (float)npc.width > Main.player[npc.target].position.X && npc.position.X < Main.player[npc.target].position.X + (float)Main.player[npc.target].width && npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && Main.netMode != 1)
            {
                npc.ai[0] += 4f;
                if (npc.ai[0] > 256f)
                {
                    npc.ai[0] = 0f;
                    int num1169 = (int)(npc.position.X + 10f + (float)Main.rand.Next(npc.width - 20));
                    int num1170 = (int)(npc.position.Y + (float)npc.height + 4f);
                    int num184 = 14;
                    if (Main.expertMode)
                    {
                        num184 = 18;
                    }
                    Projectile.NewProjectile((float)num1169, (float)num1170, 0f, 5f, mod.ProjectileType("WitherBolt"), num184, 0f, Main.myPlayer, 0f, 0f);
                    return;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override void NPCLoot()
        {
            InvasionWorld.invasionSize -= 1;
            if (InvasionWorld.invasionSize < 0)
                InvasionWorld.invasionSize = 0;
            if (Main.netMode != 1)
                InvasionHandler.ReportInvasionProgress(InvasionWorld.invasionSizeStart - InvasionWorld.invasionSize, InvasionWorld.invasionSizeStart, 0);
            if (Main.netMode != 2)
                return;
            NetMessage.SendData(78, -1, -1, "", InvasionWorld.invasionProgress, (float)InvasionWorld.invasionProgressMax, (float)Main.invasionProgressIcon, 0.0f, 0, 0, 0);
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            if (InvasionWorld.invasionType == SpiritMod.customEvent)
                return 2f;

            return 0;
        }
    }
}