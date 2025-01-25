import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import Header from '../components/common/Header';
import { CheckCircle, X } from 'lucide-react';
import { useOrder } from "../context/OrderProvider";

const CustomerOrder = () => {
  const { orderData, markAsDelivered } = useOrder();
  const { orderId } = useParams();
  const [selectedProduct, setSelectedProduct] = useState(null);

  const order = orderData.find(o => o.id === orderId);

  if (!order) {
    return <div>Order not found</div>;
  }

  const orderStatus = order.status;

  const handleProductClick = (product) => {
    setSelectedProduct(selectedProduct === product ? null : product);
  };

  return (
    <div className='flex-1 flex-col items-center justify-center min-h-96 w-full bg-blue-100'>
      <Header title={`Order : ${order.id}`} />
      <div className='relative space-y-4 m-7 w-full max-w-6xl bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border'>
        <h1 className='text-center text-4xl font-bold text-white mb-4'>Order Details</h1>
        <div className='mb-4 space-y-2'>
          <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Customer:</span> {order.customer}</p>
          <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Total:</span> ${order.total}</p>
          <p className='text-2xl text-white'><span className='font-semibold text-violet-300'>Date:</span> {order.date}</p>
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
        {selectedProduct && (
          <div className='max-h-48 overflow-y-scroll mt-4'>
            <h3 className='text-xl font-semibold text-violet-300 mb-2'>Product Details</h3>
            <table className='min-w-full border-spacing-y-4'>
              <thead>
                <tr className='text-gray-100'>
                  <th className='px-6 py-3 text-left text-sm font-medium uppercase'>Product Item ID</th>
                  <th className='px-6 py-3 text-left text-sm font-medium uppercase'>Warehouse</th>
                  <th className='px-6 py-3 text-left text-sm font-medium uppercase'>Location</th>
                </tr>
              </thead>
              <tbody>
                {selectedProduct.details.map((detail, index) => (
                  <tr key={index} className='text-gray-100'>
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
            orderStatus === "Delivered"
              ? "bg-green-100 text-green-800"
              : orderStatus === "Processing"
              ? "bg-yellow-100 text-yellow-800"
              : orderStatus === "Shipped"
              ? "bg-blue-100 text-blue-800"
              : "bg-red-100 text-red-800"
          }`}
        >
          {orderStatus}
        </span></p>
        <button
          onClick={() => {
            markAsDelivered(order.id);
          }}
          disabled={orderStatus === 'Delivered'}
          className={`absolute bottom-4 right-4 px-4 py-2 ${orderStatus !== 'Delivered' ? `bg-green-500 text-white font-semibold rounded-lg shadow-md hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-400 focus:ring-opacity-75` : `bg-gray-500 text-white font-semibold rounded-lg shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-opacity-75`}`}
        >
          Mark as Delivered
        </button>
      </div>
    </div>
  );
};

export default CustomerOrder;