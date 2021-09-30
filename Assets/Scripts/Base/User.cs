using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string userName;
    public long keyboardHP;
    public long dPc;
    public long sPc;
    public long totalClick;
    public long upgrader;
    public long stress;
    public List<Upgrader> upgraderList = new List<Upgrader>();
    public List<Upgrader> upgraderList2 = new List<Upgrader>();

}