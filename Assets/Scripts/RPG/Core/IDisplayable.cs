using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG
{
    /// <summary>
    /// An abstract class that define an item that is able to display on UI as an image
    /// </summary>
    public interface IDisplayable
    {
        Sprite GetDisplayingImage();
    }
}