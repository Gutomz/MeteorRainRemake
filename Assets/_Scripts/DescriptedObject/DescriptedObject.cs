using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public abstract class DescriptedObject : ScriptableObject
    {
        [SerializeField]
        protected Sprite image;

        [SerializeField]
        protected new string name;

        [SerializeField, TextArea]
        protected string description;

        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public Sprite Image { get { return image; } }
    }
}
