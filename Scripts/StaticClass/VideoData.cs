using System.Collections.Generic;
using UnityEngine.UI;

public static class VideoData
{
    public readonly static List<string> videoType = new List<string>
    {
        {"Alphabet" }, {"Counting" }, {"Story" }
    };
    

    public static Dictionary<string, List<string>> vData = new Dictionary<string, List<string>> {
    {"1",  letterSound },
    {"2", earlyCounting }
    };
    public static List<string> letterSound = new List<string>
    {
        {"https://www.youtube.com/watch?v=IDnItUhSqdw" },
        {"https://www.youtube.com/watch?v=DsI-qsP-Cgk" },
        {"https://www.youtube.com/watch?v=SkF2HvGtqNw" },
        {"https://www.youtube.com/watch?v=HKUQttsgYpU" },
        {"https://www.youtube.com/watch?v=vVPoJjhnHiE" },
        {"https://www.youtube.com/watch?v=cL9BU4Ch4r0" },
        {"https://www.youtube.com/watch?v=6AXFJDJb3VA" },
        {"https://www.youtube.com/watch?v=S7G0kslVr4c" },
        {"https://www.youtube.com/watch?v=oAsMaoF8szo" },
        {"https://www.youtube.com/watch?v=3rbIUCUPFCI" },
        {"https://www.youtube.com/watch?v=RNUzyFrLmRE" },
        {"https://www.youtube.com/watch?v=iE4H8OPelYU" },
        {"https://www.youtube.com/watch?v=JVGW_VRpvEk" },
        {"https://www.youtube.com/watch?v=E7FlGWGeCqw" },
        {"https://www.youtube.com/watch?v=qfW1Wi_klAM" },
        {"https://www.youtube.com/watch?v=7_aeLaPdY3Y" },
        {"https://www.youtube.com/watch?v=7wIqGF3pgpQ" },
        {"https://www.youtube.com/watch?v=u1oV88JCf9A" },
        {"https://www.youtube.com/watch?v=EZPv0RDMy5g" },
        {"https://www.youtube.com/watch?v=-wwxHrgZRUI" },
        {"https://www.youtube.com/watch?v=woYx3-CrTcQ" },
        {"https://www.youtube.com/watch?v=l1qtjetBUMo" },
        {"https://www.youtube.com/watch?v=ze5DRQ5pmU8" },
        {"https://www.youtube.com/watch?v=y4sF6aDX3SQ" },
        {"https://www.youtube.com/watch?v=xqmsWP5Nv_k" },
        {"https://www.youtube.com/watch?v=gLlGHH_tFac" },//Repeat
        {"https://www.youtube.com/watch?v=IDnItUhSqdw" },
        {"https://www.youtube.com/watch?v=DsI-qsP-Cgk" },
        {"https://www.youtube.com/watch?v=SkF2HvGtqNw" },
        {"https://www.youtube.com/watch?v=HKUQttsgYpU" },
        {"https://www.youtube.com/watch?v=vVPoJjhnHiE" },
        {"https://www.youtube.com/watch?v=cL9BU4Ch4r0" },
        {"https://www.youtube.com/watch?v=6AXFJDJb3VA" },
        {"https://www.youtube.com/watch?v=S7G0kslVr4c" },
        {"https://www.youtube.com/watch?v=oAsMaoF8szo" },
        {"https://www.youtube.com/watch?v=3rbIUCUPFCI" },
        {"https://www.youtube.com/watch?v=RNUzyFrLmRE" },
        {"https://www.youtube.com/watch?v=iE4H8OPelYU" },
        {"https://www.youtube.com/watch?v=JVGW_VRpvEk" },
        {"https://www.youtube.com/watch?v=E7FlGWGeCqw" },
        {"https://www.youtube.com/watch?v=qfW1Wi_klAM" },
        {"https://www.youtube.com/watch?v=7_aeLaPdY3Y" },
        {"https://www.youtube.com/watch?v=7wIqGF3pgpQ" },
        {"https://www.youtube.com/watch?v=u1oV88JCf9A" },
        {"https://www.youtube.com/watch?v=EZPv0RDMy5g" },
        {"https://www.youtube.com/watch?v=-wwxHrgZRUI" },
        {"https://www.youtube.com/watch?v=woYx3-CrTcQ" },
        {"https://www.youtube.com/watch?v=l1qtjetBUMo" },
        {"https://www.youtube.com/watch?v=ze5DRQ5pmU8" },
        {"https://www.youtube.com/watch?v=y4sF6aDX3SQ" },
        {"https://www.youtube.com/watch?v=xqmsWP5Nv_k" },
        {"https://www.youtube.com/watch?v=gLlGHH_tFac" }
    };
    public readonly static List<string> Numerical = new List<string>
    {
        {"https://www.youtube.com/watch?v=LzHjfyWrU8s" },
        {"https://www.youtube.com/watch?v=bsAhkzFY3yE" },
        {"https://www.youtube.com/watch?v=xCrRJjS2mrM" },
        {"https://www.youtube.com/watch?v=GfA6EfPNuco" },
        {"https://www.youtube.com/watch?v=W5WecuyuX6g" },
        {"https://www.youtube.com/watch?v=EKG_YNDgI8U" },
        {"https://www.youtube.com/watch?v=EoFhIwvw6lg" },
        {"https://www.youtube.com/watch?v=vpShR8q-A60" },
        {"https://www.youtube.com/watch?v=HlKDZHD1COI" },
        {"https://www.youtube.com/watch?v=i24Fs6xMwT8" }
    };


    public readonly static List<string> earlyCounting = new List<string>
    {
        {"https://www.youtube.com/watch?v=hKEVDFIiqg4&ab_channel=KidsAcademy" },
        {"https://www.youtube.com/watch?v=hKEVDFIiqg4&ab_channel=KidsAcademy" }
    };
    public readonly static List<string> story = new List<string>
    {
        {"https://www.youtube.com/watch?v=hKEVDFIiqg4&ab_channel=KidsAcademy" },
        {"https://www.youtube.com/watch?v=hKEVDFIiqg4&ab_channel=KidsAcademy" }
    };

    public readonly static List<string> GeometryShape = new List<string>
    {
        {"https://www.youtube.com/watch?v=RbOtwaMqC8w" }
    };

}


public class VideoDataModel
{
    public string VideoType;
    public string VideoUrl;
    public string VideoTitle;
    public Image VideoThumbnail;
}  