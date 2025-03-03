using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

namespace CoolScaryGame
{

    /// <summary>
    /// A sprite that is actually a wall
    /// </summary>
    public class WallSprite : InvisibleObject
    {
        Sprite renderer;
        bool rendererVertical = false;
        bool renderedThisFrame = false;
        public WallSprite(TiledObject obj) : base(obj, true, 0b1, 0, false)
        { 
        }
        public WallSprite(int width, int height, bool overrideVisible = false)
        : base(width, height, true, 0b1, 0, overrideVisible)
        {
        }
        public void Setup(float roomRot, int wallSprite)
        {
            AddProxy();
            rendererVertical = width < height;
            bool rotated = (((int)roomRot % 180) != 0);
            if (!(rendererVertical ^ rotated))
            {
                renderer = new AnimationSprite("Rooms/Textures/HorizontalWall7.png", 7, 1, 7, true, false, 0, 0);
                proxy.AddChild(renderer);
                ((AnimationSprite)renderer).SetFrame(GetRandomWall(wallSprite));

                //camera faces the wall
                renderer.SetOrigin(renderer.width * .5f, renderer.height);
                renderer.depthSort = true;
            }
            else if (!rotated)
            {
                renderer = new Sprite("Rooms/Textures/VerticalWall.png", true, false);
                proxy.AddChild(renderer);
                //camera is perpendicular to the wall, room rotated 0 or 180 degrees 
                renderer.SetOrigin(renderer.width * .5f, -1 + renderer.height - height * .5f);
                renderer.height = renderer.height + 1;
            }
            else
            {
                renderer = new Sprite("Rooms/Textures/VerticalWall.png", true, false);
                proxy.AddChild(renderer);
                //camera is perpendicular to the wall, because the room rotated 90 or 270 degrees
                renderer.SetOrigin(renderer.width * .5f, -1 + renderer.height - width * .5f);
                renderer.height = renderer.height + 1;
            }
            //is it just me,,,, or is the rot consuming?? epic win!
            renderer.rotation = -roomRot;

            if (rendererVertical)
                x -= width * .5f;
            else y -= height * .5f;

            rendererVertical ^= rotated;

        }
        public override void Render(GLContext glContext, int RenderInt)
        {
            renderedThisFrame = true;
            renderer.SetDepthByY(RenderInt);

            /*if (!rendererVertical)
            {
            //it was never meant to be
                Vector2 relPos = PlayerManager.GetPosition(RenderInt);
                relPos -= TransformPoint(0, 0);

                float s = (relPos.y + 105) * .1f;
                s *= s;
                float j = relPos.x * .005f;
                j *= j;
                renderer.alpha = .1f + .9f*Mathf.Clamp01((-s + 30) * -.01f + j);
            }*/

            base.Render(glContext, RenderInt);
        }

        void Update()
        {
            //this is gross. it sucks and i hate it.
            //oh the things you do for +20 fps
            if (!renderedThisFrame)
                collider.isTrigger = true;
            else
                collider.isTrigger = false;
            renderedThisFrame = false;
        }

        int GetRandomWall(int Type)
        {
            switch(Type)
            {
                case 0:
                    return Utils.Random(0, 4);
                case 1:
                    return Utils.Random(4, 7);
            }
            return 0;
        }

        public void removeWallIfHorizontal()
        {
            if (rendererVertical) return;
            renderer.visible = false;
        }
    }
}
