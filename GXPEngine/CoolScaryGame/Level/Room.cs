﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

namespace CoolScaryGame
{
    public class Room : GameObject
    {
        Pivot tiles = new Pivot();
        Pivot objects = new Pivot();
        SpriteContainer roomContainer;
        public Room(string TMX)
        {
            TiledLoader build = new TiledLoader(TMX, tiles, false);
            build.LoadTileLayers();
            build.rootObject = objects;
            build.addColliders = true;
            build.autoInstance = true;
            build.LoadObjectGroups();
            foreach (GameObject obj in objects.GetChildren())
                if (obj is SpriteContainer)
                {
                    roomContainer = (SpriteContainer)obj;
                    roomContainer.Remove();
                    AddChild(roomContainer);
                }
            //roomContainer.proxy.AddChild(tiles);
            //roomContainer.proxy.AddChild(objects);
            AddChild(tiles);
            AddChild(objects);
            //foreach (GameObject obj in objects.GetChildren())
            //    obj.parent = roomContainer.proxy;
        }
    }
}