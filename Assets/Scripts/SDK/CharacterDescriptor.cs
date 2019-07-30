using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDescriptor : ARDynamicDescriptor {
    [Header("Character Metadata")]
    public string universeName;

    public override ObjectType objectType {
        get { return ObjectType.Character; }
    }
}
