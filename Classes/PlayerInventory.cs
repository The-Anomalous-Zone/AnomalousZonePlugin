using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;
using Exiled.API.Extensions;
using InventorySystem;
using System.Collections;

namespace AnomalousZonePlugin.Classes
{
    public class PlayerInventory
    {
        private List<Item> items;
        private Player player;
        public PlayerInventory(Player player)
        {
            this.player = player;
            items = new List<Item>();
        }

        public void addItem(Item item)
        {
            items.Add(item);
        }
        public void removeItem(Item item)
        {
            items.Remove(item);
        }
        public static Item[] getItems(PlayerInventory player)
        {
            return player.items.ToArray();
        }
    }
}