
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
		Contract.Precondition( order != null , "Invalid parameter: order cannot null" );
		//...

		// Make sure sales tax provider has been configured
		Contract.Invariant( OrderManager.SalesTaxProvider != null, "SalesTaxProvider has not been initialized." );

		// Leverage tax provider to calculate sales tax
		return OrderManager.SalesTaxProvider.CalculateSalesTax( order );
	}
	
	public static int PlaceOrder( Customer customer, Order order)
	{
		// Check preconditions
		Contract.Precondition( customer != null, "Invalid parameter: customer cannot be null." );
		Contract.Precondition( order != null , "Invalid parameter: order cannot null" );
		//...
				
		// Committ order to database
		Transaction tx = DAO.GetTransaction();

		OrderDAO orderDAO = new OrderDAO(tx);
		int orderID = orderDAO.PlaceOrder( customer.Id, order );

		CustomerDAO customerDAO = new CustomerDAO( tx );
		customerDAO.UpdateLastOrderDate( order.CustomerID, DateTime.Now );


		DAO.CommitTransaction( tx );

		// Check post-conditions
		Contract.Postcondition( order.DateOfPurchase != null, "DateOfPurchase not set on order." );
		//...
	}
}

