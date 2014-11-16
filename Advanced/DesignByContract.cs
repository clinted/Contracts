
public class Program
{
	public void Main( string[] args )
	{
		// Initialize business services
		OrderManager.Initialize( TaxRegion.Domestic );
		//...

		// Do some work...
		//...
		OrderManager.PlaceOrder( customer, order );
		//...
	}
}

public enum TaxRegion
{
	Domestic,
	Foreign
}

public static class OrderService
{	
	private static SalesTaxProvider _salesTaxProvider = null;
	private static SalesTaxProvider SalesTaxProvider
	{
		get
		{
			return _salesTaxProvider;				
		}
	}
	
	public static void Initialize( TaxRegion taxRegion )
	{
		// Initialize proper sales tax provider based on specified tax region
		switch( taxRegion )
		{
			case TaxRegion.Domestic:
				_salesTaxProvider = new DomesticSalesTaxProvider();
				break;
				
			case TaxRegion.Foreign:
				_salesTaxProvider = new ForeignSalesTaxProvider();
				break;
				
			default:
				_salesTaxProvider = null;
		}
	}
	
	public static double CalculateSalesTax( Order order )
	{
		// Check preconditions
		Require.NotNull( order != null , "order" );
		//...

		// Make sure sales tax provider has been configured
		Assume.NotNull( OrderManager.SalesTaxProvider != null, "SalesTaxProvider has not been initialized." );

		// Leverage tax provider to calculate sales tax
		return OrderManager.SalesTaxProvider.CalculateSalesTax( order );
	}
	
	public static int PlaceOrder( Customer customer, Order order)
	{
		// Check preconditions
		Require.NotNull( customer != null, "customer" );
		Require.NotNull( order != null , "order" );
		//...
				
		// Committ order to database
		Transaction tx = DAO.GetTransaction();

		OrderDAO orderDAO = new OrderDAO(tx);
		int orderID = orderDAO.PlaceOrder( customer.Id, order );

		CustomerDAO customerDAO = new CustomerDAO( tx );
		customerDAO.UpdateLastOrderDate( order.CustomerID, DateTime.Now );


		DAO.CommitTransaction( tx );

		// Check post-conditions
		Verify.NotNull( order.DateOfPurchase != null, "DateOfPurchase not set on order." );
		//...
	}
}

