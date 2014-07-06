using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Client.Logic.Players
{
    interface IPlayer : Graphics.Renderers.Sprites.ISprite
    {
        PlayerType PlayerType { get; }
        string Name { get; set; }
        string MapID { get; set; }
        string Guild { get; set; }
        Enums.GuildRank GuildAccess { get; set; }
        string Status { get; set; }
        bool Hunted { get; set; }
        bool Dead { get; set; }
        Enums.Rank Access { get; set; }
        string ID { get; set; }
        PlayerPet[] Pets {get;set;}
    }
}
