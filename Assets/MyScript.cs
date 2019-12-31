using UnityEngine;
using System.Reflection;
using System.Linq;

public class MyScript : MonoBehaviour
{
	// Ctor: Launched ever when in editor (not while playing !)
	public MyScript()
	{
		// WARNING!!! UnityException: GetName is not allowed to be called from the MonoBehaviour's constructor!
		// when calling {this.name}
		//Debug.Log($"Sender={this.name} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		//Debug.Log($"Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		Debug.Log($"Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");

		instantiated_InCtor = InstantiateTestPrefab( bInCtor : true );
		LogObjectState(nameof(instantiated_InCtor), instantiated_InCtor, bInCtor : true);
	}

	#region properties
	public void OnValidate()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
	}
	#endregion // properties

	#region initialization
	// Init order:
	// 1. Awake
	// 2. OnEnable
	// 3. Start
	public void Awake()
	{
		Debug.Log($"---------------------------------");
		Debug.Log($"-------------- AWAKE ------------");
		Debug.Log($"---------------------------------");
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");

		// Trying to instantiate prefab on Awake
		instantiated_InAwake = InstantiateTestPrefab();

		LogObjectState(nameof(instantiated_InCtor), instantiated_InCtor);
		LogObjectState(nameof(instantiated_InAwake), instantiated_InAwake);
	}

	public void OnEnable()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		LogObjectState("this", this);

		LogObjectState(nameof(instantiated_InCtor), instantiated_InCtor);
		LogObjectState(nameof(instantiated_InAwake), instantiated_InAwake);
	}

	public void Start()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		LogObjectState("this", this);

		instantiated_InStart = InstantiateTestPrefab();
		LogObjectState(nameof(instantiated_InCtor), instantiated_InCtor);
		LogObjectState(nameof(instantiated_InAwake), instantiated_InAwake);
		LogObjectState(nameof(instantiated_InStart), instantiated_InStart);
		Object transformGameObject = InstantiateTestObject(testClassTemplByTransform, "byTransform");
		Object spriteRendererGameObject = InstantiateTestObject(testClassTemplBySpriteRender, "bySpriteRenderer");
	}

	// Only in editor
	public void Reset()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		LogObjectState("this", this);

		LogObjectState(nameof(instantiated_InCtor), instantiated_InCtor);
	}
	#endregion // initialization

	#region finalization
	public void OnDisable()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		LogObjectState("this", this);

		LogObjectState(nameof(instantiated_InCtor), instantiated_InCtor);
		LogObjectState(nameof(instantiated_InAwake), instantiated_InAwake);
		LogObjectState(nameof(instantiated_InStart), instantiated_InStart);
	}

	public void OnDestroy()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		LogObjectState("this", this);

		LogObjectState(nameof(instantiated_InCtor), instantiated_InCtor);
		LogObjectState(nameof(instantiated_InAwake), instantiated_InAwake);
		LogObjectState(nameof(instantiated_InStart), instantiated_InStart);
	}
	#endregion // finalization

	#region InstantiateAndDestroyTest

	public Object InstantiateTestObject(Object templ, string objectTitle, bool bInCtor = false)
	{
		Debug.Log($"----- INSTANTIATE: templ={templ} objectTitle={objectTitle}");
		if( ! bInCtor)
		{
			Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		}
		else
		{
			Debug.Log("{MethodBase.GetCurrentMethod().Name}");
		}
		if( null == templ )
		{
			Debug.LogWarning($"Skipping {objectTitle} instantiation: {nameof(templ)} is null");
			return null;
		}
		if( ! templ )
		{
			Debug.LogWarning($"Skipping {objectTitle} instantiation: {nameof(templ)} return false");
			return null;
		}
		Object InstantiatedObject = Instantiate(templ);
		LogObjectState(nameof(InstantiatedObject), InstantiatedObject, bInCtor);
		return InstantiatedObject;
	}
	public GameObject InstantiateTestPrefab(bool bInCtor = false)
	{
		Debug.Log("--- INSTANTIATE TEST PREFAB ------");
		Object templ = testPrefab;
		const string objectName = "testPrefab";
		Object prefabObject = InstantiateTestObject(templ, objectName, bInCtor);
		if(prefabObject != null)
		{
			Debug.Log($"prefabObject: type={prefabObject.GetType()}");
		}
		else
		{
			Debug.Log("prefabObject is null");
		}
		GameObject prefab = (GameObject) prefabObject;
		if ( prefab == null )
		{
			Debug.LogError("Instantiated Prefab == null");
			return null;
		}
		return prefab;
	}
	// Setup in the spectator
	public GameObject testPrefab;
	public Transform testClassTemplByTransform;
	public SpriteRenderer testClassTemplBySpriteRender;

	GameObject instantiated_InCtor;
	GameObject instantiated_InAwake;
	GameObject instantiated_InStart;
	#endregion // InstantiateAndDestroyTest

	#region utils
	public static void LogObjectState(string header, Object obj, bool bInCtor = false)
	{
		Debug.Log($"------------- Logging \"{header}\" object");
		if( obj == null )
		{
			Debug.LogError($"obj == null");
			return;
		}
		else
		{
			Debug.Log($"Obj has valid reference");
		}
		Debug.Log($"Object type is {obj.GetType()}");
		if((obj is Component comp) && comp.gameObject != null)
		{
			if(bInCtor)
			{
				Debug.Log($"comp.gameObject is VALID");
			}
			else
			{
				Debug.Log($"comp.gameObject == {comp.gameObject.name}");
			}
		}
		else
		{
			if(obj is Component)
			{
				Debug.LogWarning($"comp.gameObject is null");
			}
		}
		if( ! obj)
		{
			Debug.LogError($"Object's bool operator returned false");
			return;
		}

		if( ! bInCtor )
		{
			Debug.Log($"Name={obj.name}; Sender={obj.GetInstanceID()}");
			if(obj is GameObject gameObj)
			{
				Debug.Log("is GameObject");
				Debug.Log($"activeSelf = {gameObj.activeSelf};   activeInHierarchy = {gameObj.activeInHierarchy};");
			}
			if(obj is Behaviour behaviour)
			{
				Debug.Log("is Behaviour");
				Debug.Log($"isActiveAndEnabled = {behaviour.isActiveAndEnabled};   enabled={behaviour.enabled}");
			}
		}
		Debug.Log($"}}}} -------- Logging \"{header}\" object");
	}
	#endregion
};
