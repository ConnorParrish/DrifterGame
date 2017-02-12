using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eOrientationMode { NODE = 0, TANGENT }

[AddComponentMenu("Splines/Spline Controller")]
[RequireComponent(typeof(SplineInterpolator))]
public class SplineController : MonoBehaviour
{
	public GameObject SplineRootHolder;
	public float Duration = 10;
	public eOrientationMode OrientationMode = eOrientationMode.NODE;
	public eWrapMode WrapMode = eWrapMode.ONCE;
	public bool AutoStart = true;
	public bool AutoClose = true;
	public bool HideOnExecute = true;
    public string DebugCardinalDirection = "S";


	SplineInterpolator mSplineInterp;
	public Transform[] mTransforms;
    List<GameObject> splineRoots = new List<GameObject>();
    List<Transform[]> trans = new List<Transform[]>();

    public int pathIndex = 1;

    void OnDrawGizmos()
	{
        for (int i = 0; i < SplineRootHolder.transform.childCount; i++)
        {
            if (!splineRoots.Contains(SplineRootHolder.transform.GetChild(i).gameObject))
            {
                splineRoots.Add(SplineRootHolder.transform.GetChild(i).gameObject);
            }

            trans.Add(GetTransforms(i));
            if (trans[i].Length < 2) 
                return;

            SplineInterpolator interp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;
            SetupSplineInterpolator(interp, trans[i]);
            interp.StartInterpolation(null, false, WrapMode);

            
            Vector3 prevPos = trans[i][0].position;
            for (int c = 1; c <= 100; c++)
            {
                float currTime = c * Duration / 100;
                Vector3 currPos = interp.GetHermiteAtTime(currTime);
                float mag = (currPos - prevPos).magnitude * 2;
                Gizmos.color = new Color(mag, 0, 0, 1);
                Gizmos.DrawLine(prevPos, currPos);
                prevPos = currPos;
            }
        }
    }

    public void SetPath(int pIndex)
    {
        pathIndex = pIndex;
    }


	void Start()
	{
		mSplineInterp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;
        for (int i = 0; i < SplineRootHolder.transform.childCount; i++)
        {
            if (!splineRoots.Contains(SplineRootHolder.transform.GetChild(i).gameObject))
            {
                splineRoots.Add(SplineRootHolder.transform.GetChild(i).gameObject);
            }
        }
        Debug.Log(splineRoots.Count);

        mTransforms = GetTransforms(0);

		if (HideOnExecute)
			DisableTransforms();

		if (AutoStart)
			FollowSpline();

        //this.enabled = false;
	}

	void SetupSplineInterpolator(SplineInterpolator interp, Transform[] trans)
	{ 
		interp.Reset();

		float step = (AutoClose) ? Duration / trans.Length :
			Duration / (trans.Length - 1);

		int c;
		for (c = 0; c < trans.Length; c++)
		{
			if (OrientationMode == eOrientationMode.NODE)
			{
				interp.AddPoint(trans[c].position, trans[c].rotation, step * c, new Vector2(0, 1));
			}
			else if (OrientationMode == eOrientationMode.TANGENT)
			{
				Quaternion rot;
				if (c != trans.Length - 1)
					rot = Quaternion.LookRotation(trans[c + 1].position - trans[c].position, trans[c].up);
				else if (AutoClose)
					rot = Quaternion.LookRotation(trans[0].position - trans[c].position, trans[c].up);
				else
					rot = trans[c].rotation;

				interp.AddPoint(trans[c].position, rot, step * c, new Vector2(0, 1));
			}
		}

		if (AutoClose)
			interp.SetAutoCloseMode(step * c);
	}


	/// <summary>
	/// Returns children transforms, sorted by name.
	/// </summary>
	Transform[] GetTransforms(int cardinalIndex)
	{
		if (splineRoots[cardinalIndex] != null)
		{

            List<Component> components = new List<Component>(splineRoots[cardinalIndex].GetComponentsInChildren(typeof(Transform)));
			List<Transform> transforms = components.ConvertAll(c => (Transform)c);

			transforms.Remove(splineRoots[cardinalIndex].transform);
			transforms.Sort(delegate(Transform a, Transform b)
			{
				return a.name.CompareTo(b.name);
			});

			return transforms.ToArray();
		}

		return null;
	}

	/// <summary>
	/// Disables the spline objects, we don't need them outside design-time.
	/// </summary>
	void DisableTransforms()
	{
		if (splineRoots != null)
		{
			splineRoots[0].SetActiveRecursively(false);
		}
	}


	/// <summary>
	/// Starts the interpolation
	/// </summary>
	public void FollowSpline()
	{
        //mTransforms = GetTransforms(pathIndex);

        Debug.Log(mTransforms.Length);
		if (mTransforms.Length > 0)
		{
			SetupSplineInterpolator(mSplineInterp, mTransforms);
			mSplineInterp.StartInterpolation(null, true, WrapMode);
		}
	}
}