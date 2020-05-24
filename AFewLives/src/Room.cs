using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using AFewLives.Entities;
using Microsoft.Xna.Framework;
using System;

namespace AFewLives
{
    class Room
    {
        public readonly List<Obstacle> walls = new List<Obstacle>();
        public readonly List<InteractableObstacle> interactables = new List<InteractableObstacle>();
        public readonly List<Spikes> spikes = new List<Spikes>();
        public readonly List<MovingPlatform> platforms = new List<MovingPlatform>();
        public readonly List<Door> doors = new List<Door>();
        private readonly Dictionary<RectangleF, CameraAim> cameraZones = new Dictionary<RectangleF, CameraAim>();
        private readonly CameraAim defaultCameraAim;

        public readonly List<Entity> thingsInSpiritRealm = new List<Entity>();
        public readonly List<Entity> solidThings = new List<Entity>();

        private readonly Color fgTint;
        private readonly RoomBackground bg;

        public Room(Color fgTint,
                    RoomBackground bg,
                    Dictionary<RectangleF, CameraAim> cameraZones=null, CameraAim? defaultCameraAim=null)
        {
            this.defaultCameraAim = defaultCameraAim ?? new CameraAim(1);
            this.cameraZones = cameraZones ?? new Dictionary<RectangleF, CameraAim>();
            this.fgTint = fgTint;
            this.bg = bg;
        }

        public void Update(float delta, Player player)
        {
            foreach(InteractableObstacle e in interactables)
            {
                e.Update(delta);
            }
            foreach (Obstacle wall in walls)
            {
                wall.Update(delta);
            }
            foreach (MovingPlatform p in platforms)
            {
                p.Update(delta, player);
            }
            if (!player.IsGhost)
            {
                foreach(Spikes spike in spikes)
                {
                    spike.Update(player);
                }
            }
        }

        public void DrawBackground(SpriteBatch sb, RenderTarget2D target, Effect effect, Matrix transform)
        {
            bg.Draw(sb, target, effect, transform);
        }

        public void DrawSolidThings(SpriteBatch spriteBatch)
        {
            foreach (Obstacle o in solidThings)
            {
                o.Draw(spriteBatch, fgTint);
            }
        }
 
        public void DrawSpiritThings(SpriteBatch spriteBatch)
        {
            foreach (Obstacle o in thingsInSpiritRealm)
            {
                o.Draw(spriteBatch, Color.White);
            }
        }

        public void SetCameraAim(Camera2D cam, Player player) {
            CameraAim aim = defaultCameraAim;
            foreach (var zone in cameraZones)
            {
                if (player.CollidesWith(zone.Key))
                {
                    aim = zone.Value;
                    break;
                }
            }
            aim.AimCamera(cam, player.Pos);
        }

        public void PartitionThings()
        {
            List<Obstacle> all = new List<Obstacle>(walls);
            all.AddRange(interactables);
            all.AddRange(platforms);
            all.AddRange(doors);
            all.AddRange(spikes);
            foreach(Obstacle e in all)
            {
                (e.inSpiritRealm ? thingsInSpiritRealm : solidThings).Add(e);
            }
        }
    }

    class RoomFactory
    {
        private readonly EntityFactory entityFactory;
        public RoomFactory(EntityFactory entityFactory)
        {
            this.entityFactory = entityFactory;
        }

