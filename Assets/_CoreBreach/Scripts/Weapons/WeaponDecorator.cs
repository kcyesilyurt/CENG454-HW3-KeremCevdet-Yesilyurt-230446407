public abstract class WeaponDecorator : IWeapon
{
    protected readonly IWeapon wrappedWeapon;

    protected WeaponDecorator(IWeapon wrappedWeapon)
    {
        this.wrappedWeapon = wrappedWeapon;
    }

    public virtual void Fire()
    {
        wrappedWeapon.Fire();
    }

    public virtual int GetDamage()
    {
        return wrappedWeapon.GetDamage();
    }

    public virtual float GetFireRate()
    {
        return wrappedWeapon.GetFireRate();
    }
}