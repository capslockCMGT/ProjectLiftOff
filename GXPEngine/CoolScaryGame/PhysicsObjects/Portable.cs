﻿using CoolScaryGame;
using CoolScaryGame.Particles;
using GXPEngine.CoolScaryGame.Particles;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Portable : RigidBody
    {
        public float StunableTimer;
        public Portable(float x = 0, float y = 0) : base(64 ,32, new Core.Vector2(0, -100))
        {
            CollisionLayers = 0b11;
            renderer = new Sprite("square.png", false, false);
            renderer.y = -renderer.height / 2;
            proxy.AddChild(renderer);
            SetXY(x, y);
        }

        void Update()
        {
            StunableTimer -= Time.deltaTime;
            PhysicsUpdate();
        }

        public void Drop(Vector2 Position, Vector2 Velocity)
        {
            position = position + Velocity.Normalized * 10;
            this.Velocity = Velocity + Velocity.Normalized * 600;
            StunableTimer = 1;
            isDissabled = false;
            isKinematic = false;
        }

        protected override void OnDestroy()
        {
            ParticleData dat = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                SpawnPosition = renderer.TransformPoint(renderer.width / 2, renderer.height / 2),
                ForceDirection = Velocity / 500,
                burst = 30,
                LifeTime = 1,
                EmissionStep = 0,
                EmissionTime = 0,
                Scale = 0.4f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                R = 0.4f,
                G = .4f,
                B = 1f,
            };

            SceneManager.AddParticles(dat);
        }
    }
}
