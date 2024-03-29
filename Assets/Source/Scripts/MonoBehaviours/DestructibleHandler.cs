﻿using System;
using Source.EasyECS;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class DestructibleHandler : EasyMonoBehaviour
    {
        [SerializeField] private Destructible[] destructibles;

        public Destructible[] Destructibles => destructibles;
        
        
    }
}