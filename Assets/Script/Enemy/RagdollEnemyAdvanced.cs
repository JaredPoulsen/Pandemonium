using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This class is made to store the position and rotation parameters of a bone.
//Mainly used for the transition from Ragdoll to Animator.

public class BoneTransformInfo2
{
	public Transform Transform;
	public Vector3 StoredPosition;
	public Quaternion StoredRotation;
	public BoneTransformInfo2(Transform t)
	{
		Transform = t;
	}
}

public class RagdollEnemyAdvanced : MonoBehaviour
{
	private EnemyBase enemy;
	private ThirdPersonController player;

	private Animator animator;
	private Rigidbody rigidBody;

	[HideInInspector]
	public bool RagdollEnabled;

	//Ragdoll States
	public enum RagdollState
	{
		/// Animator is fully in control
		Animated,
		/// Animator turned off, but when stable position will be found, the transition to Animated will heppend
		WaitStablePosition,
		/// Animator turned off, physics controls the ragdoll
		Ragdolled,
		/// Animator in control, but LateUpdate() is used to partially blend in the last ragdolled pose
		BlendToAnim,
	}
	public RagdollState State;
	private bool GetUpFromBelly;

	//Human Body Bones
	public Transform[] AllBones;
	public Rigidbody[] RagdollBones;

	private Transform Hips, HipsParent, Head;
	private Rigidbody HipsRigidbody;

	//List of stored bone transformation information
	private List<BoneTransformInfo> bones = new List<BoneTransformInfo>();

	//Current transition weight
	private float BlendAmount;

	//>>> Settings
	[Range(1, 5f)]
	public float TimeToGetUp = 3f;

	[Range(1, 5f)]
	public float BlendSpeed = 2f;

	public float RagdollDrag = 0.5f;


	//Debbuging
	public bool RagdollWhenPressKeyG;
	public bool ViewHumanBodyBones;
	public bool ViewBodyPhysics;
	public bool ViewBodyDirection;

	float timebtwShot;

