using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.particleEffects;
using Zombies.states;

namespace Zombies.entities.weapons
{
    class Explosion : Entity
    {
        private float explosionRadius;
        private float damage;
        private float knockEffect;
        private bool exploded;
        private Vector2 position;

        public Explosion(float explosionRadius, float damage, float knockEffect, Vector2 position)
        {
            this.explosionRadius = explosionRadius;
            this.damage = damage;
            this.knockEffect = knockEffect;
            this.exploded = false;
            this.position = position;
        }

        public void Explode()
        {
            if (exploded)
                return;
            else
                exploded = true;
            CreateEntity(new ParticleExplosion(position));

            Alive = false;
            ArrayList hits = new ArrayList();
            EntitiesInRadius(explosionRadius, position, hits);

            float distToTarget;
            for (int i = 0; i < hits.Count; i++)
            {
                if (hits[i] is PhysicalEntity)
                {

                    if (hits[i] == this)
                        continue;
                    Vector2 tmp = (((PhysicalEntity)hits[i]).Position - position);
                    distToTarget = explosionRadius + 25 - tmp.Length();
                    tmp.Normalize();

                    if (hits[i] is Explosive)
                        ((Explosive)hits[i]).BlowUp();
                    if (hits[i] is Being)
                    {
                        ((BeingState)((Being)hits[i]).CurrentState).GetHit(damage);
                        //CreateEntity(new BloodSplat(((Being)hits[i]).CenterPosition, tmp * distToTarget));
                    }
                    ((PhysicalEntityState)((PhysicalEntity)hits[i]).CurrentState).GetPushed(tmp * knockEffect * distToTarget);
                }
            }
        }
    }
}