        public Room Room1(RoomBackground bg)
        {
            var cameraZones = new Dictionary<RectangleF, CameraAim>();
            cameraZones.Add(new RectangleF(500, 100, 150, 200), new CameraAim(0.5f));
            cameraZones.Add(new RectangleF(200, 100, 100, 200), new CameraAim(5f, new Vector2(0, 100)));
            Room room = new Room(Color.DarkBlue, bg, cameraZones);
            Vector2 hWallSize = new Vector2(1500, 300);
            room.walls.Add(entityFactory.Wall(new Vector2(-100, -100), hWallSize));
            room.walls.Add(entityFactory.Wall(new Vector2(-100, 800), hWallSize));

            Vector2 vWallSize = new Vector2(300, 1200);
            room.walls.Add(entityFactory.Wall(new Vector2(-200, -100), vWallSize));
            room.walls.Add(entityFactory.Wall(new Vector2(1000, -100), vWallSize));

            Vector2 pitSideSize = new Vector2(250, 100);
            room.walls.Add(entityFactory.Wall(new Vector2(0, 750), pitSideSize));
            room.walls.Add(entityFactory.Wall(new Vector2(700, 750), pitSideSize));

            room.walls.Add(entityFactory.Wall(new Vector2(900, 500), new Vector2(200, 300)));

            List<RetractableWall> someRetractables = new List<RetractableWall>();
            for (int y = 300; y <= 750; y += 50)
            {
                someRetractables.Add(entityFactory.RetractableWall(new Vector2(250, y), new Vector2(0, 20), new Vector2(450, 20), y / 50));
            }
            someRetractables.Add(entityFactory.RetractableWall(new Vector2(150, 450), new Vector2(20, 50), new Vector2(20, 350), 100));
            room.walls.AddRange(someRetractables);
            List<Activatable> leverTargets = new List<Activatable>(someRetractables);

            room.spikes.Add(entityFactory.Spikes(new Vector2(250, 750), new Vector2(450, 50)));

            List<Vector2> path = new List<Vector2>() {
                new Vector2(100, 300),
                new Vector2(500, 300),
                new Vector2(200, 600),
            };
            MovingPlatform platform = entityFactory.MovingPlatform(path, new Vector2(50, 50));
            room.platforms.Add(platform);
            leverTargets.Add(platform);

            room.interactables.Add(entityFactory.Lever(new Vector2(200, 742), leverTargets));

            room.doors.Add(entityFactory.Door(new Vector2(400, 284), room));

            room.walls.Add(entityFactory.SpiritWall(new Vector2(750, 300), new Vector2(100, 100)));
            room.PartitionThings();
            return room;
        }

        public Room Room2(RoomBackground bg)
        {
            Room room = new Room(new Color(140, 0, 100), bg);
            int topGap = 150;
            int pitWidth = 450;
            int pitHeight = 400;
            var roof = entityFactory.Wall(new Vector2(-100, -500), new Vector2(1500, 500));
            var leftWall = entityFactory.Wall(new Vector2(-800, -500), new Vector2(700, 2000));
            var leftPlatform = entityFactory.Wall(new Vector2(-100, roof.Hitbox.Bottom + topGap), new Vector2(200, 800));
            var rightPlatform = entityFactory.Wall(new Vector2(leftPlatform.Hitbox.Right + pitWidth,
                                                               leftPlatform.Pos.Y),
                                                   new Vector2(topGap, 800));
            var rightWall = entityFactory.Wall(new Vector2(rightPlatform.Hitbox.Right, -500), new Vector2(700, 2000));
            var pitFloor = entityFactory.Wall(new Vector2(-500, leftPlatform.Pos.Y + pitHeight), new Vector2(2000, 500));
            var ghostDoor = entityFactory.RetractableWall(new Vector2(leftPlatform.Pos.X + 100, 0),
                                                          new Vector2(10, 0), new Vector2(10, topGap),
                                                          50, true);
            var rightDoor = entityFactory.RetractableWall(new Vector2(rightPlatform.Pos.X + 50, 0),
                                                          new Vector2(10, 0), new Vector2(10, topGap),
                                                          50, false);
            room.walls.AddRange(new List<Obstacle>(){ roof, leftWall, leftPlatform, rightPlatform, rightWall, pitFloor,
                                                      ghostDoor, rightDoor });

            room.interactables.Add(entityFactory.Lever(new Vector2(leftPlatform.Pos.X + 50, leftPlatform.Pos.Y - 8),
                                                       new List<Activatable>() { ghostDoor }, true));

            room.interactables.Add(entityFactory.Lever(new Vector2(rightPlatform.Pos.X + 20, rightPlatform.Pos.Y - 8),
                                                       new List<Activatable>() { rightDoor }));

            room.doors.Add(entityFactory.Door(new Vector2(leftPlatform.Pos.X + 20, leftPlatform.Pos.Y - 16), room));
            room.doors.Add(entityFactory.Door(new Vector2(rightWall.Pos.X - 20, rightPlatform.Pos.Y - 16), room));

            room.PartitionThings();
            return room;
        }

