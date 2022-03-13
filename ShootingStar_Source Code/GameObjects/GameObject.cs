using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace ShootingStar.GameObjects
{
    class GameObject : ICloneable
    {
        protected Texture2D _texture; // texture for obj

        public Vector2 _position; // contain position x, y
        public Vector2 _scale; // scaling obj
        public float _rotation; // rotating obj
        public string _name; // contain name of obj
        public Rectangle _viewport;
        //public Dictionary<string, SoundEffectInstance> SoundEffects;

        public bool _isActive; // contain status of this obj

        public Vector2 _mousePos;

        public GameObject(Texture2D texture)
        {
            _texture = texture;
            _position = Vector2.Zero;
            _scale = Vector2.One;
            _rotation = 0f;
            _name = "null";
            _isActive = true;
        }

        public virtual void Update(List<GameObject> gameObjects)
        {
        }

        public virtual void Update(GameObject gameObjects)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Rectangle _rectangle
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, Singleton.STARSIZE, Singleton.STARSIZE);
            }
        }

    }
}
