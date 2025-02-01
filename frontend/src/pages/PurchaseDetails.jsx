import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import Header from '../components/common/Header';
import { CheckCircle, X } from 'lucide-react';
import { usePurchase } from '../context/PurchaseProvider';

const PurchaseDetails = () => {
  const { purchaseData, markAsExecuted, markAsReceived } = usePurchase();
  const { purchaseId } = useParams();
  console.log("orderId:", purchaseId);

  const purchase = purchaseData.find(p => p.id === purchaseId);
  const [selectedProduct, setSelectedProduct] = useState(purchase ? purchase.products[0] : null);
  const [isReceived, setIsReceived] = useState(purchase ? purchase.received : false);

  if (!purchase) {
    return <div>Purchase not found</div>;
  }

  const purchaseStatus = isReceived ? "Received" : (purchase.executed ? "Executed" : "Not Executed");

  const handleProductClick = (product) => {
    setSelectedProduct(selectedProduct === product ? null : product);
  };

  const handleMarkAsReceived = () => {
    markAsReceived(purchase.id);
    setIsReceived(true);
  };

  return (
    <div className='flex-1 flex-col items-center justify-center min-h-96 w-full bg-blue-100'>
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
        {selectedProduct && (
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
          onClick={() => {
            markAsExecuted(purchase.id);
          }}
          disabled={purchase.executed}
          className={`absolute bottom-4 right-52 px-4 py-2 ${!purchase.executed ? `bg-green-500 text-white font-semibold rounded-lg shadow-md hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-400 focus:ring-opacity-75` : `bg-gray-500 text-white font-semibold rounded-lg shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-opacity-75`}`}
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