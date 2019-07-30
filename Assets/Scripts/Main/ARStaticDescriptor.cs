using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARStaticDescriptor : ObjectDescriptor {
    public override ObjectType objectType {
        get { return ObjectType.Static; }
    }
}
