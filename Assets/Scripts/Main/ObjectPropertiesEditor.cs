using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPropertiesEditor : MonoBehaviour {
	public string objectUUID;
	public ObjectPropertiesScreen parent;
	public ARObject arObject {
		get { 
			if (_arObject == null) _arObject = ObjectManager.GetObjectByUUID(objectUUID);
			return _arObject;
		}
	}
	protected ARObject _arObject;
	
	public bool loaded {
		get { return _loaded; }
		protected set { _loaded = value; }
	}
	protected bool _loaded;
	
	public virtual void Init(string objectUUID) {
		this.objectUUID = objectUUID;
		LoadMenu();
		loaded = true;
	}
	
	public virtual void Init(string objectUUID, ObjectPropertiesScreen parent) {
		this.parent = parent;
		Init(objectUUID);
	}
	
	public abstract void LoadMenu();
	public virtual void LoadMenu(string objectUUID) {
		this.objectUUID = objectUUID;
		LoadMenu();
	}
}
