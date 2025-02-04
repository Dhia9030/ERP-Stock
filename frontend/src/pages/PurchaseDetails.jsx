import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import Header from '../components/common/Header';
import { CheckCircle, X } from 'lucide-react';

const PurchaseDetails = () => {
  const { purchaseId } = useParams();
  const [purchase, setPurchase] = useState(null);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [isReceived, setIsReceived] = useState(false);

  useEffect(() => {
    const fetchPurchaseDetails = async () => {
      try {
        const response = await fetch(`http://localhost:5188/Test/detailof an order?id=${purchaseId}`);
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setPurchase({
          id: data.OrderId,
          supplier: data.Supplier ? data.Supplier.Name : 'N/A',
          total: data.TotalAmount,
          status: data.Status,
          orderDate: new Date(data.CreationDate).toLocaleDateString(),
          executed: data.Status === 1, // Assuming status 1 means executed
          received: data.Status === 5, // Assuming status 5 means received
          products: data.OrderProducts ? data.OrderProducts.map(orderProduct => ({
            id: orderProduct.Product.ProductId, // Correctly accessing ProductId
            name: orderProduct.Product.Name,
            quantity: orderProduct.Quantity,
            price: orderProduct.Product.Price,
            details: [] // Details will be populated when marked as executed
          })) : []
        });
        setIsReceived(data.Status === 5);
      } catch (error) {
        console.error('Failed to fetch purchase details:', error);
      }
    };

    fetchPurchaseDetails();
  }, [purchaseId]);

  const fetchProductDetails = async (orderId, productId) => {
    try {
      const url = `http://localhost:5188/Test/get%20ItemFrom%20A%20specific%20Buy%20order%20and%20a%20product?OrderId=${orderId}&ProductId=${productId}`;
      console.log('Fetching product details from URL:', url);
      const response = await fetch(url);
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const data = await response.json();
      console.log('Product details:', data);
      return data.items.map(item => ({
        productItemId: item.ItemId,
        warehouse: item.warehouseName,
        location: item.locationName
      }));
    } catch (error) {
      console.error('Failed to fetch product details:', error);
      return [];
    }
  };

  const handleProductClick = async (product) => {
    if (selectedProduct && selectedProduct.id === product.id) {
      setSelectedProduct(null);
    } else {
      const details = await fetchProductDetails(purchaseId, product.id);
      setSelectedProduct({ ...product, details });
    }
  };

  const handleMarkAsExecuted = async () => {
    try {
      const response = await fetch(`http://localhost:5188/api/Order/executeBuyOrder/${purchaseId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: ''
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      console.log('Order executed successfully');

      // Update the executed status
      setPurchase(prevPurchase => ({ ...prevPurchase, executed: true }));

      // Fetch the details for each product from the new endpoint
      const updatedProducts = await Promise.all(purchase.products.map(async (product) => {
        console.log('Fetching details for product:', product);
        const details = await fetchProductDetails(purchaseId, product.id);
        return { ...product, details };
      }));

      setPurchase(prevPurchase => ({ ...prevPurchase, products: updatedProducts }));
      console.log('Updated products:', updatedProducts);
    } catch (error) {
      console.error('Failed to execute order:', error);
    }
  };

  if (!purchase) {
    return <div>Purchase not found</div>;
  }

  const purchaseStatus = isReceived ? "Received" : (purchase.executed ? "Executed" : "Not Executed");

  const handleMarkAsReceived = async () => {
    try {
      const response = await fetch(`http://localhost:5188/api/Order/ConfirmBuy/${purchaseId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: ''
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      console.log('Order marked as received successfully');

      // Update the received status
      setIsReceived(true);
    } catch (error) {
      console.error('Failed to mark order as received:', error);
    }
  };

  return (
    <div className='overflow-y-scroll flex-1 flex-col items-center justify-center min-h-96 w-full bg-blue-100'>
      <Header title={`Purchase : ${purchase.id}`} />
      <div className='relative space-y-4 m-7 w-full max-w-6xl bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border'>
        <h1 className='text-center text-4xl font-bold text-white mb-4'>Purchase Details</h1>
        <div className='mb-4 space-y-2'>
          <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Supplier:</span> {purchase.supplier}</p>
          <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Total:</span> ${purchase.total}</p>
          <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Date:</span> {purchase.orderDate}</p>
        </div>
        <h2 className='text-2xl font-semibold text-violet-300 mb-2'>Products</h2>
        <div className='max-h-48 overflow-auto'>
          <ul className='list-none list-inside text-gray-300 space-y-1'>
            {purchase.products.map((product, index) => (
              <li key={index} className='flex text-2xl mb-1 justify-around cursor-pointer' onClick={() => handleProductClick(product)}>
                <div>
                  <CheckCircle className='inline-block mr-2 text-violet-300' />
                  <span className='font-semibold text-2xl text-white'>{product.name}: <span className='font-extralight text-sm'>click for details</span></span>
                </div>
                <div>
                  {product.quantity} <X className='inline-block mx-1 text-white' /> ${product.price}
                </div>
              </li>
            ))}
          </ul>
        </div>
        {selectedProduct && selectedProduct.details && (
          <div className='max-h-48 overflow-y-scroll mt-4'>
            <h3 className='text-xl font-semibold text-violet-300 mb-2'><span className='underline text-2xl'>{selectedProduct.name}</span> Details</h3>
            <table className='min-w-full border-spacing-y-4'>
              <thead>
                <tr className='text-gray-100 bg-slate-900'>
                  <th className='px-6 py-3 text-left text-sm font-medium uppercase '>Product Item ID</th>
                  <th className='px-6 py-3 text-left text-sm font-medium uppercase'>Warehouse</th>
                  <th className='px-6 py-3 text-left text-sm font-medium uppercase'>Location</th>
                </tr>
              </thead>
              <tbody>
                {selectedProduct.details.map((detail, index) => (
                  <tr key={index} className='text-gray-100 bg-slate-900'>
                    <td className='px-6 py-4 text-sm'>{detail.productItemId}</td>
                    <td className='px-6 py-4 text-sm'>{detail.warehouse}</td>
                    <td className='px-6 py-4 text-sm'>{detail.location}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
        <p className='text-2xl text-violet-300'><span className='font-semibold text-violet-300'>Status:</span> <span
          className={`ml-5 p-4 inline-flex text-xl leading-5 font-semibold rounded-full ${
            isReceived
              ? "bg-blue-100 text-blue-800"
              : (purchase.executed ? "bg-green-100 text-green-800" : "bg-yellow-100 text-yellow-800")
          }`}
        >
          {purchaseStatus}
        </span></p>
        <button
          onClick={handleMarkAsExecuted}
          disabled={purchase.executed || isReceived}
          className={`absolute bottom-4 right-52 px-4 py-2 ${!purchase.executed && !isReceived ? `bg-green-500 text-white font-semibold rounded-lg shadow-md hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-400 focus:ring-opacity-75` : `bg-gray-500 text-white font-semibold rounded-lg shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-opacity-75`}`}
        >
          Mark as Executed
        </button>
        <button
          onClick={handleMarkAsReceived}
          disabled={!purchase.executed || isReceived}
          className={`absolute bottom-4 right-4 px-4 py-2 ${purchase.executed && !isReceived ? `bg-blue-500 text-white font-semibold rounded-lg shadow-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-400 focus:ring-opacity-75` : `bg-gray-500 text-white font-semibold rounded-lg shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-opacity-75`}`}
        >
          Mark as Received
        </button>
      </div>
    </div>
  );
};

export default PurchaseDetails;