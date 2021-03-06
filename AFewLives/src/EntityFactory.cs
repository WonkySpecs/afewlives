﻿using AFewLives.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using FNAExtensions;

namespace AFewLives
{
    class EntityFactory
    {
        public readonly AssetStore assets;

        public EntityFactory(AssetStore assets)
        {
            this.assets = assets;
        }

        public Player Player(Vector2 pos)
        {
            return new Player(assets.PlayerSprite, pos);
        }

        public Corpse Corpse(Vector2 pos, Vector2 vel)
        {
            return new Corpse(assets.CorpseSprite, pos, vel);
        }

        public Obstacle Wall(Vector2 pos, Vector2 size)
        {
            return new Obstacle(assets.Patchy(size), size, pos);
        }

        public Obstacle SpiritWall(Vector2 pos, Vector2 size)
        {
            return new Obstacle(assets.Stripy(size), size, pos, true);
        }

        public Lever Lever(Vector2 pos, List<Activatable> targets, bool inSpiritRealm=false, bool startOn=false)
        {
            return new Lever(assets.LeverSprite(), pos, new Rectangle(0, 0, 16, 8), targets, startOn, inSpiritRealm);
        }

        public RetractableWall RetractableWall(Vector2 pos, Vector2 retractedSize,
                                               Vector2 fullSize, int retractTime, bool startRetracted=false,
                                               bool inSpiritRealm=false)
        {
            return new RetractableWall(assets.Stripy(fullSize), retractedSize,
                                       fullSize, pos, inSpiritRealm, startRetracted, retractTime);
        }

        public Spikes Spikes(Vector2 pos, Vector2 size)
        {
            Texture2D tex = assets.SpikesTexture((int)size.X);
            if (size.Y != tex.Height)
            {
                Console.WriteLine("Built a spikes with height different to the sprite height");
            }
            return new Spikes(tex, size, pos);
        }

        public MovingPlatform MovingPlatform(List<Vector2> path, Vector2 size,
                                             float speed=3f, bool startActive=true)
        {
            return new MovingPlatform(assets.Stripy(size), path[0],
                                      size.ToRectangle(), false, path, speed, startActive);
        }

        public Door Door(Vector2 pos, Room containingRoom)
        {
            return new Door(assets.SolidColor(new Vector2(16, 16), Color.Black), pos, containingRoom);
        }
    }
}
