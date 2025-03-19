using Snake;

public class BerryFactory
{
    private Random random;
    
    public BerryFactory()
    {
        random = new Random();
    }
    
    // Metoda pro vytvoření specifického typu bobule
    public IBerry CreateBerry(BerryType type, int xPos, int yPos)
    {
        switch (type)
        {
            case BerryType.Regular:
                return new RegularBerry(xPos, yPos);
            case BerryType.Poison:
                return new PoisonBerry(xPos, yPos);
            default:
                throw new ArgumentException($"Unknown berry type: {type}");
        }
    }
    
    // Metoda pro vytvoření náhodné bobule na náhodné pozici
    public IBerry CreateRandomBerry(int maxWidth, int maxHeight)
    {
        int xPos = random.Next(1, maxWidth - 2);
        int yPos = random.Next(1, maxHeight - 2);
        
        // Vybrat náhodný typ
        BerryType type = random.Next(100) < 80 ? BerryType.Regular : BerryType.Poison;
        
        return CreateBerry(type, xPos, yPos);
    }
    
    // Metoda pro vytvoření konkrétního typu bobule na náhodné pozici
    public IBerry CreateRandomBerry(BerryType type, int maxWidth, int maxHeight)
    {
        int xPos = random.Next(1, maxWidth - 2);
        int yPos = random.Next(1, maxHeight - 2);
        
        return CreateBerry(type, xPos, yPos);
    }
}