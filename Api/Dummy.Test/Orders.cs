using Moq;

namespace Dummy.Test;

[TestClass]
public class Orders
{
    /// <summary>
    /// Write a test that edits an existing order’s title AND quantity.
    /// You may create the order using a mutation or directly in the DB.
    /// Assert the order’s values change, and side effects apply correctly.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    [TestMethod]
    public void CanEditOrderTitleAndQuantity()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Attempting to delete an order that still has an associated item should produce an error.
    /// Assert that:
    /// - The mutation fails
    /// - The order still exists
    /// - The item still exists
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    [TestMethod]
    public void CantDeleteOrderWithAnItem()
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Use Moq to force the validator to return false for titles containing whitespace.
    /// Assert the mutation produces an error, and no data is changed.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [TestMethod]
    public Task CreateOrderFailsWhenTitleHasWhiteSpace()
    {
        var validator = new Mock<IOrderValidator>();
        validator.Setup(v => v.ValidateWhiteSpace(It.IsAny<string>()))
            .Returns(true);

        validator.Setup(v => v.ValidateTitleLength(It.IsAny<string>()))
            .Returns(true);


        
        throw new NotImplementedException();
    }

}