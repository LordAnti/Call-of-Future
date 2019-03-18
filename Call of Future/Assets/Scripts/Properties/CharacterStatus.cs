using UnityEngine;

[CreateAssetMenu(menuName = "Character/Status")]
public class CharacterStatus : ScriptableObject
{
    public bool isSit;
    public bool isSprint;
    public bool isPistol;
    public bool isRifle;
    public bool isShotgun;
    public bool isJump;
    public bool isFire;
    public int Experience;
    public int Money;
}