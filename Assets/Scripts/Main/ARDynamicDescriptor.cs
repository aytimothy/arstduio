using System;
using System.Collections.Generic;
using UnityEngine;

public class ARDynamicDescriptor : ARStaticDescriptor {
    public List<ARDynamicFeatureDescriptor> features;
    public List<ARObjectAnimatorDescriptor> animatorDescriptors;
    public List<AROBjectSkinnedMeshRendererDescriptor> skinnedMeshRendererDescriptors;

    public override ObjectType objectType {
        get { return ObjectType.Dynamic; }
    }
}

[Serializable]
public class ARObjectAnimatorDescriptor {
    public string name;
    public bool useObjectName;
    public Animator animationController;
    public List<ARObjectAnimationParameters> animatorParameters;
}

[Serializable]
public abstract class ARDynamicFeatureDescriptor {
    public string featureID;
    public string featureInstanceID;
    public string featureData;
}

[Serializable]
public class ARObjectAnimationParameters {
    public AnimationParameterType type;
    public string name;
    public int defaultInt;
    public bool defaultBool;
    public float defaultFloat;

    public bool isManagedParameter;
    public ManagedParameterType managedParameterType;

    public enum AnimationParameterType {
        None,
        Integer,
        Float,
        Boolean,
        Trigger
    }

    public enum ManagedParameterType {
        Dummy,           // Does zip. It isn't a managed value.
        ObjectTransX,    // The position of the object, x-coordinate.
        ObjectTransY,    // The position of the object, y-coordinate.
        ObjectTransZ,    // The position of the object, z-coordinate.
        SelfTransX,      // The position of the camera, x-coordinate.
        SelfTransY,      // The position of the camera, y-coordinate.
        SelfTransZ,      // The position of the camera, z-coordinate.
        ObjectRotX,      // Above, but for euler rotations.
        ObjectRotY,
        ObjectRotZ,
        SelfRotX,
        SelfRotY,
        SelfRotZ,
        ObjectScaleX,   // Above, but for scale (local)
        ObjectScaleY,
        ObjectScaleZ,
        ObjectDistance, // Distance from the camera
        OriginDistance, // Distance from (0, 0, 0)
        DeltaX,         // Delta x-axis movement.
        DeltaY,         // Delta y-axis movement.
        DeltaZ,         // Delta z-axis movement.
        DeltaMag,       // Magnitude of the delta movement.
        Life,           // Number of seconds since spawned (resets every time the app is closed)
        PersistLife     // Number of seconds since spawned (does not reset; is persistent between app closures).
    }
}

[Serializable]
public class AROBjectSkinnedMeshRendererDescriptor {
    public SkinnedMeshRenderer renderer;
    public List<ARObjectBlendshapeDescriptor> blendShapes;
}

[Serializable]
public class ARObjectBlendshapeDescriptor {
    public string name;
    public int index;
}