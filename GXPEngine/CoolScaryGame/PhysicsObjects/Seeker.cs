﻿using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    internal class Seeker : Player
    {
        float speed = 5;

        public Seeker(Vector2 Position) : base(Position, "square.png") 
        {
            RenderLayer = 1;
        }

        void Update()
        {
            //move using the arrow keys
            AddForce(Input.ArrowVector() * Time.deltaMillis * speed);

            //update all physics
            PhysicsUpdate();

            //move the camera towards the player
            CamManager.LerpToPoint(1, position + ActualVelocity * 0.5f, Time.deltaTime * 5);
        }
    }
}