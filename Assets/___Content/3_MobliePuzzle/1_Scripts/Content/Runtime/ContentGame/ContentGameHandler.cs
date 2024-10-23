using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MuPuzzle.Content.Game
{
    public sealed class ContentGameHandler : ContentHandler<ContentGameData, ContentGameAct>
    {
        [TitleGroup("Base")]
        [SerializeField] private ContentGameComponent component;
    }
}
