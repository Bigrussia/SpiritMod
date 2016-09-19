using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
namespace SpiritMod.Items.Weapon.Gun
{
    public class ShadowShot : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Shadow Shot";  
            item.damage = 21;  
            item.ranged = true;
            item.width = 50;
            item.height = 38;    
            item.useTime = 33;  
            item.useAnimation = 33;
            item.useStyle = 5;    
            item.noMelee = true; 
            item.knockBack = 4;
            item.value = 100000;
            item.rare = 6;
            item.useSound = 11;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("VileBullet"); 
            item.shootSpeed = 9f;
            item.useAmmo = ProjectileID.Bullet;
            item.crit = 6;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 4; i++)
            {
                float spread = 30f * 0.0174f;//45 degrees converted to radians
                float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                double baseAngle = Math.Atan2(speedX, speedY);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                speedX = baseSpeed * (float)Math.Sin(randomAngle);
                speedY = baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("VileBullet"), item.damage, knockBack, item.owner, 0, 0);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Boomstick);
            recipe.AddIngredient(ItemID.Musket);
            recipe.AddIngredient(ItemID.PhoenixBlaster, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}