using Terraria.ModLoader;

namespace SpiritMod.Items.Boss
{
	public class Trophy5: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vinewrath Bane Trophy");
         }
        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 0;
			item.rare = 1;
			item.createTile = mod.TileType("Trophy5Tile");
			item.placeStyle = 0;
		}
	}
}