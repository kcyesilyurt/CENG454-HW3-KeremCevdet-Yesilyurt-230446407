public class FireRateDecorator : WeaponDecorator
{
    private readonly float fireRateMultiplier;

    public FireRateDecorator(IWeapon wrappedWeapon, float fireRateMultiplier) : base(wrappedWeapon)
    {
        this.fireRateMultiplier = fireRateMultiplier;
    }

    public override float GetFireRate()
    {
        return wrappedWeapon.GetFireRate() * fireRateMultiplier;
    }
}