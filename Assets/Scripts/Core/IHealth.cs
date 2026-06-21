public interface IHealth
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public void ChangeHealth(float healthAmount);

    public void CheckRemainingHealth();

    public void KillCharacter();
}
