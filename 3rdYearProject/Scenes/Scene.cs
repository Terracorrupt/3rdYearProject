using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3rdYearProject
{
    public abstract class Scene
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}