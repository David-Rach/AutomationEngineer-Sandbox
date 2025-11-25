public interface IOrderValidator
{
    bool ValidateTitleLength(string title);
    bool ValidateWhiteSpace(string title);
}

public class OrderValidator : IOrderValidator
{
    public bool ValidateTitleLength(string title)
    {
        return title.Length <= 20;
    }

    /// <summary>
    /// Always true for MOQ purposes. 
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public bool ValidateWhiteSpace(string title)
    {
        return true;
    }
}