using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsInformation : MonoBehaviour
{
    public int KidId;
    public string KidName;
    public string KidAge;
    public string KidAvatar;
    public string KidParent;
    public int CourseCompletedId;

    public void SetInformation(int kidid, string kidname, string kidage, string kidavatar, string parent, int courseId)
    {
        KidId = kidid;
        KidName = kidname;
        KidAge = kidage;
        KidAvatar = kidavatar;
        KidParent = parent;
        CourseCompletedId = courseId;
    }
}
