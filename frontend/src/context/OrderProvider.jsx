import React, { useState, createContext, useContext, useEffect } from 'react';
import * as SignalR from '@microsoft/signalr';
import useSignalR from '../SignalR';


const orderContext = createContext();

const OrderProvider = ({ children }) => {

  //fetching orders

  const [orderData, setOrderData] = useState([]);
  useEffect(() => {
    
    const fetchOrderData = async () => {
      try {
        const response = await fetch('http://localhost:5188/Test/getall sells order');
        const data = await response.json();
        const transformedData = data.map(order => ({
          id: `ORD${order.OrderId.toString().padStart(3, '0')}`,
          customer: `${order.Client.FirstName} ${order.Client.LastName}`,
          total: order.TotalAmount,
          status: order.Status === 0 ? "Pending" : order.Status === 1 ? "Processing" : order.Status === 2 ? "Shipped" : "Delivered",
          date: new Date(order.CreationDate).toLocaleDateString(),
          products: order.OrderProducts || []
        }));
        setOrderData(transformedData);
      } catch (error) {
        console.error('Error fetching order data:', error);
      }
    };

    fetchOrderData();
   
  }, []);




  const markAsProcessing = (orderId) => {
    // setOrderStatus(prevData => {
    //   return prevData.map(order => order.id === orderId ? { ...order, status: 'Processing' } : order);

    // });
    // const connection = new SignalR.HubConnectionBuilder()
    //   .withUrl("https://localhost:5001/orderhub")
    //   .configureLogging(SignalR.LogLevel.Information)
    //   .build();

    // connection.start()
    //   .then(() => {
    //     console.log("Connected to SignalR hub for fetching details");

    //     connection.invoke("GetOrderDetails", orderId)
    //       .then(details => {
    //         setOrders(prevOrders => prevOrders.map(order => 
    //           order.id === orderId ? { ...order, products: details } : order
    //         ));
    //       })
    //       .catch(err => console.error(err.toString()));
    //   })
    //   .catch(err => console.error("Error connecting to SignalR hub:", err));

    // return () => {
    //   connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    // };
  };

  const markAsDelivered = (orderId) => {
    // setOrderStatus(prevData => {
    //   return prevData.map(order => order.id === orderId ? { ...order, status: 'Delivered' } : order);
    // });
    // const connection = new SignalR.HubConnectionBuilder()
    //   .withUrl("https://localhost:5001/orderhub")
    //   .configureLogging(SignalR.LogLevel.Information)
    //   .build();

    // connection.start()
    //   .then(() => {
    //     console.log("Connected to SignalR hub for marking as delivered");

    //     connection.invoke("MarkOrderAsDelivered", orderId)
    //       .catch(err => console.error(err.toString()));
    //   })
    //   .catch(err => console.error("Error connecting to SignalR hub:", err));

    // return () => {
    //   connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    // };
  };
  const markAsCancelled = (orderId) => {
    // setOrderStatus(prevData => {
    //   return prevData.map(order => order.id === orderId ? { ...order, status: 'Cancelled' } : order);
    // });
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