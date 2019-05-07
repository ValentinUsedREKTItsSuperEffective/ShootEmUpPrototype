public class Weapons : IEquipment {
    
    protected IEquipment equipment;
    protected int damage;

    public Weapons(IEquipment equipment){
        this.equipment = equipment;
    }
        
    public override int GetDamage() {
        return equipment.GetDamage() + damage;
    }
}

public class BigRifle : Weapons {
    
    public BigRifle(IEquipment equipment) : base (equipment) {
        damage = 1;
    }
}

public class BiggerRiffle : Weapons {

    public BiggerRiffle(IEquipment equipment) : base (equipment) {
        damage = 2;
    }
}