	private void Start()
	{	
		StartAdvancedRagdollController();
		timebtwShot = enemy.timeBetweenShot;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G) && RagdollWhenPressKeyG)
		{
			State = RagdollState.Ragdolled;
		}
		RagdollStatesController();
		
	}
	private void LateUpdate()
	{
		BlendRagdollToAnimator();
	}

	public void StartAdvancedRagdollController()
	{
		enemy = GetComponent<EnemyBase>();
		animator = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody>();

		//Stores Humanoid hips transform, hips parent and rigidbody of humanoid hips
		if (animator == null || enemy == null) return;

		Hips = animator.GetBoneTransform(HumanBodyBones.Hips);
		HipsParent = Hips.parent;
		HipsRigidbody = Hips.GetComponent<Rigidbody>();
		Head = animator.GetBoneTransform(HumanBodyBones.Head);

		//Stores All bones and Rigidbody Bones used for ragdoll
		RagdollBones = Hips.GetComponentsInChildren<Rigidbody>();
		AllBones = Hips.GetComponentsInChildren<Transform>();

		//adds a bone reference to a list of a class that stores
		//the bone information needed for the transition from ragdoll to animator.
		foreach (var bone in AllBones)
		{
			bones.Add(new BoneTransformInfo(bone.transform));
		}

		//Disable Ragdoll Physics
		SetActiveRagdoll(false);


		//print("JU TPS Advanced Ragdoll Controller Started");
	}

	public void RagdollStatesController()
	{
	
		//Animated
		if (State == RagdollState.Animated && animator.enabled == false)
		{
			animator.enabled = true;
			RagdollEnabled = false;
			SetActiveRagdoll(false);

		}

		//Ragdolled
		if (State == RagdollState.Ragdolled)
		{
			enemy.timeBetweenShot = 1000000;
			
			foreach (Rigidbody rb in RagdollBones)
            {

				rb.velocity = (transform.forward * enemy.bulletRecord.x + transform.right * enemy.bulletRecord.z + transform.up * 0.2f).normalized * 15f;
			}

			if (RagdollEnabled == false)
			{
				SetActiveRagdoll(true, true);
				Hips.parent = null;
				
			}

			if (HipsRigidbody.velocity.magnitude < 0.01f && IsInvoking("SetWaitStablePositionInvoked") == false)
			{
				Invoke("SetWaitStablePositionInvoked", TimeToGetUp);
			}
			if (Hips.parent == null)
			{
				RaycastHit hipshitground;
				LayerMask groundmask = LayerMask.GetMask("Default", "Terrain", "Walls");
				Physics.Raycast(Hips.position, -Vector3.up, out hipshitground, 0.5f, groundmask);

				var HipsPosition = Hips.position;
				HipsPosition.y = hipshitground.point.y;
				transform.position = HipsPosition;
				SetTransformRotationToBodyDirection();
			}
			transform.position = Hips.position;
		}

		//Wait to stable position
		if (State == RagdollState.WaitStablePosition)
		{
			enemy.timeBetweenShot = 1000000;

			Hips.parent = HipsParent;

			foreach (var Bone in bones)
			{
				Bone.StoredPosition = Bone.Transform.localPosition;
				Bone.StoredRotation = Bone.Transform.localRotation;
			}
			if(enemy.health > 0)
            {
				GetUp();
				State = RagdollState.BlendToAnim;
			} else
            {
				enemy.navMeshAgent.isStopped = true;
			}
			
		}


		RaycastHit hit;
		LayerMask mask = LayerMask.GetMask("Default", "Terrain", "Walls");
		if (Physics.Raycast(Hips.position, Hips.forward, out hit, 0.5f, mask))
		{
			GetUpFromBelly = true;
		}
		else
        {
			GetUpFromBelly = false;
		}
	}
	public void BlendRagdollToAnimator()
	{
		if (State == RagdollState.BlendToAnim)
		{
			foreach (var Bone in bones)
			{
				Bone.Transform.localPosition = Vector3.Slerp(Bone.Transform.localPosition, Bone.StoredPosition, BlendAmount);
				Bone.Transform.localRotation = Quaternion.Slerp(Bone.Transform.localRotation, Bone.StoredRotation, BlendAmount);
			}

			BlendAmount = Mathf.MoveTowards(BlendAmount, 0.0f, BlendSpeed * Time.deltaTime);

			if (BlendAmount <= 0)
			{
				State = RagdollState.Animated;
			}
		}
	}

	public void SetActiveRagdoll(bool Enabled, bool Inertia = default)
	{
		foreach (Rigidbody boneRigidbody in RagdollBones)
		{
			boneRigidbody.isKinematic = !Enabled;
		}
		RagdollEnabled = Enabled;
		animator.enabled = !Enabled;

		if (Inertia == true)
		{
			foreach (Rigidbody rb in RagdollBones)
			{
				rb.velocity = GetComponent<Rigidbody>().velocity;
				rb.angularVelocity = Vector3.zero;
				rb.angularDrag = RagdollDrag;
			}
		}
		if (Enabled == true)
		{
			rigidBody.isKinematic = true;
			rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
		}
		else
		{
			rigidBody.isKinematic = false;
			rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
		}

		enemy.GetComponent<CapsuleCollider>().enabled = enabled;
		//rigidbody.isKinematic = Enabled;
		//rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.y);
	}
	private void SetWaitStablePositionInvoked()
	{
		State = RagdollState.WaitStablePosition;
	}
	public void GetUp()
	{
		SetTransformRotationToBodyDirection();
		//Disable Ragdoll
		SetActiveRagdoll(false);

		//Animation
		BlendAmount = 1f;

		//Play Animation
		if (GetUpFromBelly)
		{
			animator.Play("Get Up From Belly", 0, 0);
            if (enemy.canSeePlayer)
            {
				animator.SetBool("Shoot", true);
				enemy.timeBetweenShot = timebtwShot;
			}
			else
            {
				animator.SetBool("Run", true);	
			}
			
		}
		else
		{
			animator.Play("Get Up From Back", 0, 0);
			if (enemy.canSeePlayer)
			{
				animator.SetBool("Shoot", true);
				enemy.timeBetweenShot = timebtwShot;
			}
			else
			{
				animator.SetBool("Run", true);
			}
		}
	}
	public void SetTransformRotationToBodyDirection()
	{
		transform.rotation = Quaternion.FromToRotation(transform.forward, BodyDirection()) * transform.rotation;
		Hips.rotation = Quaternion.FromToRotation(BodyDirection(), transform.forward) * Hips.rotation;
		transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
	}
	public Vector3 BodyDirection()
	{
		Vector3 ragdolldirection = Hips.position - Head.position;
		ragdolldirection.y = 0;
		if (GetUpFromBelly)
		{
			return -ragdolldirection.normalized;
		}
		else
		{
			return ragdolldirection.normalized;
		}
	}




