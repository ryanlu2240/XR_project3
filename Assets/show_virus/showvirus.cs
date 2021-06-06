using UnityEngine;
using Wikitude;

public class showvirus : SampleController
{
    public GameObject myPrefab;

    protected override void Start() {

    }

    public void OnObjectRecognized(ImageTarget recognizedTarget) {
        
	    // GameObject newAugmentation = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject newAugmentation = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);;
	    // Set the newAugmentation to be a child of the Drawable.
	    newAugmentation.transform.parent = recognizedTarget.Drawable.transform;
        // newAugmentation.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

	    // Position the augmentation relative to the Drawable by using the localPosition.
	    newAugmentation.transform.localPosition = Vector3.zero;
    }

    protected override void Update() {

    }
}
