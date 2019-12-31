using UnityEngine;
using System.Reflection;

public class MyDerivedScript : MyScript
{
	// Ctor: Launched ever when in editor (not while playing !)
	public MyDerivedScript()
	{
		// WARNING!!! UnityException: GetName is not allowed to be called from the MonoBehaviour's constructor!
		// when calling {this.name}
		//Debug.Log($"Sender={this.name} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		//Debug.Log($"Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		Debug.Log($"Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
	}

	#region properties
	public new void OnValidate()
	{
		// WARNING!!! When we hide member (OnValidate(), for example),
		// method, that is defined in the base class will NOT be called!!!
		// Q. So, how in C# to call base UNHIDDEN method?)
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
	}
	#endregion // properties

	#region initialization
	// Init order:
	// 1. Awake
	// 2. OnEnable
	// 3. Start
	public new void Awake()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		// WARNING!!! When we hide member (OnValidate(), for example),
		// method, that is defined in the base class will NOT be called!!!
		// Q. So, how in C# to call base UNHIDDEN method?)
		base.Awake();
	}

	public new void OnEnable()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		base.OnEnable();
	}

	public new void Start()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		base.Start();
	}

	// Only in editor
	public new void Reset()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
	}
	#endregion // initialization

	#region finalization
	public new void OnDisable()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		base.OnDisable();
	}

	public new void OnDestroy()
	{
		Debug.Log($"Sender={this.name} Sender={this.GetInstanceID()} Method={MethodBase.GetCurrentMethod().Name} DeclaringType={MethodBase.GetCurrentMethod().DeclaringType}");
		base.OnDestroy();
	}
	#endregion // finalization
};
