import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import Header from '../components/common/Header';
import { CheckCircle, X } from 'lucide-react';
import { useOrder } from '../context/OrderProvider';

const CustomerOrder = () => {
  const { orderId } = useParams();
  const { markAsProcessing, markAsDelivered, markAsCancelled } = useOrder();
  const [order, setOrder] = useState(null);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [total, setTotal] = useState(null);

  useEffect(() => {
    const fetchOrderDetails = async () => {
      try {
        const response = await fetch(`http://localhost:5188/Test/detailof an order?id=${orderId}`);
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setOrder({
          id: data.OrderId,
          customer: data.Client ? `${data.Client.FirstName} ${data.Client.LastName}` : 'N/A',
          discount: data.DiscountPercentage,
          status: data.Status,
          orderDate: new Date(data.CreationDate).toLocaleDateString(),
          products: data.OrderProducts ? data.OrderProducts.map(orderProduct => ({
            id: orderProduct.Product.ProductId,
            name: orderProduct.Product.Name,
            quantity: orderProduct.Quantity,
            price: orderProduct.Product.Price,
            details: [] // Details will be populated when needed
          })) : []
        });
      } catch (error) {
        console.error('Failed to fetch order details:', error);
      }
    };

    fetchOrderDetails();
  }, [orderId]);

  useEffect(() => {
    const fetchProductDetailsAndCalculateTotal = async () => {
      if (order && (order.status === 1 || order.status === 2)) {
        const updatedProducts = await Promise.all(order.products.map(async (product) => {
          const details = await fetchProductDetails(order.id, product.id);
          return { ...product, details };
        }));

        const totalAmount = updatedProducts.reduce((sum, product) => sum + (product.quantity * product.price), 0);
        const discountedTotal = totalAmount * (1 - order.discount / 100);

        setOrder(prevOrder => ({ ...prevOrder, products: updatedProducts }));
        setTotal(discountedTotal);
      }
    };

    fetchProductDetailsAndCalculateTotal();
  }, [order?.status]);

  const fetchProductDetails = async (orderId, productId) => {
    try {
      const url = `http://localhost:5188/Test/get ItemFrom A specific Sell order and a product?OrderId=${orderId}&ProductId=${productId}`;
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

  const handleMarkAsProcessing = async () => {
    try {
      await markAsProcessing(order.id);
      console.log('Order marked as processing successfully');
      setOrder(prevOrder => ({ ...prevOrder, status: 1 }));
    } catch (error) {
      console.error('Failed to mark order as processing:', error);
    }
  };

  const handleMarkAsDelivered = async () => {
    try {
      await markAsDelivered(order.id);
      console.log('Order marked as delivered successfully');
      setOrder(prevOrder => ({ ...prevOrder, status: 2 }));
    } catch (error) {
      console.error('Failed to mark order as delivered:', error);
    }
  };

  const handleMarkAsCancelled = async () => {
    try {
      await markAsCancelled(order.id);
      console.log('Order marked as cancelled successfully');
      setOrder(prevOrder => ({ ...prevOrder, status: 3 }));
    } catch (error) {
      console.error('Failed to mark order as cancelled:', error);
    }
  };

  const handleProductClick = async (product) => {
    if (selectedProduct === product) {
      setSelectedProduct(null);
    } else {
      const details = await fetchProductDetails(order.id, product.id);
      console.log('Fetched details for product:', product.name, details);
      setSelectedProduct({ ...product, details });
    }
  };

  if (!order) {
    return <div>Order not found</div>;
  }

  const orderStatus = order.status === 2 ? "Delivered" : (order.status === 1 ? "Processing" : (order.status === 3 ? "Cancelled" : "Pending"));

  return (
    <div className='flex-1 flex-col items-center justify-center min-h-[700px] w-full bg-blue-100'>
      <Header title={`Order : ${order.id}`} />
      <div className='relative space-y-4 m-7 w-full min-h-[500px] max-w-6xl bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border'>
        <h1 className='text-center text-4xl font-bold text-white mb-4'>Order Details</h1>
        <div className='mb-4 space-y-2'>
          <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Customer:</span> {order.customer}</p>
          {total !== null && <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Total:</span> <span className='text-green-500'>${total.toFixed(2)}</span></p>}
          <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Date:</span> {order.orderDate}</p>
        </div>
        <h2 className='text-2xl font-semibold text-violet-300 mb-2'>Products</h2>
        <div className='max-h-48 overflow-auto'>
          <ul className='list-none list-inside text-gray-300 space-y-1'>
            {order.products.map((product, index) => (
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
            order.status === 2
              ? "bg-green-100 text-green-800"
              : (order.status === 1 ? "bg-yellow-100 text-yellow-800" : (order.status === 3 ? "bg-red-100 text-red-800" : "bg-blue-100 text-blue-800"))
          }`}
        >
          {orderStatus}
        </span></p>
        <button
          onClick={handleMarkAsProcessing}
          disabled={order.status !== 0}
          className={`absolute bottom-4 right-52 px-4 py-2 ${order.status === 0 ? `bg-yellow-500 text-white font-semibold rounded-lg shadow-md hover:bg-yellow-700 focus:outline-none focus:ring-2 focus:ring-yellow-400 focus:ring-opacity-75` : `bg-gray-500 text-white font-semibold rounded-lg shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-opacity-75`}`}
        >
          Mark as Processing
        </button>
        <button
          onClick={handleMarkAsDelivered}
          disabled={order.status !== 1}
          className={`absolute bottom-4 right-4 px-4 py-2 ${order.status === 1 ? `bg-green-500 text-white font-semibold rounded-lg shadow-md hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-400 focus:ring-opacity-75` : `bg-gray-500 text-white font-semibold rounded-lg shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-opacity-75`}`}
        >
          Mark as Delivered
        </button>
        <button
          onClick={handleMarkAsCancelled}
          disabled={order.status !== 0}
          className={`absolute bottom-4 right-[450px] px-4 py-2 ${order.status === 0 ? `bg-red-500 text-white font-semibold rounded-lg shadow-md hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-400 focus:ring-opacity-75` : `bg-gray-500 text-white font-semibold rounded-lg shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-opacity-75`}`}
        >
          Cancel
        </button>
      </div>
    </div>
  );
};

export default CustomerOrder;