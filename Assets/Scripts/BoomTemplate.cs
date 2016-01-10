using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class BoomTemplate
    {
        public int Id;
        public Texture2D AlphaMask;
        public int ColliderDiameterPixels;
        public int ShockWaveDiameterPixels;
    }
}