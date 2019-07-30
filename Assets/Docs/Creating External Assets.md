# Introduction

Creating external assets allows you to inject new content into ARStudio. This is done through Unity's AssetBundles, which is basically runtime assets that can be loaded whenever.

For more information about **Asset Bundles**, you can find it here in the [Unity Documentation](https://docs.unity3d.com/Manual/AssetBundlesIntro.html).

# Requirements

They can be setup in any way you want, except for that you need a prefab in your AssetBundle with an `AssetBundleDescriptor` called "AssetBundleDescriptor" in the root.

# Setting up a character.

A character is just basically a prefab with an `ObjectDescriptor`. There are more advanced descriptors the are derived from that class. These include:

* `ARStaticDescriptor`, for static objects (props, not animated)
* `ARDynamicDescriptor`, for dynamic objects that react to physics, or are animated,
* `ARCharacterDescriptor`, for character objects with advanced controls.

Just simply fill in the blanks. If you know your way around Unity, the field names should be self-explainatory.
Make sure all characters have Prefabs and that a reference to their prefab is referenced in the "AssetBundleDescriptor" prefab.

# Note

Remember that to include something in a package, you tag it in the inspector for all assets that are required.

![TAG IT!!!! (Bottom of Inspector Window)](https://i.imgur.com/wYYXgYZ.png)