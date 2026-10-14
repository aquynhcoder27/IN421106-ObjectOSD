namespace L001;

public class Warrior
{
    private string Name;
    private int Health;
    
    //Constructor
    public Warrior(string name, int health)
    {
        Name = name;
        Health = health;
    }
    
    //Behavior
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name} took {damage} damage! Remaining Health: {Health}");
    }

    public void Heal(int amount)
    {
        Health += amount;
        Console.WriteLine($"{Name} healed {amount} HP! Current Health: {Health}");
    }
}