        public Room Room3(RoomBackground bg)
        {
            Room room = new Room(Color.DarkGoldenrod, bg);
            Vector2 topLeft = new Vector2(-500, -500);
            var gapWidth = 150;

            // Solid walls
            var ceiling = entityFactory.Wall(topLeft, new Vector2(1600, 500));
            var leftWall = entityFactory.Wall(topLeft, new Vector2(500, 1400));
            var floor = entityFactory.Wall(new Vector2(topLeft.X, leftWall.Hitbox.Bottom - 500), new Vector2(1600, 500));
            var rightWall = entityFactory.Wall(new Vector2(ceiling.Hitbox.Right - 500, topLeft.Y), new Vector2(500, 1600));
            var middleFloorLeft = entityFactory.Wall(new Vector2(topLeft.X + leftWall.Hitbox.Width,
                                                                 (ceiling.Pos.Y + ceiling.Hitbox.Height + floor.Pos.Y) / 3),
                                                     new Vector2(150, 200));
            var rx = middleFloorLeft.Pos.X + middleFloorLeft.Hitbox.Width + gapWidth;
            var middleFloorRight = entityFactory.Wall(new Vector2(rx, middleFloorLeft.Pos.Y),
                                                      new Vector2(rightWall.Pos.X - rx, 50));
            // Doors
            var sideDoor = entityFactory.RetractableWall(new Vector2(middleFloorLeft.Pos.X + middleFloorLeft.Hitbox.Width - 50,
                                                                     middleFloorLeft.Pos.Y + middleFloorLeft.Hitbox.Height),
                                                         new Vector2(25, 0), new Vector2(25, 66), 10);

            var ceilingBottom = ceiling.Pos.Y + ceiling.Hitbox.Height;
            var doorBlockHeight = middleFloorRight.Pos.Y - ceilingBottom;
            var doorBlockLeft = entityFactory.RetractableWall(new Vector2(middleFloorRight.Pos.X + 20, ceilingBottom),
                                                              new Vector2(20, 20), new Vector2(20, doorBlockHeight), 10, true);
            var doorBlockRight = entityFactory.RetractableWall(new Vector2(middleFloorRight.Pos.X + 50, ceilingBottom),
                                                               new Vector2(20, 20), new Vector2(20, doorBlockHeight), 10, true);

            var gapStart = middleFloorLeft.Pos.X + middleFloorLeft.Hitbox.Width;
            var elevator = entityFactory.MovingPlatform(new List<Vector2>() {
                                                            new Vector2(gapStart, floor.Pos.Y),
                                                            new Vector2(gapStart, middleFloorLeft.Pos.Y)
                                                        },
                                                        new Vector2(gapWidth, 10), 3, false);
            room.platforms.Add(elevator);
            room.walls.AddRange(new List<Obstacle>() {
                ceiling, leftWall, floor, rightWall, middleFloorLeft, middleFloorRight,
                sideDoor, doorBlockLeft, doorBlockRight,
            });

            room.doors.Add(entityFactory.Door(new Vector2(rightWall.Pos.X - 100, floor.Pos.Y - 16), room));
            room.doors.Add(entityFactory.Door(new Vector2(rightWall.Pos.X - 100, middleFloorRight.Pos.Y - 16), room));

            // Levers
            room.interactables.Add(entityFactory.Lever(new Vector2(middleFloorLeft.Pos.X + 20, middleFloorLeft.Pos.Y - 8),
                                                       new List<Activatable>() { sideDoor, doorBlockLeft }, true));
            room.interactables.Add(entityFactory.Lever(new Vector2(sideDoor.Pos.X - 20, floor.Pos.Y - 8),
                                                       new List<Activatable>() {elevator, doorBlockRight}));
            var elevatorLever = entityFactory.Lever(new Vector2(elevator.Pos.X + gapWidth / 2, elevator.Pos.Y - 8),
                                                       new List<Activatable>() { elevator, doorBlockRight });
            room.interactables.Add(elevatorLever);
            elevator.Attach(elevatorLever);

            //TODO: Revifier

            room.PartitionThings();
            return room;
        }
    }
}
