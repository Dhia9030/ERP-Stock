import React, { useState, createContext, useContext, useEffect } from 'react';
import * as SignalR from '@microsoft/signalr';
import useSignalR from '../SignalR';
import { getToken } from '../utility/storage';

const purchaseContext = createContext();

const PurchaseProvider = ({ children }) => {
  

  const [purchaseData, setPurchaseData] = useState([]);

  
  useEffect(() => {
    const fetchPurchases = async () => {
      const token = getToken();
            if (!token) {
              console.log('User is not authenticated. Skipping fetch.');
              return;
            }
      try {
        const response = await fetch('http://localhost:5188/Test/getall buy order');
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        // Filter out orders with status different from 0 (pending) or 1 (processing) and format the data
        const filteredData = data
          .filter(order => order.Status === 0 || order.Status === 1)
          .map(order => ({
            id: order.OrderId,
            supplier: order.Supplier ? order.Supplier.Name : 'N/A',
            total: order.TotalAmount,
            status: order.Status,
            orderDate: new Date(order.CreationDate).toLocaleDateString(),
            executed: order.Status === 1, // Assuming status 1 means executed
            received: order.Status === 5, // Assuming status 5 means received
            products: order.OrderProducts ? order.OrderProducts.map(orderProduct => ({
              name: orderProduct.Product.Name,
              quantity: orderProduct.Quantity,
              price: orderProduct.Product.Price,
              details: [] // Details will be populated when marked as executed
            })) : []
          }));
        setPurchaseData(filteredData);
      } catch (error) {
        console.error('Failed to fetch data:', error);
      }
    };

    const intervalId = setInterval(fetchPurchases, 5000);
    return () => clearInterval(intervalId);

  }, []);


  const markAsExecuted = (purchaseId) => {
    setPurchaseData(prevData =>
      prevData.map(purchase =>
        purchase.id === purchaseId ? { ...purchase, executed: true } : purchase
      )
    );
  };

  const markAsReceived = (purchaseId) => {
    setPurchaseData(prevData =>
      prevData.map(purchase =>
        purchase.id === purchaseId ? { ...purchase, received: true } : purchase
      )
    );
  };

  return (
    <purchaseContext.Provider value={{ purchaseData, markAsExecuted, markAsReceived }}>
      {children}
    </purchaseContext.Provider>
  );
};

const usePurchase = () => {
  return useContext(purchaseContext);
};

export { usePurchase, PurchaseProvider };