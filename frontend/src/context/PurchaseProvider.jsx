import React, { useState, createContext, useContext, useEffect } from 'react';
import * as SignalR from '@microsoft/signalr';

const purchaseContext = createContext();

const PurchaseProvider = ({ children }) => {
  const initialPurchaseData = [
    {
      id: "PUR001",
      supplier: "Supplier A",
      total: 5000,
      orderDate: "2024-12-05",
      executed: true,
      received: false,
      products: [
        {
          name: "Phone",
          quantity: 50,
          price: 100,
          details: [
            { productItemId: "P001", warehouse: "Main Warehouse", location: "Aisle1" }
          ]
        }
      ]
    },
    {
      id: "PUR002",
      supplier: "Supplier B",
      total: 9000,
      orderDate: "2024-12-10",
      executed: false,
      received: false,
      products: [
        {
          name: "Laptop",
          quantity: 30,
          price: 300,
          details: [
            { productItemId: "P002", warehouse: "Main Warehouse", location: "Aisle2" }
          ]
        }
      ]
    },
    {
      id: "PUR003",
      supplier: "Supplier C",
      total: 4000,
      orderDate: "2024-12-15",
      executed: true,
      received: false,
      products: [
        {
          name: "Tablet",
          quantity: 20,
          price: 200,
          details: [
            { productItemId: "P003", warehouse: "Secondary Warehouse", location: "Aisle3" }
          ]
        }
      ]
    },
    {
      id: "PUR004",
      supplier: "Supplier D",
      total: 8000,
      orderDate: "2024-12-20",
      executed: false,
      received: false,
      products: [
        {
          name: "Monitor",
          quantity: 40,
          price: 200,
          details: [
            { productItemId: "P004", warehouse: "Main Warehouse", location: "Aisle4" }
          ]
        }
      ]
    },
    {
      id: "PUR005",
      supplier: "Supplier E",
      total: 6000,
      orderDate: "2024-12-25",
      executed: true,
      received: false,
      products: [
        {
          name: "Keyboard",
          quantity: 60,
          price: 100,
          details: [
            { productItemId: "P005", warehouse: "Secondary Warehouse", location: "Aisle5" }
          ]
        }
      ]
    }
  ];

  const [purchaseData, setPurchaseData] = useState(initialPurchaseData);

  useEffect(() => {
    // Create a connection to the SignalR hub
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/purchasehub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    connection.start()
      .then(() => {
        console.log("Connected to SignalR hub");

        // Listen for updates from the server
        connection.on("ReceivePurchases", (data) => {
          console.log("Received purchases:", data);
          setPurchaseData(data);
        });

        // Optionally, you can invoke a method on the server to request initial purchases
        connection.invoke("GetInitialPurchases")
          .catch(err => console.error(err.toString()));
      })
      .catch(err => console.error("Error connecting to SignalR hub:", err));

    // Clean up the connection when the component unmounts
    return () => {
      connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    };
  }, []);

  const markAsExecuted = (purchaseId) => {
    setPurchaseData(prevData => {
      return prevData.map(purchase => purchase.id === purchaseId ? { ...purchase, executed: true } : purchase);
    });
  };

  const markAsReceived = (purchaseId) => {
    setPurchaseData(prevData => {
      return prevData.map(purchase => purchase.id === purchaseId ? { ...purchase, received: true } : purchase);
    });
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