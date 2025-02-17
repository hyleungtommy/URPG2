﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "World/Dialog")]
    public class Dialog : ScriptableObject
    {
        [SerializeField] List<string> lines;

        public List<string> Lines
        {
            get { return lines; }
        }
    }
}
