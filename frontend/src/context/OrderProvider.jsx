import React, { useState, createContext, useContext, useEffect } from 'react';
import * as SignalR from '@microsoft/signalr';
import useSignalR from '../SignalR';
import { getToken } from '../utility/storage';


const orderContext = createContext();

const OrderProvider = ({ children }) => {
  

  //fetching orders

  const [orderData, setOrderData] = useState([]);
  useEffect(() => {
    const fetchOrderData = async () => {
      const token = getToken();
      if (!token) {
        console.log('User is not authenticated. Skipping fetch.');
        return;
      }
      try {
        const response = await fetch('http://localhost:5188/Test/getall sells order',
          // {
          //   headers: {
          //     'Authorization': `Bearer ${token}`
          //   }
          // }
        );
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        const transformedData = data.map(order => {
          const totalAmount = order.OrderProducts ? order.OrderProducts.reduce((sum, orderProduct) => sum + (orderProduct.Quantity * orderProduct.Product.Price), 0) : 0;
          const discountedTotal = totalAmount * (1 - order.DiscountPercentage / 100);
          return {
            id: order.OrderId,
            customer: `${order.Client.FirstName} ${order.Client.LastName}`,
            total: discountedTotal,
            status: order.Status === 0 ? "Pending" : order.Status === 1 ? "Processing" : order.Status === 2 ? "Delivered" : "Cancelled",
            date: new Date(order.CreationDate).toLocaleDateString(),
            products: order.OrderProducts || []
          };
        });
        setOrderData(transformedData);
      } catch (error) {
        console.error('Error fetching order data:', error);
      }
    };

    fetchOrderData();
  }, []);



  const markAsProcessing = async (orderId) => {
    try {
      const response = await fetch(`http://localhost:5188/api/Order/executeSellOrder/${orderId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: ''
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      setOrderData(prevData =>
        prevData.map(order =>
          order.id === orderId ? { ...order, status: 'Processing' } : order
        )
      );
    } catch (error) {
      console.error('Failed to mark order as processing:', error);
    }
  };

  const markAsDelivered = async (orderId) => {
    try {
      const response = await fetch(`http://localhost:5188/api/Order/ConfirmSale/${orderId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: ''
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      setOrderData(prevData =>
        prevData.map(order =>
          order.id === orderId ? { ...order, status: 'Delivered' } : order
        )
      );
    } catch (error) {
      console.error('Failed to mark order as delivered:', error);
    }
  };

  const markAsCancelled = async (orderId) => {
    try {
      const response = await fetch(`http://localhost:5188/api/Order/markAsCancelled/${orderId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: ''
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      setOrderData(prevData =>
        prevData.map(order =>
          order.id === orderId ? { ...order, status: 'Cancelled' } : order
        )
      );
    } catch (error) {
      console.error('Failed to mark order as cancelled:', error);
    }
  };


  return (
    <orderContext.Provider value={{ orderData,markAsCancelled,markAsProcessing, markAsDelivered }}>
      {children}
    </orderContext.Provider>
  );
};

const useOrder = () => {
  return useContext(orderContext);
};

export { useOrder, OrderProvider };