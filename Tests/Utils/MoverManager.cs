using BulletML;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests.Utils
{
    public class MoverManager : IBulletManager
    {
        // Public to be used in the unit tests
        public readonly List<Mover> Movers = new List<Mover>();

        private readonly List<Mover> _topLevelMovers = new List<Mover>();
        private readonly PositionDelegate _getPlayerPosition;

        public MoverManager(PositionDelegate playerDelegate)
        {
            Debug.Assert(null != playerDelegate);
            _getPlayerPosition = playerDelegate;
        }

        public Vector2 PlayerPosition(IBullet targettedBullet)
        {
            Debug.Assert(null != _getPlayerPosition);
            return _getPlayerPosition();
        }

        public IBullet CreateBullet(bool topBullet = false)
        {
            var mover = new Mover(this);
            mover.Init();

            if (topBullet)
                _topLevelMovers.Add(mover);
            else
                Movers.Add(mover);

            return mover;
        }

        public void RemoveBullet(IBullet deadBullet)
        {
            var myMover = deadBullet as Mover;

            if (myMover != null)
                myMover.Used = false;
        }

        public void Update()
        {
            for (int i = 0; i < Movers.Count; i++)
                Movers[i].Update();

            for (int i = 0; i < _topLevelMovers.Count; i++)
                _topLevelMovers[i].Update();

            FreeMovers();
        }

        public void FreeMovers()
        {
            for (var i = 0; i < Movers.Count; i++)
            {
                if (!Movers[i].Used)
                {
                    Movers.Remove(Movers[i]);
                    i--;
                }
            }

            // Clear out top level bullets
            for (var i = 0; i < _topLevelMovers.Count; i++)
            {
                if (_topLevelMovers[i].TasksFinished())
                {
                    _topLevelMovers.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Clear()
        {
            Movers.Clear();
            _topLevelMovers.Clear();
        }
    }
}