#if UNITY_EDITOR
	[HideInInspector] private Camera MainCamera;
	private void OnDrawGizmos()
	{
		if (MainCamera == null)
		{
			MainCamera = Camera.current;
		}

		//BODY DIRECTION
		if (State == RagdollState.Ragdolled && ViewBodyDirection)
		{
			Handles.Label(Hips.position + MainCamera.transform.right * 1f, "Ragdoll Body Direction");
			if (GetUpFromBelly)
			{
				Handles.color = Color.cyan;
				Handles.ArrowHandleCap(0, Hips.position + MainCamera.transform.right * 1f, Quaternion.LookRotation(BodyDirection()), 0.5f, EventType.Repaint);
			}
			else
			{
				Handles.color = Color.white;
				Handles.ArrowHandleCap(0, Hips.position + MainCamera.transform.right * 1f, Quaternion.LookRotation(BodyDirection()), 0.5f, EventType.Repaint);
			}
		}

		if (AllBones != null)
		{
			//DRAW BONES OUTLINE
			if (ViewHumanBodyBones)
			{
				foreach (var CurrentBone in AllBones)
				{
					if (CurrentBone.transform.parent == null)
						continue;
					if (CurrentBone.transform.parent == transform)
						continue;
					float distparent = Vector3.Distance(CurrentBone.position, CurrentBone.transform.parent.position);
					Vector3 direction = CurrentBone.transform.parent.position - CurrentBone.position;
					if (State == RagdollState.Animated)
					{
						Handles.color = Color.yellow;
						Handles.DrawDottedLine(CurrentBone.position, CurrentBone.transform.parent.position, 0.3f);
						Gizmos.color = Color.red;
						Gizmos.DrawSphere(CurrentBone.position, 0.02f);
					}
					else
					{
						Handles.color = Color.grey;
						Handles.DrawDottedLine(CurrentBone.position, CurrentBone.transform.parent.position, 0.3f);
					}
				}
			}



			//DRAW PHYSICS
			if (ViewBodyPhysics)
			{
				foreach (var CurrentBone in RagdollBones)
				{
					if (CurrentBone.transform.parent == null)
						continue;
					if (CurrentBone.transform.parent == transform)
						continue;
					if (State == RagdollState.Ragdolled)
					{
						Color green = new Color(0, 1, 0, .5f);
						Gizmos.color = green;
						Gizmos.DrawSphere(CurrentBone.position + CurrentBone.transform.up * 0.2f, 0.05f);
						//Gizmos.color = green;
						//Gizmos.DrawWireSphere(CurrentBone.position + CurrentBone.transform.up * 0.2f, 0.05f);
						Gizmos.color = green;
						Gizmos.DrawLine(CurrentBone.position, CurrentBone.position + CurrentBone.transform.up * 0.2f);
					}
					else
					{
						Gizmos.color = Color.gray;
						Gizmos.DrawSphere(CurrentBone.position + CurrentBone.transform.up * 0.2f, 0.05f);
						Gizmos.DrawWireSphere(CurrentBone.position + CurrentBone.transform.up * 0.2f, 0.05f);
					}
				}
			}



		}
	}
#endif


}
