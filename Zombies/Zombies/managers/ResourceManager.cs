using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.managers
{
    class ResourceManager
    {
        private Hashtable Textures;

        public ResourceManager()
        {
            Textures = new Hashtable();
        }

        public Texture2D RequestTexture(string name)
        {
            if (name != null)
            {
                if (Textures.ContainsKey(name))
                    return (Texture2D)Textures[name];
                else
                {
                    Texture2D temp = Game1.Instance.Content.Load<Texture2D>(name);
                    Textures.Add(name, temp);
                    return temp;
                }
            }
            return null;
        }
    }
}
