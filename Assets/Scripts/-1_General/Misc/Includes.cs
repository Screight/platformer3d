public enum ANIMATIONS { LOCOMOTION, FALL_TO_LANDIING, JUMP, JUMP_2, JUMP_3, LAND, FALLING, EMPTY, LAST_NO_USE }
public struct JumpInfo
{
    public float gravity;
    public float initialSpeed;
    public ANIMATIONS animation;
}