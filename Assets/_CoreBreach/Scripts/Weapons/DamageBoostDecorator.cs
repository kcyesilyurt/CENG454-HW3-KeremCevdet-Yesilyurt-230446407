public class DamageBoostDecorator : WeaponDecorator
{
    private readonly int bonusDamage;

    public DamageBoostDecorator(IWeapon wrappedWeapon, int bonusDamage) : base(wrappedWeapon)
    {
        this.bonusDamage = bonusDamage;
    }

    public override int GetDamage()
    {
        return wrappedWeapon.GetDamage() + bonusDamage;
    }
}