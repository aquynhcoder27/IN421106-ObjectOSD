using L001;

Warrior warrior1 = new Warrior("Arthur", 100);

Warrior warrior2 = new Warrior("Lancelot", 90);

Console.WriteLine(warrior1.Name);
Console.WriteLine(warrior1.Health);
Console.WriteLine(warrior2.Name);
Console.WriteLine(warrior2.Health);

warrior1.TakeDamage(40);
warrior1.Heal(20);